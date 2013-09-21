using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RubbishCollector.Util;

namespace RubbishCollector.Windows
{
    public class StartScreen
    {

        #region Variables
        private KeyboardState _keyState;
        private KeyboardState _prevKeyState;
        private SpriteBatch _spriteBatch;
        private Texture2D _windowsBackground;
        private bool start = false;
        private bool scoreboard = false;
        private bool Level = false;
        #endregion

        #region Methods

        /// <summary>
        /// Load textures to be rendered on the level
        /// </summary>
        public virtual void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            //_graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _windowsBackground = contentManager.Load<Texture2D>(".\\Images\\startScreen");
            Text.LoadContent(graphicsDevice, contentManager);
            MouseObject.Load(contentManager);
            //Sound.PlayTransitionScreen();
        }

        ///// <summary>
        ///// Updates the objects to be rendered on the level
        ///// </summary>
        ///// <param name="gameTime">GameTime</param>
        public virtual void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            MouseObject.Update();

           

            if (_keyState.IsKeyDown(Keys.S))
            {
            //    start

            }
            if (_keyState.IsKeyDown(Keys.B))
            {


            }
            if (_keyState.IsKeyDown(Keys.L))
            {


            }
            // if (_keyState.IsKeyDown(Keys.Escape)) Exit();

            Rectangle startWindow = new Rectangle(0, 0, 800, 600);


        }

        /// <summary>
        /// Renders the objects on the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public virtual void Draw(GameTime gameTime)
        {
            Rectangle startWindow = new Rectangle(0, 0, 800, 600);

            _spriteBatch.Begin();
            //Text.Text.Draw(gameTime, "Start", new Vector2(300, 300));
            _spriteBatch.Draw(_windowsBackground, startWindow, Color.White);


            _spriteBatch.End();
            // Text.Text.Draw(gameTime, "Start", new Vector2(300, 300));
            // MouseObject.MouseObject.Draw(_spriteBatch);

        }

        public bool isInside(Rectangle rectangle)
        {

            return false;
        }

        #endregion

    }
}
