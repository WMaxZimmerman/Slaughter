using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class GameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public int Height { get; set; }
        public int Width { get; set; }

        private Vector2 InitialPos { get; }

        public GameObject(Texture2D texture, Vector2 postion)
        {
            Texture = texture;
            Position = postion;
            InitialPos = postion;

            Height = texture.Height;
            Width = texture.Width;
        }

        public void MoveToInitialPos()
        {
            Position = InitialPos;
        }

        public bool CollidesWith(GameObject obj)
        {
            var objRect = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, obj.Width, obj.Height);
            var thisRect = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            return thisRect.Intersects(objRect);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
