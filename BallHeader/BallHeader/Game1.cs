using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BallHeader
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            /*
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            */
            GameElements.currentState = GameElements.State.Menu;
            GameElements.Initialize();
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
            GameElements.LoadContent(Content, Window);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            GameElements.UnloadSave();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            switch (GameElements.currentState)
            {
                case GameElements.State.Run:
                    GameElements.currentState = GameElements.RunUpdate(Content, Window, gameTime);
                    break;

                case GameElements.State.HightScore:
                    GameElements.currentState = GameElements.HighScoreUpdate(gameTime, Window, Content);
                    break;

                case GameElements.State.Quit:
                    this.Exit();
                    break;

                default:
                    GameElements.currentState = GameElements.MenuUpdate();
                    break;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);

            spriteBatch.Begin();
            switch (GameElements.currentState)
            {
                case GameElements.State.Run:
                    GameElements.RunDraw(spriteBatch, Window);
                    break;

                case GameElements.State.HightScore:
                    GameElements.HighScoreDraw(spriteBatch);
                    break;

                case GameElements.State.Quit:
                    this.Exit();
                    break;

                default:
                    GameElements.MenuDraw(spriteBatch);
                    break;
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
