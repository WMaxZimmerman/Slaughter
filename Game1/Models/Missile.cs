using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class Missile: GameObject
    {
        protected int Velocity = 6;

        public Missile(Texture2D texture, Vector2 postion) : base(texture, postion)
        {
        }
    }
}
