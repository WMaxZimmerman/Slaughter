using System.Linq;
using Game1.Enums;
using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class Wall: GameObject
    {
        public int Direction { get; set; }
        public int Velocity { get; set; }

        public Wall(Texture2D texture, Vector2 postion) : base(texture, postion)
        {
            Velocity = 1;
            Direction = 1;
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            if (game.State != GameState.Play) return;

            Move(game);
        }

        private void Move(MainGame game)
        {
            if (game.Level >= 4)
            {
                var walls = game.Objects.Where(o => o is Wall).ToList();

                if (walls.Any(a => a.Position.X < 0))
                {
                    Direction = 1;
                }
                else if (walls.Any(a => a.Position.X > game.GraphicsDevice.Viewport.Width - Width))
                {
                    Direction = -1;
                }

                Position.X = Position.X + (Velocity * Direction);
            }
        }
    }
}
