using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Models
{
    public class PlayerShip: Ship
    {
        public PlayerShip(Texture2D texture, Vector2 postion, int height, int width) : base(texture, postion, height, width)
        {
            timer = 0f;
            interval = 100f;
            Velocity = 4;
        }

        public override void Update(GameTime gameTime, ref int gameStart)
        {
            var Move = Keyboard.GetState();
            if (Move.IsKeyDown(Keys.Left))
            {
                gameStart = 1;
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
            else if (Move.IsKeyDown(Keys.Right))
            {
                gameStart = 1;
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
    }
}
