using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChessGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Board board;
        Texture2D tileTexture, pawnTexture, rookTexture, bishopTexture, queenTexture, kingTexture, knightTexture;
        Pawns[] pawnWhite =  new Pawns[8];
        Pawns pawn2;
        MouseState oldState, newState;
        Tile newSelectedTile, oldSelectedTile;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            board = new Board();
            board.generateTiles();
            oldState = Mouse.GetState();
            newSelectedTile = null;
            oldSelectedTile = null;
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
            tileTexture = Content.Load<Texture2D>("Tile");
            pawnTexture = Content.Load<Texture2D>("Pawn");
            board.setTexture(tileTexture);
            Console.WriteLine("Tile " + board.tiles[5].position);
            for (int x = 0; x < 8; x++)
            {
                pawnWhite[x] = new Pawns(pawnTexture, board.tiles[x+8], Pawns.PawnTypes.Pawn, Pawns.PawnColor.White);
                Console.WriteLine("created pawn on tile " + board.tiles[x+8].position);
            }
            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            newState = Mouse.GetState();
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                Rectangle mouseClick = new Rectangle(newState.X, newState.Y, 1, 1);
                System.Diagnostics.Debug.WriteLine(newState.X.ToString() +
                               "," + newState.Y.ToString());
                for(int x = 0; x<64; x++)
                {
                    if(mouseClick.Intersects(board.tiles[x].bound))
                    {
                        Console.WriteLine("Tile clicked " + board.tiles[x].Row + ", " + board.tiles[x].Column+" with bottomTile "+board.tiles[x].bottomTile.Row+", " + board.tiles[x].bottomTile.Column);
                        if(board.tiles[x].isOccupiedWhite)
                        {
                            Console.WriteLine("With pawn "+ board.tiles[x].occupyingPawn.position);
                            oldSelectedTile = board.tiles[x];
                            Console.WriteLine("Tile " + board.tiles[x].Row + ", " + board.tiles[x].Column + " is selected");
                        }
                        else if(oldSelectedTile != null)
                        {
                            newSelectedTile = board.tiles[x];
                            Console.WriteLine("oldSelected Tile "+oldSelectedTile.Row+", "+oldSelectedTile.Column+" newSelected tile " + newSelectedTile.Row + ", " + newSelectedTile.Column);
                            oldSelectedTile.occupyingPawn.Move(newSelectedTile);
                            oldSelectedTile = null;
                            newSelectedTile = null;
                        }
                    }
                }
            }
            oldState = newState;

            for (int x = 0; x < 8; x++)
            {
                pawnWhite[x].Update();
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
            spriteBatch.Begin();

            board.Draw(spriteBatch);
            for (int x = 0; x < 8; x++)
            {
                pawnWhite[x].Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
