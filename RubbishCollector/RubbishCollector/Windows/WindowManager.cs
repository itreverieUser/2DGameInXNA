using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RubbishCollector.Managers;
using RubbishCollector.Util;

namespace RubbishCollector.Windows
{
    public class WindowManager
    {
        enum GameStates { StartScreen, Playing, GameOver, Pause, Score, Transition, Help };

        #region Variables
        private GameStates _gameState;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _contentManager;
        private readonly StartScreen _startScreen;
        private readonly LevelManager _levelManager;
        private readonly Help.Help _help;
        private readonly Gameover.Gameover _gameover;
        private readonly Transition.transition _transition;
        private KeyboardState _keyState;
        private KeyboardState _prevKeyState;
        private double millisecondsPerFrame = 400; //Update every xxx second
        private double timeSinceLastUpdate = 0; //Accumulate the elapsed time
        private SpriteBatch _spriteBatch;
        private float timeForCurrentFrame = 0.0f;
        private bool localScore=true;
        private List<int> _listScorePlayers= new List<int>();
       
        #endregion

        #region Properties
        public int TotalScore { set; get; }
        public int TotalScoreUfo { set; get; }
        public bool exit = false;
        #endregion

        #region Constructor

        public WindowManager()
        {
            _startScreen = new StartScreen();

            _levelManager = new LevelManager();
            //_levelManager.CreateLevels();BRENDA

            _help = new Help.Help();
            _gameover = new Gameover.Gameover();
            _transition = new Transition.transition();

            //Set what is the first Window
            _gameState = GameStates.StartScreen;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load textures to be rendered on the level
        /// </summary>
        public virtual void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _graphicsDevice = graphicsDevice;
            _contentManager = contentManager;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _startScreen.LoadContent(graphicsDevice, contentManager);
            _help.LoadContent(graphicsDevice, contentManager);
            _gameover.LoadContent(graphicsDevice, contentManager);
            _transition.LoadContent(graphicsDevice, contentManager);
            _levelManager.InitializeLoadLevels(graphicsDevice, contentManager);
            _levelManager.GetNextLevel(0, 0);
        }

        public virtual void Update(GameTime gameTime)
        {
            _keyState = Keyboard.GetState();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastUpdate += 50;
            switch (_gameState)
            {
                case GameStates.Help:
                    if (_keyState.IsKeyDown(Keys.H))
                    {
                        if (timeSinceLastUpdate >= millisecondsPerFrame)
                        {
                            timeSinceLastUpdate = 0;
                            _gameState = GameStates.Playing;
                        }
                    }
                    break;
                case GameStates.StartScreen:
                    localScore = true;
                    _startScreen.Update(gameTime);
                    if (_keyState.IsKeyDown(Keys.E))
                    {
                        exit = true;
                    }
                    if (_keyState.IsKeyDown(Keys.S))
                    {
                        _gameState = GameStates.Playing;
                        if (_levelManager.ListLevels != null)
                        {
                            if (_levelManager.ListLevels.Count < 1)
                            {

                                _levelManager.InitializeLoadLevels(_graphicsDevice, _contentManager);
                            }
                        }
                    }
                    break;
                case GameStates.Playing:

                    if (_keyState.IsKeyDown(Keys.E))
                    {
                        exit = true;
                    }
                    if (_keyState.IsKeyDown(Keys.H))
                    {
                        if (timeSinceLastUpdate >= millisecondsPerFrame)
                        {
                            timeSinceLastUpdate = 0;
                            _gameState = GameStates.Help;
                        }
                    }

                    if (_keyState.IsKeyDown(Keys.Back))
                    {
                        _levelManager.InitializeLoadLevels(_graphicsDevice, _contentManager);
                        _gameState = GameStates.StartScreen;
                        break;
                    }

                    //If the current level is active
                    if (_levelManager.CurrentLevel.IsActive)
                    {
                        //Update
                        _levelManager.CurrentLevel.Update(gameTime);
                    }
                    else
                    {
                        //When removing the level we have to keep track of the score 
                        //If they exceed certain score, go to next level 

                        if (_levelManager.CurrentLevel.IsFinished())
                        {
                            _levelManager.RemoveLevel();

                            if (_levelManager.CurrentLevel.NumberLevel >= 3)
                            {
                                //if (_levelManager.CurrentLevel.ScoreTotalPlayer > 0)
                                //{
                                    _listScorePlayers.Add(_levelManager.CurrentLevel.ScoreTotalPlayer);

                                //}
                                _levelManager.ListLevels.Clear();
                                _gameState = GameStates.GameOver;
                            }
                            else
                            {

                                _gameState = GameStates.Transition;
                                TotalScore = _levelManager.CurrentLevel.ScoreTotalPlayer;
                                TotalScoreUfo = _levelManager.CurrentLevel.ScoreTotalUfo;
                                _levelManager.GetNextLevel(_levelManager.CurrentLevel.ScoreTotalPlayer, _levelManager.CurrentLevel.ScoreTotalUfo);

                            }
                        }
                        //But if they do not reach the score minimum
                        else
                        {
                            TotalScore = _levelManager.CurrentLevel.ScoreTotalPlayer;
                            TotalScoreUfo = _levelManager.CurrentLevel.ScoreTotalUfo;

                            _listScorePlayers.Add(_levelManager.CurrentLevel.ScoreTotalPlayer);
                           
                  
                            _levelManager.ListLevels.Clear();
                            _gameState = GameStates.GameOver;
                        }
                    }
                    break;
                case GameStates.GameOver:
                    if (localScore)
                    {
                      //  _listScorePlayers.Add(TotalScore);
                        _gameover._listScorePlayers = _listScorePlayers;
                        localScore = false;
                    }
                    if (_keyState.IsKeyDown(Keys.Enter))
                    {
                        _gameState = GameStates.StartScreen;
                       // Sound.GameOver();
                        _levelManager.ListLevels.Clear();
                        _levelManager.InitializeLoadLevels(_graphicsDevice, _contentManager);
                    }
                    break;
                case GameStates.Transition:
                    
                    if (_keyState.IsKeyDown(Keys.Enter))
                    {
                        _gameState = GameStates.Playing;
                    }
                    break;
                default:
                    break;
            }


        }

        /// <summary>
        /// Renders the objects on the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public virtual void Draw(GameTime gameTime)
        {
            switch (_gameState)
            {
                case GameStates.Help:
                    _help.Draw(gameTime);
                    break;
                case GameStates.StartScreen:
                    _startScreen.Draw(gameTime);
                    break;
                case GameStates.Playing:
                    _levelManager.CurrentLevel.Draw(gameTime);
                    break;
                case GameStates.GameOver:
                    _gameover.Draw(gameTime);
                    break;
                case GameStates.Transition:
                    _transition.Draw(gameTime);
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
