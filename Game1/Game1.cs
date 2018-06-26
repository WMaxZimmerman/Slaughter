using System.Collections.Generic;
using Game1.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        // These are all global vaiables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState Move;
        KeyboardState Shoot;
        KeyboardState Menu_Move;
        KeyboardState Menu_Select;

        #region These are all of the Texture
        Texture2D Ghost1;
        Texture2D Ghost2;
        Texture2D Ghost3;
        Texture2D Ghost4;

        SpriteFont Banner;
        string Ban;
        
        Texture2D StoryMenu;
        Texture2D LoseScreen;
        Texture2D Background;
        Texture2D WinScreen;
        Texture2D Pointer;
        Texture2D MainMenu;
        Texture2D ControlMenu;

        SpriteFont Score;
        #endregion

        //Rectangles
        Rectangle Player_Rec;
        Rectangle Shield_Rec;

        //A Timer variable
        float timer = 0f;
        float Ghost_timer = 0f;
        //The interval (100 milliseconds)
        float interval = 100f;
        //Current frame holder (start at 1)
        int currentFrame = 1;
        int Ghost_Frame = 0;
        //Width of a single sprite image, not the whole Sprite Sheet
        int spriteWidth = 100;
        //Height of a single sprite image, not the whole Sprite Sheet
        int spriteHeight = 100;

        #region Theses are all vectors
        
        Vector2 AlienSpeed = new Vector2(50.0f, 50.0f);

        Vector2 WinScreenPosition = Vector2.Zero;
        Vector2 MainMenuPosition = Vector2.Zero;
        Vector2 PointerPosition = new Vector2(150, 140);
        Vector2 ControlMenuPosition = Vector2.Zero;
        Vector2 StoryMenuPosition = Vector2.Zero;
        Vector2 BannerPosition = Vector2.Zero;

        PlayerShip _playerGameObject;
        private GameObject playerMissle;
        private List<GameObject> alienShips;
        private List<GameObject> wallsObjects;
        private List<GameObject> alienMissles;
        private List<GameObject> GameObjects;
        #endregion

        // These are sprite speeds
        Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);
        Vector2 MissleSpeed = new Vector2(50.0f, 50.0f);

        #region These are all global integers
        int Fire_Count = 0;
        int Time = 0;
        int Menu_Hold = 0;
        int Game_State = 0;
        int Fire_Hold = 0;
        int Game_Start = 0;
        int Game_level = 7;
        int Alien1_H = 0;
        int Alien2_H = 0;
        int Alien3_H = 0;
        int Alien4_H = 0;
        int Missle_H = 1;
        int Walls = 1;
        int Level_H = 7;
        int Level_Cap = 7;
        int Alien1_H_Fire = 0;
        int Alien2_H_Fire = 0;
        int Alien3_H_Fire = 0;
        int Alien4_H_Fire = 0;
        int A1Missle_H = 1;
        int A2Missle_H = 1;
        int A3Missle_H = 1;
        int A4Missle_H = 1;
        int Play_H = 0;
        int Player_Velocity = 4;
        int Alien_Velocity = 2;
        int Missle_Velocity = 6;
        int AMissle_Velocity = 6;
        int Alien_direc = 1;
        int Wall_direc = 1;
        int Wall_Velocity = 1;
        int Tot_Score = 0;
        int A1_AccumH = 0;
        int A2_AccumH = 0;
        int A3_AccumH = 0;
        int A4_AccumH = 0;
        #endregion

        #region These are all Game state bools
        bool Play = false;
        bool Menu = true;
        bool Controls = false;
        bool Story = false;
        bool Win_Lose = false;
        bool win = false;
        bool lose = false;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            Ban = "We Come in Peace";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameObjects = new List<GameObject>();
            
            playerMissle = new GameObject(Content.Load<Texture2D>("Missle"), new Vector2(390, 388));

            GameObjects.Add(new PlayerShip(Content.Load<Texture2D>("Player_Ship"), new Vector2(360, 388), 100, 100));

            GameObjects.Add(new AlienShip(Content.Load<Texture2D>("Alien_Ship"), Vector2.Zero, 100, 100));
            GameObjects.Add(new AlienShip(Content.Load<Texture2D>("Alien_Ship"), new Vector2(200, 0), 100, 100));
            GameObjects.Add(new AlienShip(Content.Load<Texture2D>("Alien_Ship"), new Vector2(400, 0), 100, 100));
            GameObjects.Add(new AlienShip(Content.Load<Texture2D>("Alien_Ship"), new Vector2(600, 0), 100, 100));
            
            GameObjects.Add(new Wall(Content.Load<Texture2D>("Wall"), new Vector2(0, 203)));
            GameObjects.Add(new Wall(Content.Load<Texture2D>("Wall"), new Vector2(200, 203)));
            GameObjects.Add(new Wall(Content.Load<Texture2D>("Wall"), new Vector2(400, 203)));
            GameObjects.Add(new Wall(Content.Load<Texture2D>("Wall"), new Vector2(600, 203)));
            
            alienMissles = new List<GameObject>
            {
                new GameObject(Content.Load<Texture2D>("AMissle"), new Vector2(30, 0)),
                new GameObject(Content.Load<Texture2D>("AMissle"), new Vector2(230, 0)),
                new GameObject(Content.Load<Texture2D>("AMissle"), new Vector2(430, 0)),
                new GameObject(Content.Load<Texture2D>("AMissle"), new Vector2(630, 0))
            };
            
            Ghost1 = Content.Load<Texture2D>("Alien_Ghost");
            Ghost2 = Content.Load<Texture2D>("Alien_Ghost");
            Ghost3 = Content.Load<Texture2D>("Alien_Ghost");
            Ghost4 = Content.Load<Texture2D>("Alien_Ghost");

            //Menu Stuff
            MainMenu = Content.Load<Texture2D>("MAIN_MENU1");
            Pointer = Content.Load<Texture2D>("POINTER");
            WinScreen = Content.Load<Texture2D>("Win_Screen");
            ControlMenu = Content.Load<Texture2D>("CONTROLS_MENU");
            Banner = Content.Load<SpriteFont>("Banner");
            
            StoryMenu = Content.Load<Texture2D>("BACKSTORY_MENU");
            LoseScreen = Content.Load<Texture2D>("Lose_Screen");
            Background = Content.Load<Texture2D>("BackGround");
            Score = Content.Load<SpriteFont>("Score");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            //Game_State = 2;
            UpdateGameLevels();

            if (Menu)
            {
                UpdateMenu();
            }
            else if (Play)
            {
                UpdatePlay(gameTime);
            }
            else if (Win_Lose)
            {
                UpdateWinLose();
            }
            else if (Controls)
            {
                UpdateControls();
            }
            else if (Story)
            {
                UpdateStory();
            }

            ManageGameState();
        }

        private void UpdateGameLevels()
        {
            if (Game_level > 0)
            {
                if (Game_level == 1)
                {
                    // Performs initial setup for level 1

                    //Sets all initial positions for sprites
                    _playerGameObject.MoveToInitialPos();
                    playerMissle.MoveToInitialPos();

                    foreach (var alienShip in alienShips)
                    {
                        alienShip.Texture = Content.Load<Texture2D>("Alien_Ship");
                        alienShip.MoveToInitialPos();
                    }
                    
                    Alien1_H = 0;
                    Alien2_H = 0;
                    Alien3_H = 0;
                    Alien4_H = 0;
                    Missle_H = 1;
                    Walls = 1;
                    Level_H = 1;
                    A1_AccumH = 0;
                    A2_AccumH = 0;
                    A3_AccumH = 0;
                    A4_AccumH = 0;
                    Alien_Velocity = 2;

                    lose = false;
                    win = false;
                    Ban = "We come in Peace";

                    //Score = 0;

                    Game_Start = 0;
                }
                else if (Game_level >= 2)
                {
                    // Performs initial setup for level 1
                    
                    //Sets all initial positions for sprites
                    _playerGameObject.MoveToInitialPos();
                    playerMissle.MoveToInitialPos();

                    foreach (var alienShip in alienShips)
                    {
                        alienShip.Texture = Content.Load<Texture2D>("Alien_Ship");
                        alienShip.MoveToInitialPos();
                    }
                    
                    //Resets wall positions
                    foreach (var wall in wallsObjects)
                    {
                        wall.MoveToInitialPos();
                    }
                    
                    Alien1_H = 0;
                    Alien2_H = 0;
                    Alien3_H = 0;
                    Alien4_H = 0;
                    Missle_H = 1;
                    Walls = 0;
                    A1_AccumH = 0;
                    A2_AccumH = 0;
                    A3_AccumH = 0;
                    A4_AccumH = 0;
                    Alien_Velocity = 2;

                    lose = false;
                    win = false;

                    //Score = 0;

                    Game_Start = 0;
                }
            }
        }

        private void UpdateMenu()
        {
            Menu_Move = Keyboard.GetState();
            if (Menu_Move.IsKeyDown(Keys.Down) && Menu_Hold == 0 && PointerPosition.Y < 400)
            {
                Menu_Hold = 1;
                PointerPosition.Y = PointerPosition.Y + 65;
            }
            else if (Menu_Move.IsKeyDown(Keys.Up) && Menu_Hold == 0 && PointerPosition.Y > 140)
            {
                Menu_Hold = 1;
                PointerPosition.Y = PointerPosition.Y - 65;
            }
            else if (Menu_Move.IsKeyUp(Keys.Down) && Menu_Move.IsKeyUp(Keys.Up))
            {
                Menu_Hold = 0;
            }
            Menu_Select = Keyboard.GetState();
            if (Menu_Select.IsKeyDown(Keys.Enter))
            {
                if (PointerPosition.Y == 140)
                {
                    Game_State = 1;
                }
                else if (PointerPosition.Y == 205 && Play_H == 0)
                {
                    Game_State = 2;
                }
                else if (PointerPosition.Y == 270)
                {
                    Game_State = 3;
                }
                else if (PointerPosition.Y == 335)
                {
                    Game_State = 4;
                }
                else if (PointerPosition.Y == 400)
                {
                    Game_State = 5;
                }
            }
        }

        private void UpdatePlay(GameTime gameTime)
        {
            int MaxX = graphics.GraphicsDevice.Viewport.Width - 100;
            int MaxX_A = graphics.GraphicsDevice.Viewport.Width - alienShips[0].Width;
            int MaxX_W = graphics.GraphicsDevice.Viewport.Width - wallsObjects[0].Width;
            int MinX = 0;
            int MaxY = graphics.GraphicsDevice.Viewport.Height - _playerGameObject.Texture.Height;

            Play_H = 0;

            Game_State = 2;
            Game_level = 0;
            Time++;
            
            _playerGameObject.Update(gameTime, ref Game_Start, GameObjects);

            BulletDeployment();

            CheckForMissleHittingWall();

            CheckForMissleHittingAlien();

            LevelDifferences(gameTime, MaxX_W, MinX);

            AlienMovement(MaxX_A, MinX);

            AlienVelocityAccumulator();

            CheckForBorder(MaxX, MinX);

            CheckForWin();
        }
        
        private void BulletDeployment()
        {
            Shoot = Keyboard.GetState();
            if (Shoot.IsKeyDown(Keys.Up))
            {
                Game_Start = 1;
                if (playerMissle.Position.Y < 0)
                {
                    Fire_Hold = 0;
                }
                Fire_Count++;
                if (playerMissle.Position.Y == 388 || Fire_Hold == 0 || Missle_H == 1)
                {
                    Fire_Hold = 1;
                    playerMissle.Position.Y = 388;
                    playerMissle.Position.X = _playerGameObject.Position.X + 39;
                }
                Missle_H = 0;
            }
            if (Missle_H == 0)
            {
                playerMissle.Position.Y = playerMissle.Position.Y - Missle_Velocity;
            }
        }

        private void CheckForMissleHittingWall()
        {
            if (Walls == 0)
            {
                foreach (var wall in wallsObjects)
                {
                    if (playerMissle.Position.Y >= wall.Position.Y && playerMissle.Position.Y <= wall.Position.Y + 17 && playerMissle.Position.X >= wall.Position.X && playerMissle.Position.X <= wall.Position.X + 94)
                    {
                        Missle_H = 1;
                    }
                }
            }
        }

        private void CheckForMissleHittingAlien()
        {
            if (Missle_H == 0 && playerMissle.Position.Y >= alienShips[0].Position.Y && playerMissle.Position.Y <= alienShips[0].Position.Y + 100 && playerMissle.Position.X >= alienShips[0].Position.X && playerMissle.Position.X <= alienShips[0].Position.X + 100 && Alien1_H == 0)
            {
                Alien1_H = 1;
                Missle_H = 1;
                Tot_Score = Tot_Score + 100;
            }
            else if (Missle_H == 0 && playerMissle.Position.Y >= alienShips[1].Position.Y && playerMissle.Position.Y <= alienShips[1].Position.Y + 100 && playerMissle.Position.X >= alienShips[1].Position.X && playerMissle.Position.X <= alienShips[1].Position.X + 100 && Alien2_H == 0)
            {
                Alien2_H = 1;
                Missle_H = 1;
                Tot_Score = Tot_Score + 100;
            }
            else if (Missle_H == 0 && playerMissle.Position.Y >= alienShips[2].Position.Y && playerMissle.Position.Y <= alienShips[2].Position.Y + 100 && playerMissle.Position.X >= alienShips[2].Position.X && playerMissle.Position.X <= alienShips[2].Position.X + 100 && Alien3_H == 0)
            {
                Alien3_H = 1;
                Missle_H = 1;
                Tot_Score = Tot_Score + 100;
            }
            else if (Missle_H == 0 && playerMissle.Position.Y >= alienShips[3].Position.Y && playerMissle.Position.Y <= alienShips[3].Position.Y + 100 && playerMissle.Position.X >= alienShips[3].Position.X && playerMissle.Position.X <= alienShips[3].Position.X + 100 && Alien4_H == 0)
            {
                Alien4_H = 1;
                Missle_H = 1;
                Tot_Score = Tot_Score + 100;
            }
        }

        private void LevelDifferences(GameTime gameTime, int MaxX_W, int MinX)
        {
            if (Level_H >= 3)
            {
                Ban = "We tried being peaceful";
                AlienOneShooting();

                AlienTwoShooting();

                AlienThreeShooting();

                AlienFourShooting();

                TurnWallsOff();

                CheckForAlienMissleHittingPlayerMissle();

                MoveWallsSideToSide(MaxX_W, MinX);

                CheckForMissleHittingPlayer();

                CheckForAlienGhost(gameTime);
            }
        }

        private void AlienOneShooting()
        {
            if (Game_Start == 1 && Alien1_H == 0 && ((alienShips[0].Position.X >= _playerGameObject.Position.X && alienShips[0].Position.X <= _playerGameObject.Position.X + 100) || (alienShips[0].Position.X + 140 >= _playerGameObject.Position.X && alienShips[0].Position.X + 140 <= _playerGameObject.Position.X + 100)))
            {
                if (alienMissles[0].Position.Y > 400)
                {
                    Alien1_H_Fire = 0;
                }
                if (alienMissles[0].Position.Y == 0 || Alien1_H_Fire == 0 || A1Missle_H == 1)
                {
                    Alien1_H_Fire = 1;
                    alienMissles[0].Position.Y = 0;
                    alienMissles[0].Position.X = alienShips[0].Position.X + 30;
                }
                A1Missle_H = 0;
            }

            if (A1Missle_H == 0)
            {
                alienMissles[0].Position.Y = alienMissles[0].Position.Y + AMissle_Velocity;
            }
        }

        private void AlienTwoShooting()
        {
            if (Game_Start == 1 && Alien2_H == 0 && ((alienShips[1].Position.X >= _playerGameObject.Position.X && alienShips[1].Position.X <= _playerGameObject.Position.X + 100) || (alienShips[1].Position.X + 140 >= _playerGameObject.Position.X && alienShips[1].Position.X + 140 <= _playerGameObject.Position.X + 100)))
            {
                if (alienMissles[1].Position.Y > 400)
                {
                    Alien2_H_Fire = 0;
                }
                if (alienMissles[1].Position.Y == 0 || Alien2_H_Fire == 0 || A2Missle_H == 1)
                {
                    Alien2_H_Fire = 1;
                    alienMissles[1].Position.Y = 0;
                    alienMissles[1].Position.X = alienShips[1].Position.X + 30;
                }
                A2Missle_H = 0;
            }

            if (A2Missle_H == 0)
            {
                alienMissles[1].Position.Y = alienMissles[1].Position.Y + AMissle_Velocity;
            }
        }

        private void AlienThreeShooting()
        {
            if (Game_Start == 1 && Alien3_H == 0 && ((alienShips[2].Position.X >= _playerGameObject.Position.X && alienShips[2].Position.X <= _playerGameObject.Position.X + 100) || (alienShips[2].Position.X + 140 >= _playerGameObject.Position.X && alienShips[2].Position.X + 140 <= _playerGameObject.Position.X + 100)))
            {
                if (alienMissles[2].Position.Y > 400)
                {
                    Alien3_H_Fire = 0;
                }
                if (alienMissles[2].Position.Y == 0 || Alien3_H_Fire == 0 || A3Missle_H == 1)
                {
                    Alien3_H_Fire = 1;
                    alienMissles[2].Position.Y = 0;
                    alienMissles[2].Position.X = alienShips[2].Position.X + 30;
                }
                A3Missle_H = 0;
            }

            if (A3Missle_H == 0)
            {
                alienMissles[2].Position.Y = alienMissles[2].Position.Y + AMissle_Velocity;
            }
        }

        private void AlienFourShooting()
        {
            if (Game_Start == 1 && Alien4_H == 0 && ((alienShips[3].Position.X >= _playerGameObject.Position.X && alienShips[3].Position.X <= _playerGameObject.Position.X + 100) || (alienShips[3].Position.X + 140 >= _playerGameObject.Position.X && alienShips[3].Position.X + 140 <= _playerGameObject.Position.X + 100)))
            {
                if (alienMissles[3].Position.Y > 400)
                {
                    Alien4_H_Fire = 0;
                }
                if (alienMissles[3].Position.Y == 0 || Alien4_H_Fire == 0 || A4Missle_H == 1)
                {
                    Alien4_H_Fire = 1;
                    alienMissles[3].Position.Y = 0;
                    alienMissles[3].Position.X = alienShips[3].Position.X + 30;
                }
                A4Missle_H = 0;
            }

            if (A4Missle_H == 0)
            {
                alienMissles[3].Position.Y = alienMissles[3].Position.Y + AMissle_Velocity;
            }
        }

        private void TurnWallsOff()
        {
            if (Level_H < 6)
            {
                if (alienMissles[0].Position.Y + 65 >= wallsObjects[0].Position.Y && alienMissles[0].Position.Y + 65 <= wallsObjects[0].Position.Y + 17 && alienMissles[0].Position.X >= wallsObjects[0].Position.X && alienMissles[0].Position.X <= wallsObjects[0].Position.X + 94)
                {
                    A1Missle_H = 1;
                }
                if (alienMissles[1].Position.Y + 65 >= wallsObjects[1].Position.Y && alienMissles[1].Position.Y + 65 <= wallsObjects[1].Position.Y + 17 && alienMissles[1].Position.X >= wallsObjects[1].Position.X && alienMissles[1].Position.X <= wallsObjects[1].Position.X + 94)
                {
                    A2Missle_H = 1;
                }
                if (alienMissles[2].Position.Y + 65 >= wallsObjects[2].Position.Y && alienMissles[2].Position.Y + 65 <= wallsObjects[2].Position.Y + 17 && alienMissles[2].Position.X >= wallsObjects[2].Position.X && alienMissles[2].Position.X <= wallsObjects[2].Position.X + 94)
                {
                    A3Missle_H = 1;
                }
                if (alienMissles[2].Position.Y + 65 >= wallsObjects[3].Position.Y && alienMissles[2].Position.Y + 65 <= wallsObjects[3].Position.Y + 17 && alienMissles[2].Position.X >= wallsObjects[3].Position.X && alienMissles[2].Position.X <= wallsObjects[3].Position.X + 94)
                {
                    A4Missle_H = 1;
                }
            }
        }

        private void CheckForAlienMissleHittingPlayerMissle()
        {
            if (Level_H >= 3)
            {
                foreach (var alienMissle in alienMissles)
                {
                    if (Missle_H == 0 && alienMissle.Position.Y + 65 >= playerMissle.Position.Y && alienMissle.Position.Y + 65 <= playerMissle.Position.Y + 17 && alienMissle.Position.X >= playerMissle.Position.X && alienMissle.Position.X <= playerMissle.Position.X + 83)
                    {
                        Missle_H = 1;
                        if (Level_H < 5)
                        {
                            A1Missle_H = 1;
                            A2Missle_H = 1;
                            A3Missle_H = 1;
                            A4Missle_H = 1;
                        }
                    }
                }
            }
        }

        private void MoveWallsSideToSide(int MaxX_W, int MinX)
        {
            if (Level_H >= 4)
            {
                if (wallsObjects[0].Position.X < MinX)
                {
                    Wall_direc = 1;
                }
                else if (wallsObjects[3].Position.X > MaxX_W)
                {
                    Wall_direc = -1;
                }

                foreach (var wall in wallsObjects)
                {
                    wall.Position.X = wall.Position.X + (Wall_Velocity * Wall_direc);
                }
            }
        }

        private void CheckForMissleHittingPlayer()
        {
            if (A1Missle_H == 0 && alienMissles[0].Position.Y + 65 >= _playerGameObject.Position.Y && alienMissles[0].Position.Y + 65 <= _playerGameObject.Position.Y + 17 && alienMissles[0].Position.X >= _playerGameObject.Position.X && alienMissles[0].Position.X <= _playerGameObject.Position.X + 83)
            {
                A1Missle_H = 1;
                Tot_Score = Tot_Score - 100;
                lose = true;
                Game_State = -1;
            }
            if (A2Missle_H == 0 && alienMissles[1].Position.Y + 65 >= _playerGameObject.Position.Y && alienMissles[1].Position.Y + 65 <= _playerGameObject.Position.Y + 17 && alienMissles[1].Position.X >= _playerGameObject.Position.X && alienMissles[1].Position.X <= _playerGameObject.Position.X + 83)
            {
                A2Missle_H = 1;
                Tot_Score = Tot_Score - 100;
                lose = true;
                Game_State = -1;
            }
            if (A3Missle_H == 0 && alienMissles[2].Position.Y + 65 >= _playerGameObject.Position.Y && alienMissles[2].Position.Y + 65 <= _playerGameObject.Position.Y + 17 && alienMissles[2].Position.X >= _playerGameObject.Position.X && alienMissles[2].Position.X <= _playerGameObject.Position.X + 83)
            {
                A3Missle_H = 1;
                Tot_Score = Tot_Score - 100;
                lose = true;
                Game_State = -1;
            }
            if (A4Missle_H == 0 && alienMissles[3].Position.Y + 65 >= _playerGameObject.Position.Y && alienMissles[3].Position.Y + 65 <= _playerGameObject.Position.Y + 17 && alienMissles[3].Position.X >= _playerGameObject.Position.X && alienMissles[3].Position.X <= _playerGameObject.Position.X + 83)
            {
                A4Missle_H = 1;
                Tot_Score = Tot_Score - 100;
                lose = true;
                Game_State = -1;
            }
        }

        private void CheckForAlienGhost(GameTime gameTime)
        {
            if (Level_H >= 7)
            {
                Shield_Rec = new Rectangle((Ghost_Frame * spriteWidth) - 100, 0, spriteWidth, spriteHeight);

                Ghost_timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (Ghost_timer > interval)
                {
                    //Show the next frame
                    Ghost_Frame--;
                    //Reset the timer
                    Ghost_timer = 0f;
                }
                //If we are on the last frame, reset back to the one before the first frame (because currentFrame++ is called next so the next frame will be 1!)
                if (Ghost_Frame < 1)
                {
                    Ghost_Frame = 3;
                }

                if (Missle_H == 0 && playerMissle.Position.Y >= alienShips[0].Position.Y && playerMissle.Position.Y <= alienShips[0].Position.Y + 100 && playerMissle.Position.X >= alienShips[0].Position.X && playerMissle.Position.X <= alienShips[0].Position.X + 100)
                {
                    if (Alien1_H == 1)
                    {
                        Alien1_H = 0;
                        Missle_H = 1;
                    }
                    else
                    {
                        Alien1_H = 1;
                        Missle_H = 1;
                    }
                }
                else if (Missle_H == 0 && playerMissle.Position.Y >= alienShips[1].Position.Y && playerMissle.Position.Y <= alienShips[1].Position.Y + 100 && playerMissle.Position.X >= alienShips[1].Position.X && playerMissle.Position.X <= alienShips[1].Position.X + 100)
                {
                    if (Alien2_H == 1)
                    {
                        Alien2_H = 0;
                        Missle_H = 1;
                    }
                    else
                    {
                        Alien2_H = 1;
                        Missle_H = 1;
                    }
                }
                else if (Missle_H == 0 && playerMissle.Position.Y >= alienShips[2].Position.Y && playerMissle.Position.Y <= alienShips[2].Position.Y + 100 && playerMissle.Position.X >= alienShips[2].Position.X && playerMissle.Position.X <= alienShips[2].Position.X + 100)
                {
                    if (Alien3_H == 1)
                    {
                        Alien3_H = 0;
                        Missle_H = 1;
                    }
                    else
                    {
                        Alien3_H = 1;
                        Missle_H = 1;
                    }
                }
                else if (Missle_H == 0 && playerMissle.Position.Y >= alienShips[3].Position.Y && playerMissle.Position.Y <= alienShips[3].Position.Y + 100 && playerMissle.Position.X >= alienShips[3].Position.X && playerMissle.Position.X <= alienShips[3].Position.X + 100)
                {
                    if (Alien4_H == 1)
                    {
                        Alien4_H = 0;
                        Missle_H = 1;
                    }
                    else
                    {
                        Alien4_H = 1;
                        Missle_H = 1;
                    }
                }
            }
        }

        private void AlienMovement(int MaxX_A, int MinX)
        {
            //Move Alien side to side
            if (Alien1_H == 0)
            {
                if (alienShips[0].Position.X < MinX)
                {
                    Alien_direc = 1;
                }
            }
            else if (Alien2_H == 0)
            {
                if (alienShips[1].Position.X < MinX)
                {
                    Alien_direc = 1;
                }
            }
            else if (Alien3_H == 0)
            {
                if (alienShips[2].Position.X < MinX)
                {
                    Alien_direc = 1;
                }
            }
            else if (Alien4_H == 0)
            {
                if (alienShips[3].Position.X < MinX)
                {
                    Alien_direc = 1;
                }
            }
            if (Alien4_H == 0)
            {
                if (alienShips[3].Position.X > MaxX_A)
                {
                    Alien_direc = -1;
                }
            }
            else if (Alien3_H == 0)
            {
                if (alienShips[2].Position.X > MaxX_A)
                {
                    Alien_direc = -1;
                }
            }
            else if (Alien2_H == 0)
            {
                if (alienShips[1].Position.X > MaxX_A)
                {
                    Alien_direc = -1;
                }
            }
            else if (Alien1_H == 0)
            {
                if (alienShips[0].Position.X > MaxX_A)
                {
                    Alien_direc = -1;
                }
            }

            foreach (var alienShip in alienShips)
            {
                alienShip.Position.X = alienShip.Position.X + (Alien_Velocity * Alien_direc);
            }
        }

        private void AlienVelocityAccumulator()
        {
            if (Alien1_H == 1 && A1_AccumH == 0)
            {
                Alien_Velocity++;
                A1_AccumH = 1;
            }
            else if (Alien2_H == 1 && A2_AccumH == 0)
            {
                Alien_Velocity++;
                A2_AccumH = 1;
            }
            else if (Alien3_H == 1 && A3_AccumH == 0)
            {
                Alien_Velocity++;
                A3_AccumH = 1;
            }
            else if (Alien4_H == 1 && A4_AccumH == 0)
            {
                Alien_Velocity++;
                A4_AccumH = 1;
            }
        }

        private void CheckForBorder(int MaxX, int MinX)
        {
            if (_playerGameObject.Position.X > MaxX)
            {
                _playerGameObject.Position.X = MaxX;
            }

            else if (_playerGameObject.Position.X < MinX)
            {
                _playerGameObject.Position.X = MinX;
            }

            Menu_Select = Keyboard.GetState();
            if (Menu_Select.IsKeyDown(Keys.P))
            {
                Game_State = 0;
            }
        }

        private void CheckForWin()
        {
            if (Alien1_H == 1 && Alien2_H == 1 && Alien3_H == 1 && Alien4_H == 1 && lose != true)
            {
                win = true;
                if (Level_H == Level_Cap)
                {
                    Game_State = -1;
                }
                Level_H++;
                Game_level = Level_H;
                if (Game_level > Level_Cap)
                {
                    Game_level = 0;
                    win = true;
                    lose = false;
                }
                Tot_Score = Tot_Score - (Fire_Count + (Time / 1000));
            }
        }

        private void UpdateWinLose()
        {
            Menu_Select = Keyboard.GetState();
            if (Menu_Select.IsKeyDown(Keys.P))
            {
                Game_State = 0;
            }
        }

        private void UpdateControls()
        {
            Menu_Select = Keyboard.GetState();
            if (Menu_Select.IsKeyDown(Keys.Back))
            {
                Game_State = 0;
            }
        }

        private void UpdateStory()
        {
            Menu_Select = Keyboard.GetState();
            if (Menu_Select.IsKeyDown(Keys.Back))
            {
                Game_State = 0;
            }
        }

        private void ManageGameState()
        {
            if (Game_State == 0)
            {
                Menu = true;
                Play = false;
                Controls = false;
                Story = false;
                Win_Lose = false;
            }
            else if (Game_State == 1)
            {
                Game_level = 1;
                Menu = false;
                Play = true;
            }
            else if (Game_State == 2 && lose != true)
            {
                Menu = false;
                Play = true;
            }
            else if (Game_State == 3)
            {
                Menu = false;
                Controls = true;
            }
            else if (Game_State == 4)
            {
                Menu = false;
                Story = true;
            }
            else if (Game_State == 5)
            {
                this.Exit();
            }
            else if (Game_State == -1)
            {
                Play = false;
                Win_Lose = true;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(Background, MainMenuPosition, Color.White);
            spriteBatch.End();
            //Check for game state
            if (Menu)
            {
                DrawMenu();
            }
            else if (Play)
            {
                DrawGame();
            }
            else if (Win_Lose)
            {
                DrawWinLose();
            }
            else if (Controls)
            {
                DrawControls();
            }
            else if (Story)
            {
                DrawStory();
            }
            base.Draw(gameTime);
        }

        private void DrawStory()
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(StoryMenu, StoryMenuPosition, Color.White);
            spriteBatch.End();
        }

        private void DrawControls()
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(ControlMenu, ControlMenuPosition, Color.White);
            spriteBatch.End();
        }

        private void DrawMenu()
        {
            // Draws Menu and pointer
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(Pointer, PointerPosition, Color.White);
            spriteBatch.Draw(MainMenu, MainMenuPosition, Color.White);
            spriteBatch.End();
        }

        private void DrawGame()
        {
            // Draw the sprite.
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            if (Missle_H == 0)
            {
                playerMissle.Draw(spriteBatch);
            }
            _playerGameObject.Draw(spriteBatch);
            if (Level_H >= 3)
            {
                if (A1Missle_H == 0)
                {
                    alienMissles[0].Draw(spriteBatch);
                }
                if (A2Missle_H == 0)
                {
                    alienMissles[1].Draw(spriteBatch);
                }
                if (A3Missle_H == 0)
                {
                    alienMissles[2].Draw(spriteBatch);
                }
                if (A4Missle_H == 0)
                {
                    alienMissles[3].Draw(spriteBatch);
                }
                if (Level_H >= 7)
                {
                    if (Alien1_H == 1)
                    {
                        spriteBatch.Draw(Ghost1, alienShips[0].Position, Shield_Rec, Color.White);
                    }
                    if (Alien2_H == 1)
                    {
                        spriteBatch.Draw(Ghost2, alienShips[1].Position, Shield_Rec, Color.White);
                    }
                    if (Alien3_H == 1)
                    {
                        spriteBatch.Draw(Ghost3, alienShips[2].Position, Shield_Rec, Color.White);
                    }
                    if (Alien4_H == 1)
                    {
                        spriteBatch.Draw(Ghost4, alienShips[3].Position, Shield_Rec, Color.White);
                    }
                }
            }
            if (Alien1_H == 0 && Game_Start != 0)
            {
                alienShips[0].Draw(spriteBatch);
            }
            if (Alien2_H == 0 && Game_Start != 0)
            {
                alienShips[1].Draw(spriteBatch);
            }
            if (Alien3_H == 0 && Game_Start != 0)
            {
                alienShips[2].Draw(spriteBatch);
            }
            if (Alien4_H == 0 && Game_Start != 0)
            {
                alienShips[3].Draw(spriteBatch);
            }
            if (Walls == 0)
            {
                foreach (var wall in wallsObjects)
                {
                    wall.Draw(spriteBatch);
                }
            }
            if (Game_Start == 0)
            {
                spriteBatch.DrawString(Banner, Ban, BannerPosition, Color.Red);
            }
            spriteBatch.End();
        }

        private void DrawWinLose()
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            if (lose)
            {
                spriteBatch.DrawString(Score, "Score: " + Tot_Score, Vector2.Zero, Color.White);
                spriteBatch.Draw(LoseScreen, WinScreenPosition, Color.White);
            }
            else if (win)
            {
                spriteBatch.DrawString(Score, "Score: " + Tot_Score, Vector2.Zero, Color.White);
                spriteBatch.Draw(WinScreen, WinScreenPosition, Color.White);
            }
            spriteBatch.End();
        }
    }
}
