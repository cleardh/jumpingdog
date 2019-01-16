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
    /// Dog class
    /// </summary>
    public class Dog : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 velocity;
        private bool hasJumped;
        private SoundEffect jumpSound;

        private const float INIT_X = 50;
        private const float INIT_Y = 300;
        private const float HORIZONTAL_MOVE = 2;

        private const float POSITION_CHANGE = 20F;
        private const float INIT_VELOCITY = -8F;
        private const float STOP_VELOCITY = 0F;
        private const float VELOCITY_CHANGE = 0.15F;
        private const int LANDING_POSITION = 320;


        /// <summary>
        /// dog constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex">dog image</param>
        /// <param name="position">dog position</param>
        public Dog(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.hasJumped = true;
            this.jumpSound = game.Content.Load<SoundEffect>("Music/jump");
        }
        /// <summary>
        /// put dog in the initial position
        /// </summary>
        public void InitializeDog()
        {
            this.position = new Vector2(INIT_X, INIT_Y);
        }
        /// <summary>
        /// drawing dog
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// dog's jumping motion
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position.Y += velocity.Y;

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up) && !hasJumped)
            {
                position.Y -= POSITION_CHANGE;
                velocity.Y = INIT_VELOCITY;
                hasJumped = true;
                jumpSound.Play(0.1f, 0f, 0f);
            }

            if (hasJumped)
            {
                velocity.Y += VELOCITY_CHANGE;
            }

            if (position.Y + tex.Height >= LANDING_POSITION)
            {
                hasJumped = false;
            }

            if (!hasJumped)
            {
                velocity.Y = STOP_VELOCITY;
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= HORIZONTAL_MOVE;
                if (position.X < 0)
                {
                    position.X = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += HORIZONTAL_MOVE;
                if (position.X > Shared.stage.X - tex.Width)
                {
                    position.X = Shared.stage.X - tex.Width;
                }
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
