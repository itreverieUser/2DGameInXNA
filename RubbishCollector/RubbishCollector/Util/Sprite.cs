using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RubbishCollector.Util
{
    public class Sprite
    {

        #region Properties
        public Texture2D TextureSprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public double Rotation { get; set; }
        public Vector2 Origin { get; set; }
        #endregion


        #region Methods
        public void ResetSprite()
        {
          
            Position = new Vector2(0,0);
            Velocity = new Vector2(0,0);
            Rotation = 0;
            Origin = new Vector2(0,0);
        
        }

        public void CreateSprite(Texture2D texture, Vector2 position, Vector2 velocity, double rotation, Vector2 origin)
        {
            TextureSprite = texture;
            Position = position;
            Velocity = velocity;
            Rotation = rotation;
            Origin = origin;
        }

        /// <summary>
        /// Method to simulates a throwing object 
        /// </summary>
        /// <param name="rndY"></param>
        public void Through(Random rndY)
        {
            int velocityY = rndY.Next(300, 400);
            //If you want to change the velocity to through the objects change the velocity X and Y values in a range of 400,400
            var velocity = new Vector2(0, -velocityY);

            //Randonmly creates the X location for the complete window from (0-800)
            int locationX = rndY.Next(0, 800);

            //Creates the random value of velocity in X 
            //We knw that 80 is teh value that does not exit the window
            int velocityX = rndY.Next(0, 80);
            if (locationX < 600)//400
            {
                //Creates the random value of velocity in X 
                //We knw that 80 is teh value that does not exit the window
                Velocity = new Vector2(velocityX, velocity.Y);
            }
            else
            {
                Velocity = new Vector2(-velocityX, velocity.Y);
            }
            Position = new Vector2(locationX, 500);
            
            Rotation = (rndY.Next(1, 400) - 200) * 0.001;
        }
        #endregion
    }
}
