using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace Monogame_2___Lists_and_Loops
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont catFont, instructionFont;
        Rectangle window;
        Vector2 textRect, instructions, enter;
        Texture2D titleScreen, mainScreen;
        Random generator = new Random();
        List<Texture2D> CatTextures = new List<Texture2D>();
        List<Rectangle> CatRects = new List<Rectangle>();
        int scrollCount;
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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            window = new Rectangle(0, 0, 1000, 800);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();
            textRect = new Vector2(50, 100);
            enter = new Vector2(50, 200);
            for (int i = 1; i < 7; i++)
                CatTextures.Add(Content.Load<Texture2D>("Cat" + i));
            instructionFont = Content.Load<>
            catFont = Content.Load<SpriteFont>("CatFont");
            titleScreen = Content.Load<Texture2D>("CatTitle");
            mainScreen = Content.Load<Texture2D>("CatBackground");
            for (int j = 1; j < scrollCount; j++)
                CatRects.Add
                    (
                        new Rectangle(generator.Next(window.Width - 25), generator.Next(window.Height - 25), generator.Next(0, 51), generator.Next(0, 101))
                    );
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleScreen, window, Color.White);
                _spriteBatch.DrawString(catFont, "Christian MacDonald", textRect, Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
