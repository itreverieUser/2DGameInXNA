using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RubbishCollector.Managers
{
    public class LevelManager
    {
        #region Variables
        private const int numberTotalLevels = 3;
        private  List<Level.Level> _listLevels;
        private Level.Level _currentLevel;
        #endregion

        #region Properties

        public Level.Level CurrentLevel
        {
            get { return _currentLevel; }
        }
        public List<Level.Level> ListLevels
        {
            get { return _listLevels; }
        }
        #endregion

        #region Constructor
        public LevelManager()
        {
            _listLevels = new List<Level.Level>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load the levels of the game
        /// </summary>
        public void InitializeLoadLevels(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            _listLevels.Clear();
            for (int i = 1; i <= numberTotalLevels; i++)
            {
                Level.Level level = new Level.Level();
                level.NumberLevel = i;
                level.IsActive = false;
                level.ScorePlayer = 0;
                level.ScoreUfo = 0;
                level.ScoreTotalPlayer = 0;
                level.ScoreTotalUfo = 0;
                level.Initialize(graphicsDevice, contentManager);
                level.LoadContent();

                _listLevels.Add(level);
            }
            _currentLevel = _listLevels[0];
            _currentLevel.IsActive = true;
        }

        /// <summary>
        /// Add a level to the current game
        /// </summary>
        public void GetNextLevel(int previousScorePlayer, int previousScoreUfo)
        {
            if (_listLevels.Count > 0)
            {
                _currentLevel = _listLevels[0];
                _currentLevel.IsActive = true;
                _currentLevel.ScorePlayer = previousScorePlayer;
                _currentLevel.ScoreUfo = previousScoreUfo;
            }

        }

        public void ResetCurrentLevel()
        {
            _currentLevel.IsActive = true;
            _currentLevel.ScoreTotalPlayer = 0;
            _currentLevel.ScorePlayer = 0;
            _currentLevel.ScoreTotalUfo = 0;
            _currentLevel.LoadContent();
            _currentLevel.LoadWaves();
        }


        /// <summary>
        /// Remove a Level
        /// </summary>
        public void RemoveLevel()
        {
            if (_listLevels.Count > 0)
            {
                _listLevels.RemoveAt(0);
            }
        }



        /// <summary>
        /// Clear levels
        /// </summary>
        public void ClearLevel()
        {
            _listLevels.Clear();
        }

        #endregion
    }
}
