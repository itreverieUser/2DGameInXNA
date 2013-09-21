using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RubbishCollector.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace RubbishCollector.Object
{
   public class Ufo 
    {
        #region Variables
        public Vector2 _ufoPosition;
        private Vector2 _ufoDirection;
        private Vector2 _ufoSpeed = new Vector2(20,20);
        public Rectangle RectangleUfo;
        private Texture2D _texture;
       //private float 

        public Item Target;
        #endregion
        
        #region Constructors

        /// <summary>
        /// Ufo auto chace given item.
        /// </summary>
        /// <param name="targetItem">what to chace</param>

        public Ufo(Item item, Vector2 ufoPosition)
        {
            _ufoPosition = ufoPosition;
            Target = item;
        }       
        #endregion

        #region Methods

        public void LoadUfo(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>("Images/ufo");
            RectangleUfo = new Rectangle((int)_ufoPosition.X, (int)_ufoPosition.Y, _texture.Width, _texture.Height);
        }

        public void UpdateUfo(GameTime gameTime)
        {
                if (Target != null)
                {
                    float distanceX = Target.Position.X - _ufoPosition.X;
                    float distanceY = Target.Position.Y - _ufoPosition.Y;

                    if (distanceX != 0 && distanceY != 0)
                    {
                        _ufoDirection.X = Math.Abs(distanceX)/distanceX;
                        _ufoDirection.Y = Math.Abs(distanceY)/distanceY;
                    }

                    //To increase the difficulty of the game, you could increase the speed at which the Ufo catches the rubbish
                    //for example: 30 -> faster; 60 -> slower
                    _ufoPosition.X += _ufoSpeed.X*_ufoDirection.X *((float) gameTime.ElapsedGameTime.TotalMilliseconds / 30);
                    _ufoPosition.Y += _ufoSpeed.Y*_ufoDirection.Y *((float)gameTime.ElapsedGameTime.TotalMilliseconds / 30);
                    RectangleUfo.X = (int)_ufoPosition.X;
                    RectangleUfo.Y = (int)_ufoPosition.Y;
                }
                
         //   }
        }

        public void DrawUfo(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, RectangleUfo, Color.White);
        }

        #endregion
    }
}
