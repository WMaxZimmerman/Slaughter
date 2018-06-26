using System.Collections.Generic;
using System.Linq;
using Game1.Enums;
using Game1.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Game
{
    public class MainGame: Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public List<GameObject> Objects { get; set; }
        public GameState State { get; set; }
        public Dictionary<string, Texture2D> Textures { get; set; }
        public Dictionary<string, SpriteFont> Fonts { get; set; }
        public KeyboardState Inputs { get; set; }
        public int Level { get; set; }

        private bool _sceneSwitched;
        private int _newScene;

        private List<GameObject> _newObjects = new List<GameObject>();
        
        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Objects = new List<GameObject>();
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            Level = 0;
            State = GameState.Pause;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Textures = new Dictionary<string, Texture2D>();

            Textures.Add("MainMenu", Content.Load<Texture2D>("MAIN_MENU1"));
            Textures.Add("Pointer", Content.Load<Texture2D>("POINTER"));
            Textures.Add("WinScreen", Content.Load<Texture2D>("Win_Screen"));
            Textures.Add("ControlsMenu", Content.Load<Texture2D>("CONTROLS_MENU"));
            Textures.Add("BackstoryMenu", Content.Load<Texture2D>("BACKSTORY_MENU"));
            Textures.Add("LoseScreen", Content.Load<Texture2D>("Lose_Screen"));
            Textures.Add("Background", Content.Load<Texture2D>("BackGround"));

            Textures.Add("PlayerMissle", Content.Load<Texture2D>("Missle"));
            Textures.Add("PlayerShip", Content.Load<Texture2D>("Player_Ship"));
            Textures.Add("AlienShip", Content.Load<Texture2D>("Alien_Ship"));
            Textures.Add("Wall", Content.Load<Texture2D>("Wall"));
            Textures.Add("AlienMissle", Content.Load<Texture2D>("AMissle"));
            Textures.Add("AlienGhost", Content.Load<Texture2D>("Alien_Ghost"));

            Fonts = new Dictionary<string, SpriteFont>();

            Fonts.Add("Banner", Content.Load<SpriteFont>("Banner"));
            Fonts.Add("Score", Content.Load<SpriteFont>("Score"));

            SceneSwitch(0);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            Inputs = Keyboard.GetState();

            foreach (var gameObject in Objects)
            {
                gameObject.Update(gameTime, this);
            }

            Objects.RemoveAll(o => o.IsRemoved);

            Objects.AddRange(_newObjects);
            _newObjects = new List<GameObject>();

            if (_sceneSwitched) SceneSwitch();
        }
        
        public void SceneSwitch(int level)
        {
            _newScene = level;
            _sceneSwitched = true;
        }

        private void SceneSwitch()
        {
            _sceneSwitched = false;
            Objects.RemoveAll(o => true);
            Level = _newScene;
            
            if (_newScene == 0)
            {
                Objects.Add(new GameObject(Textures["Background"], Vector2.Zero) { Layer = 0 });
                Objects.Add(new MainMenu(Textures["MainMenu"], Vector2.Zero));
                Objects.Add(new MenuPointer(Textures["Pointer"], new Vector2(150, 140)));
            }
            else if (_newScene == -1)
            {
                Objects.Add(new GameObject(Textures["Background"], Vector2.Zero) { Layer = 0 });
                Objects.Add(new Menu(Textures["BackstoryMenu"], Vector2.Zero));
            }
            else if (_newScene == -2)
            {
                Objects.Add(new GameObject(Textures["Background"], Vector2.Zero) { Layer = 0 });
                Objects.Add(new Menu(Textures["ControlsMenu"], Vector2.Zero));
            }
            else if (_newScene == 1)
            {
                State = GameState.Pause;
                Objects.Add(new Menu(Textures["Background"], Vector2.Zero) { Layer = 0 });

                Objects.Add(new PlayerShip(Textures["PlayerShip"], new Vector2(360, 388), 100, 100));

                Objects.Add(new AlienShip(Textures["AlienShip"], Vector2.Zero, 100, 100));
                Objects.Add(new AlienShip(Textures["AlienShip"], new Vector2(200, 0), 100, 100));
                Objects.Add(new AlienShip(Textures["AlienShip"], new Vector2(400, 0), 100, 100));
                Objects.Add(new AlienShip(Textures["AlienShip"], new Vector2(600, 0), 100, 100));
            }
            else if (_newScene > 1)
            {
                State = GameState.Pause;
                Objects.Add(new Menu(Textures["Background"], Vector2.Zero) { Layer = 0 });

                Objects.Add(new PlayerShip(Textures["PlayerShip"], new Vector2(360, 388), 100, 100));

                Objects.Add(new AlienShip(Textures["AlienShip"], Vector2.Zero, 100, 100));
                Objects.Add(new AlienShip(Textures["AlienShip"], new Vector2(200, 0), 100, 100));
                Objects.Add(new AlienShip(Textures["AlienShip"], new Vector2(400, 0), 100, 100));
                Objects.Add(new AlienShip(Textures["AlienShip"], new Vector2(600, 0), 100, 100));

                Objects.Add(new Wall(Textures["Wall"], new Vector2(0, 203)));
                Objects.Add(new Wall(Textures["Wall"], new Vector2(200, 203)));
                Objects.Add(new Wall(Textures["Wall"], new Vector2(400, 203)));
                Objects.Add(new Wall(Textures["Wall"], new Vector2(600, 203)));
            }
        }

        public void AddObject(GameObject newObject)
        {
            _newObjects.Add(newObject);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            foreach (var gameObject in Objects.OrderBy(o => o.Layer))
            {
                gameObject.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
