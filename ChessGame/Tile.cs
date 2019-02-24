using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessGame
{
    class Tile
    {
        public int Column, Row;
        public Texture2D texture;
        public Rectangle bound;
        public Vector2 position;
        public Tile rightTile, leftTile, topTile, bottomTile;
        public Boolean isOccupiedWhite, isSelected, isOccupiedBlack;
        public Pawns occupyingPawn;

        public Tile(Tile rightTile, Tile leftTile, Tile topTile, Tile bottomTile)
        {
            this.texture = texture;
            this.rightTile = rightTile;
            this.leftTile = leftTile;
            this.topTile = topTile;
            this.bottomTile = bottomTile;
            isSelected = false;
            isOccupiedWhite = false;
            isOccupiedBlack = false;
            occupyingPawn = null;
        }

        public void setTileNum(int Column, int Row)
        {
            this.Column = Column;
            this.Row = Row;
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }
        
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, bound, color);
        }
    }
}
