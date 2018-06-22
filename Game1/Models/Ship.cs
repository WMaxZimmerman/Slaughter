using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class Ship: GameObject
    {
        public bool HoldFile { get; set; }
        public int Velocity { get; set; }
        protected Rectangle spriteRect;
        protected int currentFrame;
        protected float timer = 0f;
        protected float interval = 100f;

        public Ship(Texture2D texture, Vector2 postion, int height, int width) : base(texture, postion)
        {
            Width = width;
            Height = height;
            spriteRect = new Rectangle((1 * width) - width, 0, width, height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, spriteRect, Color.White);
        }
    }
}
