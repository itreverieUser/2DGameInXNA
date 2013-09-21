using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace RubbishCollector.Util
{
    public static class Sound
    {
        #region Variables
        private static SoundEffect environmentSong;
        private static bool songstart = false;
        private static SoundEffect newRubbish;
        private static SoundEffect rubbishCollsion;
        private static SoundEffect ufoCollision;
        private static SoundEffect transitionScreen;
        private static SoundEffect gameOver;
        private static Random rand = new Random();
        #endregion


        #region Methods
        public static void Initialize(ContentManager content)
        {
            try
            {
                newRubbish = content.Load<SoundEffect>(@"Sound\wave");
                rubbishCollsion = content.Load<SoundEffect>(@"Sound\select");
                ufoCollision = content.Load<SoundEffect>(@"Sound\ufoGetsObj");
                environmentSong = content.Load<SoundEffect>(@"Sound\BackgoundSound");
                transitionScreen = content.Load<SoundEffect>(@"Sound\start");
                gameOver = content.Load<SoundEffect>(@"Sound\gameover");

                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.3f;
            }
            catch
            {
                Debug.Write("SoundManager Initialization Failed");
            }
        }

        public static void PlayNewRubbsih()
        {
            try
            {
                newRubbish.Play();
            }
            catch
            {
                Debug.Write("PlayPlayerShot Failed");
            }
        }

        public static void PlayRubbishCollsion()
        {
            try
            {
               rubbishCollsion.Play();
            }
            catch
            {
                Debug.Write("PlayEnemyShot Failed");
            }
        }

        public static void PlayUfoCollision()
        {
            try
            {
               ufoCollision.Play();
            }
            catch
            {
                Debug.Write("PlayPlayerShot Failed");
            }
        }

        public static void PlayTransitionScreen()
        {
            try
            {
               transitionScreen.Play();
            }
            catch
            {
                Debug.Write("PlayEnemyShot Failed");
            }
        }

        public static void GameOver()
        {
            try
            {
                gameOver.Play();
                
            }
            catch (Exception ex)
            {
                Debug.Write("PlayEnvironmentMusic Failed");
            }
        }

        public static void PlayEnvironmentMusic()
        {
            try
            {

                 environmentSong.Play();

                //if (!songstart)
                //{
                //    MediaPlayer.Play(environmentSong);
                //    songstart = true;
                //}  
            }
            catch (Exception ex)
            {
                Debug.Write("PlayEnvironmentMusic Failed");
            }
        }

        #endregion
    }
}
