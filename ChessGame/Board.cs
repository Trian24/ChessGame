using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessGame
{
    class Board
    {
        public Tile[] tiles = new Tile[64];
        int tileNumber;

        public void generateTiles()
        {
            tileNumber = 0;
            for(int r = 1; r <= 8; r++)
            {
                for(int c = 1; c <= 8;  c++)
                {
                    if (r == 1 && c == 1)  //kotak pertama
                    {
                        tiles[tileNumber] = new Tile(null, null, null, null);
                        tiles[tileNumber].setTileNum(r, c);
                        tiles[tileNumber].position = new Vector2(0, 0);
                        tiles[tileNumber].bound = new Rectangle((int)tiles[tileNumber].position.X, (int)tiles[tileNumber].position.Y, 75, 75);
                        Console.WriteLine("Created Tile " + r + ", " + c + " on index "+tileNumber);
                        tileNumber++;
                    }

                    else if(r == 1)  //baris pertama
                    {
                        tiles[tileNumber] = new Tile(null, tiles[tileNumber - 1], null, null);
                        tiles[tileNumber].leftTile.rightTile = tiles[tileNumber];
                        tiles[tileNumber].setTileNum(c, r);
                        tiles[tileNumber].position = new Vector2(tiles[tileNumber].leftTile.bound.Right, tiles[tileNumber].leftTile.bound.Top);
                        tiles[tileNumber].bound = new Rectangle((int)tiles[tileNumber].position.X, (int)tiles[tileNumber].position.Y, 75, 75);
                        Console.WriteLine("Created Tile " + r + ", " + c + " on index " + tileNumber);
                        tileNumber++;
                    }

                    else if(c == 1) //kolom pertama
                    {
                        tiles[tileNumber] = new Tile(null, null, tiles[tileNumber - 8], null);
                        tiles[tileNumber].topTile.bottomTile = tiles[tileNumber];
                        tiles[tileNumber].setTileNum(c, r);
                        tiles[tileNumber].position = new Vector2(tiles[tileNumber].topTile.bound.Left, tiles[tileNumber].topTile.bound.Bottom);
                        tiles[tileNumber].bound = new Rectangle((int)tiles[tileNumber].position.X, (int)tiles[tileNumber].position.Y, 75, 75);
                        Console.WriteLine("Created Tile " + r + ", " + c + " on index " + tileNumber +" with topTile "+tiles[tileNumber].topTile.Row+", "+tiles[tileNumber].topTile.Column);
                        tileNumber++;
                    }

                    else if(c != 1 || r!= 1) //sisa
                    {
                        tiles[tileNumber] = new Tile(null, tiles[tileNumber - 1], tiles[tileNumber - 8], null);
                        tiles[tileNumber].leftTile.rightTile = tiles[tileNumber];
                        tiles[tileNumber].topTile.bottomTile = tiles[tileNumber];
                        tiles[tileNumber].setTileNum(c, r);
                        tiles[tileNumber].position = new Vector2(tiles[tileNumber].leftTile.bound.Right, tiles[tileNumber].leftTile.bound.Top);
                        tiles[tileNumber].bound = new Rectangle((int)tiles[tileNumber].position.X, (int)tiles[tileNumber].position.Y, 75, 75);
                        Console.WriteLine("Created Tile " + r + ", " + c + " on index " + tileNumber + " with topTile " + tiles[tileNumber].topTile.Row + ", " + tiles[tileNumber].topTile.Column);
                        tileNumber++;
                    }
                }
            }
        }

        public void setTexture(Texture2D texture)
        {
            for(int i = 0; i < 64; i++)
            {
                tiles[i].setTexture(texture);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int count = 0;
            Color color;
            for(int x = 1; x <= 8; x++)
            {
                for(int y = 1; y <= 8; y++)
                {
                    color = Color.White;
                    if((x%2 == 0 && y%2 != 0) || (x%2 != 0 && y%2 == 0))
                    {
                    color = Color.Black;
                    }
                    tiles[count].Draw(spriteBatch, color);
                    count++;
                }
            }
        }
    }
}
