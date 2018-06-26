using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1.Models
{
    public class Menu: GameObject
    {
        public Menu(Texture2D texture, Vector2 postion) : base(texture, postion)
        {
        }

        public override void Update(GameTime gameTime, MainGame game)
        {
            if (game.Inputs.IsKeyDown(Keys.Back))
            {
                game.SceneSwitch(0);
            }
        }
    }
}
