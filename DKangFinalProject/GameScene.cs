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
    /// game scene
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components; // any child object of GameComponent can be added here
        public List<GameComponent> Components { get => components; set => components = value; }

        /// <summary>
        /// enable and make current game scene component visible
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        /// <summary>
        /// disable and make current game scene component invisible
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        /// <summary>
        /// game scene constructor
        /// </summary>
        /// <param name="game"></param>
        public GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            hide();
        }
        /// <summary>
        /// fire draw of currently visible component
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }
        /// <summary>
        /// fire update of currently enabled component
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
    }
}
