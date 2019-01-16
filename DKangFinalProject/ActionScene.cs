/* Jumping Dog
 * DKangFinalProject
 * Revision History: 
 *      Dongha Kang, 2018-11-17: Started
 *      Dongha Kang, 2018-11-28: Restarted
 *      Dongha Kang, 2018-12-02: Finished
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace DKangFinalProject
{
    /// <summary>
    /// action scene
    /// </summary>
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Bone bone1;
        private Bone bone2;
        private Bone bone3;
        private Dog dog;
        private CollisionManager cm;
        private Score score;
        private GameOver gameOver;

        private SpriteFont timeFont;
        private Vector2 timePosition;

        private bool isStartOfGame = false;
        private bool isPaused = false;

        private double totalTimePlayed = 0;
        private double oldTimePlayed = 0;
        private string timePlayed = "";

        private const int MIN_X = 0;
        private const int MAX_X = 300;
        private const int MIN_GAP = 40;
        private const int FIRST_MIN_Y = 70;
        private const int FIRST_MAX_Y = 120;
        private const int SECOND_MIN_Y = 140;
        private const int SECOND_MAX_Y = 180;
        private const int THIRD_MIN_Y = 200;
        private const int THIRD_MAX_Y = 260;

        private const int MIN_SPEED = 4;
        private const int MAX_SPEED = 6;

        private const int INIT_DOG_X = 50;
        private const int INIT_DOG_Y = 300;

        private const int SCORE_X = 480;
        private const int SCORE_Y = 325;

        private const int TIME_X = 400;
        private const int TIME_Y = 30;

        private const double TIME_LIMIT = 30;

        private bool isGameOver = false;

        Random r = new Random();

        /// <summary>
        /// action scene constructor where all the components' instantiation takes place
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;

            Texture2D boneTex = game.Content.Load<Texture2D>("Images/bone");
            bone1 = new Bone(game, spriteBatch, boneTex, InitializeBonePosition(bone1));
            this.Components.Add(bone1);

            bone2 = new Bone(game, spriteBatch, boneTex, InitializeBonePosition(bone2));
            this.Components.Add(bone2);

            bone3 = new Bone(game, spriteBatch, boneTex, InitializeBonePosition(bone3));
            this.Components.Add(bone3);

            Texture2D dogTex = game.Content.Load<Texture2D>("Images/dog");
            Vector2 dogPosition = new Vector2(INIT_DOG_X, INIT_DOG_Y);
            dog = new Dog(game, spriteBatch, dogTex, dogPosition);
            this.Components.Add(dog);

            cm = new CollisionManager(game, dog, bone1, bone2, bone3);
            this.Components.Add(cm);

            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/scoreFont");
            string message = CollisionManager.score.ToString();
            Vector2 position = new Vector2(SCORE_X, SCORE_Y);
            score = new Score(game, spriteBatch, font, message, position, Color.DarkRed);
            this.Components.Add(score);

            gameOver = new GameOver(game, spriteBatch);
            this.Components.Add(gameOver);

            timePosition = new Vector2(TIME_X, TIME_Y);
            timeFont = game.Content.Load<SpriteFont>("Fonts/timeFont");
        }

        private Vector2 InitializeBonePosition(Bone bone)
        {
            int xVar;
            int yValue;

            if (bone == bone1)
            {
                xVar = r.Next(MIN_X, (MAX_X * 1) / 3);
                yValue = r.Next(FIRST_MIN_Y, FIRST_MAX_Y);
            }
            else if (bone == bone2)
            {
                xVar = r.Next((MAX_X * 2) / 3, MAX_X);
                yValue = r.Next(SECOND_MIN_Y, SECOND_MAX_Y);
            }
            else
            {
                xVar = MIN_X;
                yValue = r.Next(THIRD_MIN_Y, THIRD_MAX_Y);
            }

            return new Vector2(Shared.stage.X + xVar, yValue);
        }

        /// <summary>
        /// initialize game by calling initialize methods from all the components
        /// </summary>
        public void InitializeGame()
        {
            dog.InitializeDog();
            bone1.InitializeBone();
            bone2.InitializeBone();
            bone3.InitializeBone();
            bone1.position = InitializeBonePosition(bone1);
            bone2.position = InitializeBonePosition(bone2);
            bone3.position = InitializeBonePosition(bone3);
            cm.InitializeScore();
            gameOver.Enabled = false;
            gameOver.Visible = false;
            isStartOfGame = true;
            isGameOver = false;
        }
        /// <summary>
        /// drawing ticking time
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(timeFont, timePlayed, timePosition, Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// keep track of score changes and time passing
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            score.message = CollisionManager.score.ToString();

            KeyboardState ks = Keyboard.GetState();

            totalTimePlayed = gameTime.TotalGameTime.TotalSeconds;

            if (!isPaused)
            {
                if (!isStartOfGame)
                {
                    if (totalTimePlayed - oldTimePlayed > TIME_LIMIT)
                    {
                        isGameOver = true;
                        oldTimePlayed = totalTimePlayed;
                    }

                    if (isGameOver)
                    {
                        ResetGame(false);
                        timePlayed = "30:000000";

                        if (ks.IsKeyDown(Keys.Space))
                        {
                            InitializeGame();
                            ResetGame(true);
                            isGameOver = false;
                        }
                    }
                    else
                    {
                        ResetGame(true);
                        timePlayed = (totalTimePlayed - oldTimePlayed).ToString();

                        if (ks.IsKeyDown(Keys.LeftControl))
                        {
                            isPaused = true;
                            dog.Enabled = false;
                            bone1.Enabled = false;
                            bone2.Enabled = false;
                            bone3.Enabled = false;
                        }
                    }
                }
                else
                {
                    isStartOfGame = false;
                    oldTimePlayed = totalTimePlayed;
                }

                //set bone positon until it finds the appropriate heights for all the bones
                do
                {
                    SetBonePosition();
                } while (bone3.position.Y - bone2.position.Y < MIN_GAP || bone2.position.Y - bone1.position.Y < MIN_GAP); 
            }
            else
            {
                if (ks.IsKeyDown(Keys.LeftAlt))
                {
                    isPaused = false;
                    dog.Enabled = true;
                    bone1.Enabled = true;
                    bone2.Enabled = true;
                    bone3.Enabled = true;
                }
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// randomly set bone position
        /// </summary>
        private void SetBonePosition()
        {
            if (bone1.position.X == Shared.stage.X)
            {
                bone1.position.Y = r.Next(FIRST_MIN_Y, FIRST_MAX_Y);
                bone1.speed.X = r.Next(MIN_SPEED, MAX_SPEED);
            }
            if (bone2.position.X == Shared.stage.X)
            {
                bone2.position.Y = r.Next(SECOND_MIN_Y, SECOND_MAX_Y);
                bone2.speed.X = r.Next(MIN_SPEED, MAX_SPEED);
            }
            if (bone3.position.X == Shared.stage.X)
            {
                bone3.position.Y = r.Next(THIRD_MIN_Y, THIRD_MAX_Y);
                bone3.speed.X = r.Next(MIN_SPEED, MAX_SPEED);
            }
        }
        /// <summary>
        /// either reset game by enabling dog and bone or otherwise
        /// </summary>
        /// <param name="yes"></param>
        private void ResetGame(bool yes)
        {
            dog.Enabled = yes;
            bone1.Enabled = yes;
            bone2.Enabled = yes;
            bone3.Enabled = yes;
            score.Enabled = yes;

            gameOver.Enabled = !yes;
            gameOver.Visible = !yes;
        }
    }
}