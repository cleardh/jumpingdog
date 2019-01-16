/* Jumping Dog
 * DKangFinalProject
 * Revision History: 
 *      Dongha Kang, 2018-11-17: Started
 *      Dongha Kang, 2018-11-28: Restarted
 *      Dongha Kang, 2018-12-02: Finished
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace DKangFinalProject
{
    /// <summary>
    /// This is the main control center for the game.
    /// </summary>
    ///
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Background background;
        private SoundEffect menuSound;
        private SoundEffect optionChangeSound;
        private SoundEffect exitSound;
        private int oldIndex;


        private string filePath = @"scores.txt";

        // Declares all scenes here
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private HighScoreScene highScoreScene;
        private AboutScene aboutScene;
        // Scene declaration ends

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 364;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            System.Console.WriteLine(Shared.stage.X + " " + Shared.stage.Y);

            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D bgTex = this.Content.Load<Texture2D>("Images/background");
            background = new Background(this, spriteBatch, bgTex);
            this.Components.Add(background);

            // Instantiate all scenes here
            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);

            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            highScoreScene = new HighScoreScene(this, spriteBatch);
            this.Components.Add(highScoreScene);

            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);
            // Instantiation ends

            menuSound = this.Content.Load<SoundEffect>("Music/menu");
            optionChangeSound = this.Content.Load<SoundEffect>("Music/optionChange");
            exitSound = this.Content.Load<SoundEffect>("Music/pop");

            // Reading old scores
            try
            {
                StreamReader sr = new StreamReader(filePath);

                while (!sr.EndOfStream)
                {
                    try
                    {
                        string ranking = sr.ReadLine();
                        ranking = ranking.Substring(3);
                        HighScoreScene.scores.Add(int.Parse(ranking));
                    }
                    catch
                    {
                        continue;
                    }

                }
                sr.Close();
            }
            catch
            {
            }

            // Make only startScene active
            startScene.show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here

            //double timeAlive = gameTime.ElapsedGameTime.TotalSeconds;
            if (HighScoreScene.scores.Count > 0)
            {
                highScoreScene.Draw(gameTime);
            }

            KeyboardState ks = Keyboard.GetState();
            int selectedIndex = 0;

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;

                if (selectedIndex != oldIndex)
                {
                    optionChangeSound.Play();
                }
                oldIndex = selectedIndex;

                Song bgm = this.Content.Load<Song>("Music/bgm");
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.5f;

                //menu selection and corresponding actions
                switch (selectedIndex)
                {
                    case 0:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            menuSound.Play();
                            startScene.hide();
                            actionScene.show();
                            MediaPlayer.Play(bgm);
                        }
                        break;
                    case 1:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            menuSound.Play();
                            startScene.hide();
                            helpScene.show();
                        }
                        break;
                    case 2:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            menuSound.Play();
                            startScene.hide();
                            highScoreScene.show();
                        }
                        break;
                    case 3:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            menuSound.Play();
                            startScene.hide();
                            aboutScene.show();
                        }
                        break;
                    case 4:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            try
                            {
                                StreamWriter sw = File.CreateText(filePath);
                                sw.WriteLine("High Scores!");
                                if (HighScoreScene.scores.Count < 3)
                                {
                                    for (int i = 0; i < HighScoreScene.scores.Count; i++)
                                    {
                                        sw.WriteLine(i + 1 + ": " + HighScoreScene.scores[i]);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        sw.WriteLine(i + 1 + ": " + HighScoreScene.scores[i]);
                                    }
                                }
                                sw.Close();
                            }
                            catch
                            {
                            }
                            menuSound.Play();
                            Exit();
                        }
                        break;
                    default:
                        break;
                }
            }

            // Escaping to startScene
            if (actionScene.Enabled || helpScene.Enabled || highScoreScene.Enabled || aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    exitSound.Play();
                    actionScene.hide();
                    actionScene.InitializeGame();

                    helpScene.hide();
                    highScoreScene.hide();
                    aboutScene.hide();

                    startScene.show();
                    MediaPlayer.Stop();
                }
            }

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
