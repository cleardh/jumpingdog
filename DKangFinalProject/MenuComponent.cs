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
    /// menu component
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, highlightFont;
        private List<String> menuItems;

        public int SelectedIndex { get; set; } = 0;
        private Vector2 position;
        private Color regularColor = Color.Black;
        private Color highlightColor = Color.DarkMagenta;

        private KeyboardState oldState;
        /// <summary>
        /// menu component
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="regularFont">font for non-selected menu item</param>
        /// <param name="highlightFont">font for selected menu item</param>
        /// <param name="menus">array of menu items</param>
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, SpriteFont highlightFont, string[] menus) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            this.menuItems = menus.ToList<string>();

            this.position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
        }
        /// <summary>
        /// draw menu components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position; // it changes as selected menu item changes

            spriteBatch.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i], tempPos, highlightColor);
                    // LineSpacing holds value of the font's height
                    tempPos.Y += highlightFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// user navigate through menu items
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex >= menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex < 0)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }

            oldState = ks;

            base.Update(gameTime);
        }
    }
}
