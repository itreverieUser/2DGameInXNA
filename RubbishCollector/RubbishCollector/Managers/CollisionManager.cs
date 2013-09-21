using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RubbishCollector.Util;
using RubbishCollector.Object;

namespace RubbishCollector.Managers
{
    public class CollisionManager
    {
        #region Variables
        private List<Item> _listItem;
        private bool _isPlayer;
        #endregion

        #region Properties
        public int ScorePlayer { set; get; }
        public int ScoreUfo { set; get; }
        public bool IsPlayer { set; get; }
        #endregion

        #region Constructor
        public CollisionManager()
        {
        }

        public CollisionManager(List<Item> listItem, bool isPlayer)
        {
            _listItem = listItem;
            _isPlayer = isPlayer;
            ScorePlayer = 0;
            ScoreUfo = 0;
        }
        #endregion

        #region Methods

        public bool CollisionMouseItem(Rectangle bsItem, int itemValue)
        {
            bool isCollide = (MouseObject.LeftClick && bsItem.Intersects(MouseObject.MouseSphere));
            if (isCollide)
            {
                Sound.PlayRubbishCollsion();
                ScorePlayer += itemValue;
            }

            return isCollide;
        }

        

        public bool CollisionUfo(Rectangle bsItem, Rectangle ufo, int itemValue)
        {
            bool isCollide = false;

            isCollide = (bsItem.Intersects(ufo));

            if ((ufo.X < GlobalVariables.Width - 10) && (ufo.Y < GlobalVariables.Height - 50))
            {
                if (isCollide)
                {
                    Sound.PlayUfoCollision();
                    ScoreUfo += itemValue;
                }


            }

            return isCollide;
        }

        #endregion
    }
}
