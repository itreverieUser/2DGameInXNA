using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RubbishCollector.Background
{
    public class Background
    {
        #region
        private ContentManager _contentManager;
        private Texture2D _backgroundTexture;
        private Texture2D _backgroundTexture2;
        private Rectangle _background2Position;
        private readonly ScrollBackGround _scrollBackGround;
        private double millisecondsPerFrame = 50; //Update every xxx second
        private double timeSinceLastUpdate = 0; //Accumulate the elapsed time
        #endregion

        #region Constructor
        public Background(Texture2D texture, Rectangle source, Rectangle destination, ContentManager _ContentManager)
        {
            _backgroundTexture = texture;
            _scrollBackGround = new ScrollBackGround(_backgroundTexture, source, destination, 1, 2);
            _contentManager = _ContentManager;
        }
        #endregion

        #region Methods



        private void Loadimage()
        {
            _backgroundTexture2 = _contentManager.Load<Texture2D>(".\\Images\\Background");

        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate >= millisecondsPerFrame)
            {
                timeSinceLastUpdate = 0;
                _scrollBackGround.Update(gameTime);
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            Loadimage();

            _scrollBackGround.Draw(spriteBatch);
            // _spriteBatch.Begin();
            _background2Position = new Rectangle(0, 0, 800, 600);
            spriteBatch.Draw(_backgroundTexture2, _background2Position, Color.White);
            //_spriteBatch.End();

        }
        #endregion
    }


    public class ScrollBackGround
    {
        #region Variables
        Rectangle _screenRectangle;
        Rectangle _sourceRectangle;

        Rectangle _source1;
        Rectangle _source2;

        Rectangle _screen1;
        Rectangle _screen2;

        float _scrollV;
        float _scrollH;
        float scrollDelta;

        Texture2D texture;
        int direction; // 0=none 1=vertical 2=horizontal
        private Color colour;
        #endregion

        #region Constructors
        public ScrollBackGround()
        {
            scrollDelta = 1;
            colour = Color.White;
            Reset();
        }
        /// <summary>
        /// direction  0=none 1=vertical 2=horizontal
        /// </summary>
        /// <param name="backgroundTexture"></param>
        /// <param name="source"></param>
        /// <param name="screen"></param>
        /// <param name="delta"></param>
        /// <param name="directionZ"></param>
        public ScrollBackGround(Texture2D backgroundTexture, Rectangle source, Rectangle screen, float delta, int directionZ)
        {
            scrollDelta = delta;
            direction = directionZ;
            texture = backgroundTexture;
            _sourceRectangle = source;
            _screenRectangle = screen;
            colour = Color.White;
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch sb)
        {
            if (direction == 0) sb.Draw(texture, _screenRectangle, _sourceRectangle, Color.White);

            if (direction == 1) //Vertical scroll
            {
                int s = (int)(_sourceRectangle.Height - _scrollV);
                float ratio = _screenRectangle.Height / (float)_sourceRectangle.Height;

                _source1 = new Rectangle(_sourceRectangle.X, _sourceRectangle.Y, _sourceRectangle.Width, _sourceRectangle.Height - (int)_scrollV);
                _screen1 = new Rectangle(_screenRectangle.X, _screenRectangle.Y + (int)(_scrollV * ratio), _screenRectangle.Width, _screenRectangle.Height - (int)(_scrollV * ratio));

                _source2 = new Rectangle(_sourceRectangle.X, _sourceRectangle.Y + s, _sourceRectangle.Width, (int)_scrollV);
                _screen2 = new Rectangle(_screenRectangle.X, _screenRectangle.Y, _screenRectangle.Width, (int)(_scrollV * ratio));

                sb.Draw(texture, _screen1, _source1, colour);
                sb.Draw(texture, _screen2, _source2, colour);
            }

            if (direction == 2) //Horizontal scroll
            {
                //var s = (int)(_sourceRectangle.Width - _scrollH);
                //float ratio = _screenRectangle.Width / (float)_sourceRectangle.Width;

                //_source1 = new Rectangle(_sourceRectangle.X, _sourceRectangle.Y, _sourceRectangle.Width - (int)_scrollH, _sourceRectangle.Height);
                //_screen1 = new Rectangle(_screenRectangle.X + (int)(_scrollH * ratio), _screenRectangle.Y, _screenRectangle.Width - (int)(_scrollH * ratio), _screenRectangle.Height);

                //_source2 = new Rectangle(_sourceRectangle.X + s, _sourceRectangle.Y, (int)_scrollH, _sourceRectangle.Height);
                //_screen2 = new Rectangle(_screenRectangle.X, _screenRectangle.Y, (int)(_scrollH * ratio), _screenRectangle.Height);

                //sb.Draw(texture, _screen1, _source1, colour);
                //sb.Draw(texture, _screen2, _source2, colour);


                var s = (int)(_sourceRectangle.Width - _scrollH);
                float ratio = _screenRectangle.Width / (float)_sourceRectangle.Width;

                _source1 = new Rectangle(_sourceRectangle.X, _sourceRectangle.Y, _sourceRectangle.Width - (int)_scrollH, _sourceRectangle.Height);
                _screen1 = new Rectangle(_screenRectangle.X + (int)(_scrollH * ratio), _screenRectangle.Y, _screenRectangle.Width - (int)(_scrollH * ratio), _screenRectangle.Height);

                _source2 = new Rectangle(_sourceRectangle.X + s, _sourceRectangle.Y, (int)_scrollH, _sourceRectangle.Height);
                _screen2 = new Rectangle(_screenRectangle.X, _screenRectangle.Y, (int)(_scrollH * ratio), _screenRectangle.Height);

                sb.Draw(texture, _screen1, _source1, colour);
                sb.Draw(texture, _screen2, _source2, colour);
            }

        }

        public void Update(GameTime gameTime)
        {

            _scrollV = _scrollV + scrollDelta;
            if (_scrollV > _sourceRectangle.Height) _scrollV = 0;
            if (_scrollV < 0) _scrollV = _sourceRectangle.Height;

            _scrollH = _scrollH + scrollDelta;
            if (_scrollH > _sourceRectangle.Width) _scrollH = 0;
            if (_scrollH < 0) _scrollH = _sourceRectangle.Width;
            //YOUR GAMES LOGIC GOES HERE



        }

        public void Reset()
        {
            _scrollV = 0;
            _scrollH = 0;
        }

        #endregion
    }

}
