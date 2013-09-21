using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RubbishCollector.Util;

namespace RubbishCollector.Transition
{
   public class transition
    {
        #region Variables
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private Texture2D _windowsBackground;
        #endregion

        #region Methods

        /// <summary>
        /// Load textures to be rendered on the level
        /// </summary>
        public virtual void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _windowsBackground = contentManager.Load<Texture2D>(".\\Images\\transition");
            Text.LoadContent(graphicsDevice, contentManager);
        }

        ///// <summary>
        ///// Updates the objects to be rendered on the level
        ///// </summary>
        ///// <param name="gameTime">GameTime</param>
        //public new virtual void Update(GameTime gameTime)
        //{


        //}

        /// <summary>
        /// Renders the objects on the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public virtual void Draw(GameTime gameTime)
        {
            Rectangle startWindow = new Rectangle(0, 0, 800, 600);


            _spriteBatch.Begin();
            _spriteBatch.Draw(_windowsBackground, startWindow, Color.White);
            _spriteBatch.End();
            Text.Draw("Congratulation You have beaten the UFO ", new Vector2(300, 200));
            Text.Draw( "Press Enter to contiue  to next level", new Vector2(300, 300));

        }

        #endregion


    }
}


