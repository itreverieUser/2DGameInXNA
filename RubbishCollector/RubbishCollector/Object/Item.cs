using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using RubbishCollector.Util;


namespace RubbishCollector.Object
{
    public class Item : Sprite
    {
        #region Variables
        private readonly Vector2 _gravity = new Vector2(0, 3);
        private float _xvalue;
        private float _yvalue;
        private Rectangle _boundingSphereItem;
        private Texture2D _textureBox;
        #endregion

        #region Properties
        public int Id { get; set; }
        public int IdType { get; set; }
        public int ScoreValue { get; set; }
        public Rectangle BoundingSphereItem
        {
            get { return _boundingSphereItem; }
        }
        #endregion
        
        #region Methods

        public void LoadItem(Random rndY, int id, int typeId, ContentManager contentManager)
        {
            Id = id;
            IdType = typeId;
            TextureSprite = contentManager.Load<Texture2D>("Images/RubbishObjects/" + GetTypeIdContainer(typeId) + "/" + id);//If they send me the texture I do not need , ContentManager _contentManager
            Origin = new Vector2((float)TextureSprite.Width / 2, (float)TextureSprite.Height / 2);
            CreateSprite(TextureSprite, Position, Velocity, Rotation, Origin);
            Through(rndY);

            _xvalue = TextureSprite.Width;
            _yvalue = TextureSprite.Height;
            Math.Max(_xvalue, _yvalue);

            _boundingSphereItem = new Rectangle(0, 0, (int)_xvalue, (int)_yvalue);
            _textureBox = contentManager.Load<Texture2D>("Images/AssistanceButtons/bottom");

        }
        
        public void UpdateItem(GameTime gameTime)
        {
                var elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;
                Velocity = Velocity + _gravity;
                Position += Velocity*elapsed;
                Rotation += Rotation*elapsed;

            _boundingSphereItem.X = (int)Position.X;
            _boundingSphereItem.Y = (int) Position.Y;
        }
        

        public void DrawItem(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureSprite, Position, new Rectangle(0, 0, TextureSprite.Width, TextureSprite.Height), Color.White, (float)Rotation, Origin, 1, SpriteEffects.None, 0);
           // spriteBatch.Draw(_textureBox, Position, new Rectangle(0, 0, _textureBox.Width, _textureBox.Height), Color.GreenYellow, (float)Rotation, Origin, 1, SpriteEffects.None, 0);
        }
        
        #endregion
        
        #region PrivateMethods

        private string GetTypeIdContainer(int  typeContainer)
        {
            string typeText;
            switch (typeContainer)
            {
                case 1:
                    typeText = "Organic";
                    ScoreValue = 10; 
                    break;
                case 2:
                    typeText = "Plastic";
                    ScoreValue = 10;
                    break;
                case 3:
                    typeText = "Steel";
                    ScoreValue = 10; 
                    break;
                case 4:
                    typeText = "Bomb";
                    ScoreValue = -10; 
                    break;
                default:
                    typeText = "";
                    break;
            }
            return typeText;
        }
        public bool IsOnScreen()
        {

            return _boundingSphereItem.Intersects(GlobalVariables.BoundingWindow); 
        }

        #endregion
    }
}
