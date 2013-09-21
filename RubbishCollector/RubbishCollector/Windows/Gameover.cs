using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RubbishCollector.Util;

namespace RubbishCollector.Gameover
{
    public class Gameover
    {
        #region Variables
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;
        private Texture2D _windowsBackground;
        #endregion

        #region properties
        public List<int> _listScorePlayers = new List<int>();
        #endregion

        #region Methods

        /// <summary>
        /// Load textures to be rendered on the level
        /// </summary>
        public virtual void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _windowsBackground = contentManager.Load<Texture2D>(".\\Images\\gameover");
            Text.LoadContent(graphicsDevice, contentManager);

            //Sound.GameOver();
        }

        /// <summary>
        /// Updates the objects to be rendered on the level
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public  virtual void Update(GameTime gameTime)
        {

           
        }

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
            Text.Draw( "Press Enter to contiue!", new Vector2(300, 500));
            // _listScorePlayers 
            //Text.Draw(_listScorePlayers., new Vector2(300, 500));

            var sortedDoubles = from d in _listScorePlayers 
        orderby d descending
        select d;

            int i = 0;

                //for (int i = 0; i < _listScorePlayers.Count; i++)
                //{
            Text.Draw("Top 10 High score table", new Vector2(300, i * 100 + 100));
            foreach (int sortedDouble in sortedDoubles)
            {
              
           // }
                if (i<10)
                {  Text.Draw("Player "+i+" :    "+(int)sortedDouble, new Vector2(300, i*100+200));}
                i++;

            }


        }

        #endregion


    }
}
