using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RubbishCollector.Util;
using RubbishCollector.Windows;


namespace RubbishCollector
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameRubbishCollector : Game
    {
        #region Variables
        private readonly GraphicsDeviceManager _graphics;
        private readonly WindowManager _windowManager;
        #endregion

        #region Constructor

        public GameRubbishCollector()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";
            Window.Title = "Rubbish Collector";
            _windowManager= new WindowManager();
           
          
           
        }

        #endregion

        #region Methods
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Components.Add(new Text.Text(this)); //TODO Check how does the drawable component works
            base.Initialize();
            IsMouseVisible = true;
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Sound.Initialize(Content);
            Sound.PlayEnvironmentMusic();
            _windowManager.LoadContent(GraphicsDevice, Content);
        }

    
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape) || _windowManager.exit)
            {
                Exit();
            }
            _windowManager.Update(gameTime);
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           _windowManager.Draw(gameTime);
           base.Draw(gameTime);
        }
        #endregion


    }
}
