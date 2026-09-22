using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using System.Threading;

namespace Monogame_2___Lists_and_Loops
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont catFont, instructionFont;
        int catCount;
        KeyboardState keyboardState;
        MouseState mouseState;
        Rectangle window;
        Vector2 textRect, instructions, enter, score;
        Texture2D titleScreen, mainScreen;
        Random generator = new Random();
        List<Texture2D> CatTextures = new List<Texture2D>();
        List<Texture2D> SpawnedCats = new List<Texture2D>();
        List<Rectangle> CatRects = new List<Rectangle>();
        int scrollCount, oldScroll;
        enum Screen
        {
            Title, Main
        }
        private Screen screen;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
            screen = Screen.Title;
            oldScroll = Mouse.GetState().ScrollWheelValue;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            window = new Rectangle(0, 0, 1000, 800);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();
            textRect = new Vector2(50, 100);
            enter = new Vector2(260, 220);
            score = new Vector2(600, 10);
            for (int i = 1; i < 7; i++)
                CatTextures.Add(Content.Load<Texture2D>("Cat" + i));
            instructionFont = Content.Load<SpriteFont>("instructionText");
            catFont = Content.Load<SpriteFont>("CatFont");
            titleScreen = Content.Load<Texture2D>("CatTitle");
            mainScreen = Content.Load<Texture2D>("CatBackground");
            for (int i = 0; i < CatRects.Count; i++)
            {
                CatTextures.Add(CatTextures[generator.Next(CatTextures.Count)]);
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
             if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Main;
                }
            }
            if (screen == Screen.Main)
            {
                int currentScroll = mouseState.ScrollWheelValue;

                if (currentScroll > oldScroll)
                {
                    CatRects.Add(
                        new Rectangle(generator.Next(window.Width - 100), generator.Next(window.Height - 100), generator.Next(0, 51), generator.Next(0, 51))
                    );

                    SpawnedCats.Add(
                        CatTextures[generator.Next(CatTextures.Count)]
                    );

                    scrollCount++;
                }
                else if (currentScroll < oldScroll)
                {
                    if (CatRects.Count > 0)
                    {
                        CatRects.RemoveAt(CatRects.Count - 1);
                        SpawnedCats.RemoveAt(SpawnedCats.Count - 1);

                        scrollCount--;
                    }
                }
                oldScroll = currentScroll;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            mouseState = Mouse.GetState();
            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleScreen, window, Color.White);
                _spriteBatch.DrawString(catFont, "Christian MacDonald", textRect, Color.White);
                _spriteBatch.DrawString(catFont, "Press ENTER", enter, Color.White);
                _spriteBatch.DrawString(catFont, "to continue", new Vector2(enter.X, enter.Y + 120), Color.White);
            }
            if (screen == Screen.Main)
            {
                _spriteBatch.Draw(mainScreen, window, Color.White);
                _spriteBatch.DrawString(instructionFont, "Scroll to add Cats!!!", textRect, Color.Pink);
                _spriteBatch.DrawString(instructionFont, "Score: " + scrollCount, score, Color.Pink);
                for (int i = 0; i < CatRects.Count; i++)
                {
                    _spriteBatch.Draw(SpawnedCats[i], CatRects[i], null, Color.White, (float)generator.Next(0, 7), new Vector2(0, 0), (SpriteEffects)generator.Next(3), 1f);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
