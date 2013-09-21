using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RubbishCollector.Managers;
using RubbishCollector.Object;
using RubbishCollector.Util;

namespace RubbishCollector.Level
{
    public class Level
    {

        #region PrivateVariables
        private CollisionManager _collisionManager = new CollisionManager();
        private GraphicsDevice _graphicsDevice;
        private ContentManager _contentManager;
        private KeyboardState _keyState;
        private SpriteBatch _spriteBatch;
        private bool _pause;
        //private bool help = false;
        private double millisecondsPerFrame = 400; //Update every xxx second
        private double timeSinceLastUpdate = 0; //Accumulate the elapsed time
        private Wave _currentWave;
        private List<Wave> _listWave;
        private List<Ufo> _listUfo;
        private Background.Background _background;
        private Texture2D _backgroundTexture;
        private int _initialNumberOfWaves = 1;
        private int _initialNumberOfObjectsInWave = 10;

        private Texture2D _textureImage1;
        private Texture2D _textureImage2;
        private Texture2D _textureImage3;
        #endregion

        #region Properties
        public int NumberLevel { set; get; }
        public bool IsActive { set; get; }
        public List<Ufo> ListUfo
        {
            set { _listUfo = value; }
            get { return _listUfo; }
        }
        public List<Wave> ListWave
        {
            set { _listWave = value; }
            get { return _listWave; }
        }
        public Wave CurrentWave
        {
            set { _currentWave = value; }
            get { return _currentWave; }
        }

        public int ScorePlayer { get; set; }
        public int ScoreUfo { get; set; }

        public int ScoreTotalPlayer { get; set; }
        public int ScoreTotalUfo { get; set; }
        
        #endregion

        #region Methods
        
        /// <summary>
        /// Initializa the level 
        /// </summary>
        /// <param name="g">Graphics Device object</param>
        /// <param name="contentManager">Content Manager</param>
        public  void Initialize(GraphicsDevice g, ContentManager contentManager)
        {
            _graphicsDevice = g;
            _contentManager = contentManager;
            _spriteBatch = new SpriteBatch(g);
        }

        /// <summary>
        /// Load textures to be rendered on the level
        /// </summary>
        public void LoadContent()
        {
            LoadBackground();
            LoadLevelText();
            LoadWaves();
            LoadMenuOptionsTop();
            if (CurrentWave.ListItem.Count >0){LoadUfo();}
            MouseObject.Load(_contentManager);
        }

     
        /// <summary>
        /// Updates the objects to be rendered on the level
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public  void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                _keyState = Keyboard.GetState();
                timeSinceLastUpdate += 50;
                if (_keyState.IsKeyDown(Keys.P))
                {
                    if (timeSinceLastUpdate >= millisecondsPerFrame)
                    {
                        timeSinceLastUpdate = 0;
                        _pause = (_pause == false) ? true : false;
                    }
                }

                if (!_pause)
                {
                    _background.Update(gameTime);
                    MouseObject.Update();
                    UpdateUfo(gameTime);
                    if (IsActive)
                    {
                       
                        UpdateWaves(gameTime);

                    }
                }
            }
        }

        /// <summary>
        /// Renders the objects on the screen
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public  void Draw(GameTime gameTime)
        {
                MouseObject.Draw(_spriteBatch);

                _spriteBatch.Begin();
                _background.Draw(_spriteBatch);
               
                DrawWaves(_spriteBatch);
                DrawUfos(_spriteBatch);

                _spriteBatch.Draw(_textureImage1, new Rectangle(0, 565, _textureImage1.Width, _textureImage1.Height), Color.White);
                _spriteBatch.Draw(_textureImage2, new Rectangle(0, 0, _textureImage2.Width, _textureImage2.Height), Color.White);
                _spriteBatch.Draw(_textureImage3, new Rectangle(500, 0, _textureImage3.Width, _textureImage3.Height), Color.White);
                _spriteBatch.End();

                Text.Draw(NumberLevel+"", new Vector2( 160,3));
                Text.Draw("" + (ScorePlayer + _collisionManager.ScorePlayer) +" / "+ (ScoreUfo + _collisionManager.ScoreUfo), new Vector2(145, 22));
               // Text.Draw("" + (ScoreUfo + _collisionManager.ScoreUfo), new Vector2(160, 22));
            
        }

        public bool IsFinished()
        {
            int scorePlayer = ScorePlayer + _collisionManager.ScorePlayer;
            int scoreUfo = ScoreUfo + _collisionManager.ScoreUfo;


                ScorePlayer = ScoreTotalPlayer + scorePlayer;
                ScoreUfo = ScoreTotalUfo + scoreUfo;
                ScoreTotalPlayer = ScorePlayer;
                ScoreTotalUfo = ScoreUfo;

            if (scorePlayer > scoreUfo)
            {
                return true;
            }
            return false;
        }


        #endregion
        
        #region LoadingTextures


        private void LoadMenuOptionsTop()
        {
            _textureImage1 = _contentManager.Load<Texture2D>(@".\Images\AssistanceButtons\bottom");
            _textureImage2 = _contentManager.Load<Texture2D>(@".\Images\AssistanceButtons\left");
            _textureImage3 = _contentManager.Load<Texture2D>(@".\Images\AssistanceButtons\right");
        }

        /// <summary>
        /// Load background image of the game
        /// </summary>
        private void LoadBackground()
        {
            _backgroundTexture = _contentManager.Load<Texture2D>(".\\Images\\Backgroundv");
            var source = new Rectangle(0, 0, GlobalVariables.Width, GlobalVariables.Height);
            var destination = new Rectangle(0, 0, GlobalVariables.Width, GlobalVariables.Height);
            _background = new Background.Background(_backgroundTexture, source, destination, _contentManager);
        }
        
        /// <summary>
        /// Loads the text to set the Level
        /// </summary>
        private void LoadLevelText()
        {
            Text.LoadContent(_graphicsDevice, _contentManager);
        }


        #endregion

        #region WaveMethods

        public void LoadWaves()
        {
            //We have three type of rubbish
            List<int> type = new List<int>();
            type.Add(1);
            type.Add(2);
            type.Add(3);

            _listWave= new List<Wave>();
            
            int numberWaves = _initialNumberOfWaves + (2 * NumberLevel);
            int numberObjectInWaves = _initialNumberOfObjectsInWave + (2 * NumberLevel);

            
            for (int i = 0; i < numberWaves; i++)
            {
                Wave wave = new Wave();
                wave.CollisionManager = _collisionManager;
                wave.CollisionManager.ScorePlayer = 0;
                wave.CollisionManager.ScoreUfo = 0;
                wave.LoadWave(numberObjectInWaves+i, type, _contentManager);
                _listWave.Add(wave);
            }

            _currentWave = _listWave[0];
            _currentWave.IsOnScreen = true;
        }

        private void UpdateWaves(GameTime gameTime)
        {
            if (_listWave.Count > 0)
            {
                _currentWave.UpdateWave(gameTime);
                if (!_currentWave.IsOnScreen)
                {
                    DestroyWave();
                    
                    if (_listWave.Count > 0)
                    {
                        Sound.PlayNewRubbsih();
                        _currentWave = _listWave[0];
                        _currentWave.IsOnScreen = true;
                    }
                }
            }
            else
            {
                _listWave = new List<Wave>();
                IsActive = false;
            }
            
            
        }

        private void DrawWaves(SpriteBatch spriteBatch)
        {
            _currentWave.DrawWave(spriteBatch);
        }

        private void DestroyWave()
        {

            _listWave.RemoveAt(0);

        }
        
        #endregion

        #region UfoMethods

        public void LoadUfo()
        {
            _listUfo = new List<Ufo>();
            int numberUfo = NumberLevel;
            
            for (int i = 0; i < numberUfo; i++)
            {
                Ufo ufo = new Ufo(CurrentWave.ListItem[_r.Next(CurrentWave.ListItem.Count)], new Vector2(i * 100+50, i * 200+50));
                ufo.LoadUfo(_contentManager);
                _listUfo.Add(ufo);
            }
        }

        static Random _r = new Random();

        private Item NearestTarget(List<Item> ItemList, Ufo ufo)
        {
            int n = _r.Next(ItemList.Count);

            return ItemList[n];
        }

        private void UpdateUfo(GameTime gameTime)
        {
            if (_currentWave != null)
            {
                foreach (var ufo in ListUfo)
                {
                    ufo.UpdateUfo(gameTime);
                    //bool update = false;
                    if(ufo.Target == null)
                    {
                        if (_currentWave.ListItem.Count > 0)
                        {
                            ufo.Target = NearestTarget(CurrentWave.ListItem, ufo);
                        }
                    }
                    if (ufo.Target != null)
                    {
                        if (CurrentWave.CollisionManager.CollisionUfo(ufo.Target.BoundingSphereItem, ufo.RectangleUfo, ufo.Target.ScoreValue))
                        {
                            if (_currentWave.ListItem.Count > 0)
                            {
                                CurrentWave.ListItem.Remove(ufo.Target);
                                ufo.Target = null;
                            }
                        }
                        if (ufo.Target != null)
                        {
                            if (ufo.Target.Position.Y > 600)
                            {
                                ufo.Target = null;
                            }
                        }
                    }
                }
            }

        }

        private void DrawUfos(SpriteBatch spriteBatch)
        {
            foreach (var ufo in ListUfo)
            {
                ufo.DrawUfo(spriteBatch);
            }
        }
        
        #endregion

    }
}
