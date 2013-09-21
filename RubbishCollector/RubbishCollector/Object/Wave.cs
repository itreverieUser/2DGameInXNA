using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using RubbishCollector.Managers;

namespace RubbishCollector.Object
{
    public class Wave
    {

        #region WaveVariables
        //private CollisionManager _collisionManager = new CollisionManager();
        #endregion

        #region Properties
        public List<Ufo> ListUfo { get; set; }
        public List<Item> ListItem{ get; set; }
        public bool IsOnScreen { get; set; }
        public CollisionManager CollisionManager { get; set; }
        #endregion
        
        #region WaveMethods
        public void LoadWave(int noObjects, List<int> idType , ContentManager contentManager)
        {
            //Each wave is created with the same seed to generate random position and velocity
            var rnd = new Random();
            ListItem = new List<Item>();
            
            foreach (int type in idType)
            {
                for (int id = 1; id <= noObjects / (idType.Count); id++)
                {
                    var item = new Item();
                    item.LoadItem(rnd, id, type, contentManager);
                    ListItem.Add(item);
                }
            }
        }

        public void UpdateWave(GameTime gameTime)
        {
            if (ListItem.Count > 0)
            {
                for (int j = 0; j < ListItem.Count; j++)
                {
                    ListItem[j].UpdateItem(gameTime);


                    if (!ListItem[j].IsOnScreen())
                    {
                        ListItem.RemoveAt(j);
                    }
                    else
                    {
                        if (CollisionManager.CollisionMouseItem(ListItem[j].BoundingSphereItem, ListItem[j].ScoreValue))
                        {

                            ListItem.RemoveAt(j);
                        }
                    }

                }
            }
            else
            {
                ListItem = new List<Item>();
                IsOnScreen = false;
            }

        }

       

        public void DrawWave(SpriteBatch spriteBatch)
       {
           foreach (var item in ListItem)
            {
                item.DrawItem(spriteBatch);
            }
        }


        public void DestroyItem(Item item)
        {
            ListItem.Remove(item);
        }

       
        #endregion
    }
}
