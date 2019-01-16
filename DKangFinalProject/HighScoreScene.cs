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

namespace DKangFinalProject
{
    /// <summary>
    /// highscore scene
    /// </summary>
    public class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private SpriteFont highScoreFont;

        private const float INIT_X = 300;
        private const float INIT_Y = 100;
        private const float GAP = 50;

        public static List<int> scores = new List<int>();
        /// <summary>
        /// highscore scene constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public HighScoreScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = game.Content.Load<Texture2D>("Images/highScore");
            this.highScoreFont = game.Content.Load<SpriteFont>("Fonts/highScoreFont");
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// drawing highscore scene that shows all the score in the list in the descending order
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(tex, Vector2.Zero, Color.White);

            try
            {
                scores.Sort();
                scores.Reverse();

                if (scores.Count > 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 pos = new Vector2(INIT_X, INIT_Y + GAP * i);
                        if (scores[i] != 0)
                        {
                            spriteBatch.DrawString(highScoreFont, scores[i].ToString(), pos, Color.Black);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < scores.Count; i++)
                    {
                        Vector2 pos = new Vector2(INIT_X, INIT_Y + GAP * i);
                        if (scores[i] != 0)
                        {
                            spriteBatch.DrawString(highScoreFont, scores[i].ToString(), pos, Color.Black);
                        }
                    }
                }
            }
            catch
            {
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
