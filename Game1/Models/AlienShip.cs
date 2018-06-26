using System.Linq;
using Game1.Enums;
using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class AlienShip: Ship
    {
        private int _shootTimer = 1000;
        private int _shootClock = 0;

        public AlienShip(Texture2D texture, Vector2 postion, int height, int width) : base(texture, postion, height, width)
        {
            Direction = 1;
            Velocity = 2;
        }


        public override void Update(GameTime gameTime, MainGame game)
        {
            if (game.State != GameState.Play) return;

            CheckHoldFire(gameTime);

            Movement(game);

            Shoot(game);
        }

        private void Movement(MainGame game)
        {
            var alienShips = game.Objects.Where(o => o is AlienShip).ToList();

            if (alienShips.Any(a => a.Position.X < 0))
            {
                Direction = 1;
            }
            else if (alienShips.Any(a => a.Position.X > game.GraphicsDevice.Viewport.Width - Width))
            {
                Direction = -1;
            }

            Position.X = Position.X + (Velocity * Direction);
        }

        private void CheckHoldFire(GameTime gameTime)
        {
            if (HoldFile)
            {
                _shootClock += gameTime.ElapsedGameTime.Milliseconds;
                if (_shootClock >= _shootTimer)
                {
                    _shootClock = 0;
                    HoldFile = false;
                }
            }
        }

        private void Shoot(MainGame game)
        {
            if (game.Level < 3) return;
            if (HoldFile) return;

            var playerShip = game.Objects.OfType<PlayerShip>().Single();

            if ((Position.X >= playerShip.Position.X && Position.X <= playerShip.Position.X + 100) || (Position.X + 140 >= playerShip.Position.X && Position.X + 140 <= playerShip.Position.X + 100))
            {
                HoldFile = true;
                var newMissle = new AlienMissle(game.Textures["AlienMissle"], new Vector2(Position.X + 30, 0));
                game.AddObject(newMissle);
            }
        }

        public override void Destroy(MainGame game, GameObject sender)
        {
            if (sender is PlayerMissle)
            {
                foreach (var alien in game.Objects.OfType<AlienShip>())
                {
                    alien.Velocity++;
                }
            }
            
            //Removes This Object
            base.Destroy(game, sender);

            if (game.Objects.OfType<AlienShip>().Count(a => a.IsRemoved == false) == 0)
            {
                game.SceneSwitch(game.Level + 1);
            }
        }
    }
}
