using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class GameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        private Vector2 InitialPos { get; }

        public GameObject(Texture2D texture, Vector2 postion)
        {
            Texture = texture;
            Position = postion;
            InitialPos = postion;
        }

        public void MoveToInitialPos()
        {
            Position = InitialPos;
        }
    }
}
