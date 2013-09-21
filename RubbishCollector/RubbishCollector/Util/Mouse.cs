using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RubbishCollector.Util
{
    public static class MouseObject
    {
        #region Variables
        private static MouseState _previousMouseState, _currentMousestate;
        private static Vector3 _position;
        private static Texture2D _texture;
        private static Rectangle _mouseSphere;
        private static Rectangle _rectangle;
        #endregion


        #region Properties
        public static Texture2D Texture { get; set; }
        public static Vector2 Position
        { 
        
            get{
                
                return new Vector2(_position.X, _position.Y);
            }

            set{
                var temp = value;
                _position = new Vector3(temp.X, temp.Y, 0);

            }
        }
        public static Rectangle MouseSphere
        {

            get
            {

                return _mouseSphere;
            }

            set
            {
                _mouseSphere = value;

            }
        }

        #endregion


        
        #region  Left button
        public static bool LeftClick
        {
            get { return _currentMousestate.LeftButton == ButtonState.Pressed; }
        }

        public static bool NewLeftClick
        {
            get { return LeftClick && _previousMouseState.LeftButton == ButtonState.Released; }
        }

        public static bool HoldLeft
        {
            get { return LeftClick && _previousMouseState.LeftButton == ButtonState.Pressed; }
        }

        public static bool LeftRelease
        {
            get { return !LeftClick && _previousMouseState.LeftButton == ButtonState.Pressed; }
        }
        #endregion

        #region Right button

        public static bool RightClick
        {
            get { return _currentMousestate.RightButton == ButtonState.Pressed; }
        }

        public static bool RightRelease
        {
            get { return !RightClick && _previousMouseState.RightButton == ButtonState.Pressed; }
        }
        #endregion


        #region Methods

        public static void ResetMouse()
        {
            _position = new Vector3(0, 0, 0);
            _mouseSphere = new Rectangle(0,0,0,0);
        }

        public static void Load(ContentManager content)
        {
            _texture = content.Load<Texture2D>(".\\Images\\cursor");
        }

        public static void Update()
        {
            _previousMouseState = _currentMousestate;
            _currentMousestate = Mouse.GetState(); 
            _position = new Vector3( _currentMousestate.X, _currentMousestate.Y,0);
            _mouseSphere = new Rectangle(_currentMousestate.X,_currentMousestate.Y,40,40 );

            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);

            
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(_texture, _rectangle, Color.White);
                spriteBatch.End();
            }
        }
        #endregion
    }
}
