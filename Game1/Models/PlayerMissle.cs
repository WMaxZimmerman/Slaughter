using System.Linq;
using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class PlayerMissle: Missile
    {
        public PlayerMissle(Texture2D texture, Vector2 postion) : base(texture, postion)
        {
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            Move();

            CheckCollision(game);

            if (Position.Y < 0) Remove(game);
        }

        private void Move()
        {
            Position.Y = Position.Y - Velocity;
        }

        private void CheckCollision(MainGame game)
        {
            foreach (var wall in game.Objects.OfType<Wall>())
            {
                if (CollidesWith(wall))
                {
                    Remove(game);
                }
            }

            foreach (var alien in game.Objects.OfType<AlienShip>())
            {
                if (CollidesWith(alien))
                {
                    alien.Destroy(game, this);
                    Remove(game);
                }
            }
        }

        public override void Destroy(MainGame game, GameObject sender)
        {
            Remove(game);
        }

        private void Remove(MainGame game)
        {
            IsRemoved = true;
            var ship = (PlayerShip)game.Objects.Single(o => o is PlayerShip);
            ship.HoldFile = false;
        }
    }
}
