using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RubbishCollector.Util
{

    public static class Text 
    {
        #region Variables
        private static  GraphicsDevice _graphicsDevice;
        private static  ContentManager _contentManager;
        private static SpriteBatch _spriteBatch;
        private static SpriteFont _spriteFont;
        private static Color _fontColor;

        #endregion
 
        #region Methods

        public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;

            _spriteFont = _contentManager.Load<SpriteFont>("Font\\StarFont");
            _fontColor = new Color(255,192,0);

        }

        public static void Draw(string text, Vector2 position)
        {
          
               _spriteBatch = new SpriteBatch(_graphicsDevice);
               _spriteBatch.Begin();
               _spriteBatch.DrawString(_spriteFont, text, position, _fontColor);
               _spriteBatch.End();
          
        }

        #endregion
    }

}
