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
    /// bone class
    /// </summary>
    public class Bone : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position;
        public Vector2 speed;

        private float rotation;
        private float rotationChange = 3;

        private Random r = new Random();
        private const int MIN_SPEED = 4;
        private const int MAX_SPEED = 7;

        /// <summary>
        /// bone constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex">bone image</param>
        /// <param name="position">bone position</param>
        public Bone(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;

            Random r = new Random();
            int x = r.Next(MIN_SPEED, MAX_SPEED);

            this.speed = new Vector2(x, 0);
            this.position = position;
        }
        /// <summary>
        /// initialize bone to the very right side of the screen
        /// </summary>
        public void InitializeBone()
        {
            this.position.X = Shared.stage.X;
        }
        /// <summary>
        /// drawing bone with rotation
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, new Rectangle(0, 0, tex.Width, tex.Height), Color.White, rotation, new Vector2(tex.Width / 2, tex.Height / 2), 1f, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// if bone touches the dog or the left side of the screen, sends the bone to the very right
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            rotation += rotationChange;

            position.X -= speed.X;

            if (position.X < -tex.Width)
            {
                float xVar = r.Next(0, 10);
                position.X = Shared.stage.X + xVar;
            }

            if (this.Visible == false)
            {
                this.Visible = true;
                float xVar = r.Next(0, 10);
                position.X = Shared.stage.X + xVar;
            }           

            base.Update(gameTime);
        }
        /// <summary>
        /// for keeping track of scores in collision manager class
        /// </summary>
        /// <returns></returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
