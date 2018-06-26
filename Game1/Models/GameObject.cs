﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using Game1.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Models
{
    public class GameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public int Layer { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool IsRemoved { get; set; }

        private Vector2 InitialPos { get; }

        public GameObject(Texture2D texture, Vector2 postion)
        {
            Texture = texture;
            Position = postion;
            InitialPos = postion;

            Height = texture.Height;
            Width = texture.Width;

            Layer = 1;
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

        public virtual void Update(GameTime gameTime, ref int gameStart, List<GameObject> gameObjects)
        {
            //Can Be Overriden to do stuff.
        }

        public virtual void Update(GameTime gameTime, MainGame game)
        {
            //Can Be Overriden to do stuff.
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public virtual void Destroy(MainGame game, GameObject sender)
        {
            IsRemoved = true;
        }
    }
}
