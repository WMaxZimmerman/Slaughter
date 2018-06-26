using System.Linq;
using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class AlienMissle: Missile
    {
        public AlienMissle(Texture2D texture, Vector2 postion) : base(texture, postion)
        {
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            Move();

            CheckCollision(game);

            if (Position.Y > game.GraphicsDevice.Viewport.Height - Height) Destroy(game, null);
        }

        private void Move()
        {
            Position.Y = Position.Y + Velocity;
        }

        private void CheckCollision(MainGame game)
        {
            if (game.Level < 6)
            {
                foreach (var wall in game.Objects.OfType<Wall>())
                {
                    if (CollidesWith(wall))
                    {
                        IsRemoved = true;
                    }
                }
            }
            
            foreach (var playerMissle in game.Objects.OfType<PlayerShip>())
            {
                if (CollidesWith(playerMissle))
                {
                    playerMissle.Destroy(game, this);
                    if (game.Level < 5)
                    {
                        IsRemoved = true;
                    }
                }
            }
        }
    }
}
