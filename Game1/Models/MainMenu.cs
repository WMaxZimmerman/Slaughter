using System.Linq;
using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Models
{
    public class MainMenu: Menu
    {
        private bool _holdMenu;

        public MainMenu(Texture2D texture, Vector2 postion) : base(texture, postion)
        {
            _holdMenu = false;
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            var pointer = game.Objects.OfType<MenuPointer>().Single();

            if (game.Inputs.IsKeyDown(Keys.Down) && !_holdMenu && pointer.Position.Y < 400)
            {
                _holdMenu = true;
                pointer.Position.Y = pointer.Position.Y + 65;
            }
            else if (game.Inputs.IsKeyDown(Keys.Up) && !_holdMenu && pointer.Position.Y > 140)
            {
                _holdMenu = true;
                pointer.Position.Y = pointer.Position.Y - 65;
            }
            else if (game.Inputs.IsKeyUp(Keys.Down) && game.Inputs.IsKeyUp(Keys.Up))
            {
                _holdMenu = false;
            }

            if (game.Inputs.IsKeyDown(Keys.Enter))
            {
                if (pointer.Position.Y == 140)
                {
                    game.SceneSwitch(1);
                }
                else if (pointer.Position.Y == 205)
                {
                    game.SceneSwitch(1);
                }
                else if (pointer.Position.Y == 270)
                {
                    game.SceneSwitch(-1);
                }
                else if (pointer.Position.Y == 335)
                {
                    game.SceneSwitch(-2);
                }
                else if (pointer.Position.Y == 400)
                {
                    game.Exit();
                }
            }
        }
    }
}
