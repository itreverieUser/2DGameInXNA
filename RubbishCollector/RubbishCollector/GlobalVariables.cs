using System.Configuration;
using System;
using Microsoft.Xna.Framework;

namespace RubbishCollector
{
    public static class GlobalVariables
    {
        /// <summary>
        /// Path of the images files
        /// </summary>
        /// <remarks>Can not change because it is not constant</remarks>
        public static readonly string Dir = ConfigurationManager.AppSettings.Get(0);

        /// <summary>
        /// Height of the window
        /// </summary>
        public static readonly Int32 TopHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get(1));

        /// <summary>
        /// Button of the window
        /// </summary>
        public static readonly Int32 ButtomHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get(2));

        /// <summary>
        /// Right of the window 
        /// </summary>
        public static readonly Int32 RightHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get(3));


        /// <summary>
        /// Left of the window
        /// </summary>
        public static readonly Int32 LeftHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get(4));

        //<summary>
        //Extra space aund the window to make the objects dissapear smothly
        //</summary>
        public static readonly Int32 Padding = Convert.ToInt32(ConfigurationManager.AppSettings.Get(5));

        /// <summary>
        /// Gravity
        /// </summary>
        public static readonly Int32 Gravity = Convert.ToInt32(ConfigurationManager.AppSettings.Get(6));

        /// <summary>
        /// Total of levels
        /// </summary>
        public static readonly Int32 Levels = Convert.ToInt32(ConfigurationManager.AppSettings.Get(7));

        /// <summary>
        /// Width of the window
        /// </summary>
        public const int Width = 800;

        /// <summary>
        /// Height of the window
        /// </summary>
        public const int Height = 600;

        /// <summary>
        /// BoundingBox that contains the window plus a padding to simulate that the objects appear smoothly to the screen
        /// </summary>
        //static Vector3 windowMin= new Vector3(-Padding, -Padding, 0);
        //static Vector3 windowMax= new Vector3(Width + Padding, Height + Padding,0);
        //public static BoundingBox BoundingWindow = new BoundingBox(windowMin, windowMax);


        public static Rectangle BoundingWindow = new Rectangle(-Padding, -Padding, Width + Padding, Height + Padding);
       

    }

}
