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
    /// StartScene Creates menu components
    /// </summary>
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        private SpriteBatch spriteBatch;
        private string[] menuItems =
            {
            "Start Game",
            "Help",
            "High Score",
            "About",
            "Quit"
        };
        /// <summary>
        /// creating menu component using highlight and regular fonts
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        public StartScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/regularFont");
            SpriteFont highlightFont = game.Content.Load<SpriteFont>("Fonts/highlightFont");

            Menu = new MenuComponent(game, spriteBatch, regularFont, highlightFont, menuItems);
            this.Components.Add(Menu);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
