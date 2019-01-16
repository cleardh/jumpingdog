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
using Microsoft.Xna.Framework.Audio;

namespace DKangFinalProject
{
    /// <summary>
    /// Collision manager class
    /// </summary>
    public class CollisionManager : GameComponent
    {
        private Dog dog;
        private Bone bone1;
        private Bone bone2;
        private Bone bone3;
        private SoundEffect barkSound;
        public static int score = 0;
        /// <summary>
        /// collision manager constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="dog">dog</param>
        /// <param name="bone1">1st bone</param>
        /// <param name="bone2">2nd bone</param>
        /// <param name="bone3">3rd bone</param>
        public CollisionManager(Game game, Dog dog, Bone bone1, Bone bone2, Bone bone3) : base(game)
        {
            this.dog = dog;
            this.bone1 = bone1;
            this.bone2 = bone2;
            this.bone3 = bone3;
            this.barkSound = game.Content.Load<SoundEffect>("Music/bark");
        }
        /// <summary>
        /// initialize score to 0 and add the score to scores list
        /// </summary>
        public void InitializeScore()
        {
            HighScoreScene.scores.Add(score);
            score = 0;
        }
        /// <summary>
        /// when dog intersects with bone, score goes up
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Rectangle dogRect = dog.GetBounds();
            Rectangle bone1Rect = bone1.GetBounds();
            Rectangle bone2Rect = bone2.GetBounds();
            Rectangle bone3Rect = bone3.GetBounds();

            if (dogRect.Intersects(bone1Rect))
            {
                bone1.Visible = false;
                score++;
                barkSound.Play();
            }
            if (dogRect.Intersects(bone2Rect))
            {
                bone2.Visible = false;
                score++;
                barkSound.Play();
            }
            if (dogRect.Intersects(bone3Rect))
            {
                bone3.Visible = false;
                score++;
                barkSound.Play();
            }

            base.Update(gameTime);
        }
    }
}
