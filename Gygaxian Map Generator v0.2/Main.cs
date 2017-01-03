using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gygaxian_Map_Generator_v0._2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int cellSize = 10;
        int cols, rows;
        Cell[,] Cells;
        Walker walker;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            cols = Convert.ToInt32(Math.Floor(Convert.ToDouble(graphics.PreferredBackBufferWidth / cellSize)));
            rows = Convert.ToInt32(Math.Floor(Convert.ToDouble(graphics.PreferredBackBufferHeight / cellSize)));
            Cells = new Cell[cols, rows];
            this.IsMouseVisible = true;
            walker = new Walker();          
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
            for(int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    Cells[col,row] = new Cell(new Vector2(col, row), cellSize, GraphicsDevice);
                }
            }
            Debug.Print("Load Complete.");

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Cell c in Cells)
                {
                    if (c.Rectangle.Contains(mouseState.Position))
                    {
                        //set Current Cell
                        if (!walker.Started)
                        {
                            walker.Start(c);
                        }
                        else
                        {
                            Window.Title = c.Terrain;
                        }
                    }
                }
            }

            // TODO: Add your update logic here
            walker.Update(Cells, cols, rows);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            //// TODO: Add your drawing code here
            foreach (Cell c in Cells)
            {
                spriteBatch.Draw(c.Texture, c.Position);
            }
           
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
