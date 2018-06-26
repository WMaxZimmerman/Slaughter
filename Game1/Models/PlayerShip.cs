using System.Collections.Generic;
using System.Linq;
using Game1.Enums;
using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Models
{
    public class PlayerShip: Ship
    {
        public int FireCount { get; set; }

        public PlayerShip(Texture2D texture, Vector2 postion, int height, int width) : base(texture, postion, height, width)
        {
            timer = 0f;
            interval = 100f;
            Velocity = 4;
        }

        public override void Update(GameTime gameTime, ref int gameStart, List<GameObject> gameObjects)
        {
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            Movement(gameTime, game);

            BulletDeployment(game);
        }

        private void Movement(GameTime gameTime, MainGame game)
        {
            if (game.Inputs.IsKeyDown(Keys.Left))
            {
                game.State = GameState.Play;
                Position.X = Position.X - Velocity;
                spriteRect = new Rectangle((currentFrame * Width) - Width, 0, Width, Height);

                //Increase the timer by the number of milliseconds since update was last called
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                //Check the timer is more than the chosen interval
                if (timer > interval)
                {
                    //Show the next frame
                    currentFrame++;
                    //Reset the timer
                    timer = 0f;
                }
                //If we are on the last frame, reset back to the one before the first frame (because currentFrame++ is called next so the next frame will be 1!)
                if (currentFrame < 2 || currentFrame > 4)
                {
                    currentFrame = 2;
                }
            }
            else if (game.Inputs.IsKeyDown(Keys.Right))
            {
                game.State = GameState.Play;
                Position.X = Position.X + Velocity;
                spriteRect = new Rectangle((currentFrame * Width) - Width, 0, Width, Height);

                //Increase the timer by the number of milliseconds since update was last called
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                //Check the timer is more than the chosen interval
                if (timer > interval)
                {
                    //Show the next frame
                    currentFrame--;
                    //Reset the timer
                    timer = 0f;
                }
                //If we are on the last frame, reset back to the one before the first frame (because currentFrame++ is called next so the next frame will be 1!)
                if (currentFrame < 5)
                {
                    currentFrame = 7;
                }
            }
            else
            {
                spriteRect = new Rectangle((currentFrame * Width) - Width, 0, Width, Height);
                currentFrame = 1;
            }
        }

        private void BulletDeployment(MainGame game)
        {
            if (game.Inputs.IsKeyDown(Keys.Up))
            {
                game.State = GameState.Play;
                FireCount++;
                if (!HoldFile)
                {
                    HoldFile = true;
                    var newMissle = new PlayerMissle(game.Textures["PlayerMissle"], new Vector2(Position.X + 39, 388));
                    game.AddObject(newMissle);
                }
                HoldFile = true;
            }
        }

        public override void Destroy(MainGame game, GameObject sender)
        {
            game.SceneSwitch(0);

            base.Destroy(game, sender);
        }
    }
}
