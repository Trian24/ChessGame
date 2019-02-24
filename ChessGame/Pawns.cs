using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChessGame
{
    class Pawns
    {
        Texture2D texture;
        public Tile currentTile, previousTile;
        public enum PawnTypes {King, Queen, Pawn, Bishop, Rook, Knight};
        PawnTypes pawnType;
        public enum PawnColor { Black, White};
        PawnColor pawnColor;
        Board board;
        Pawns[] pawn;
        public Rectangle bound;
        public Vector2 position;
        Boolean isDeleted;

        public Texture2D Texture { get => texture; set => texture = value; }

        public Pawns(Texture2D texture, Tile currentTile, PawnTypes pawnType, PawnColor pawnColor)
        {
            this.texture = texture;
            this.currentTile = currentTile;
            previousTile = null;
            position = this.currentTile.position;
            bound = new Rectangle((int)position.X + 5, (int)position.Y + 5, 65, 65);
            this.pawnType = pawnType;
            this.pawnColor = pawnColor;
            isDeleted = false;
        }

        public void Move(Tile selectedTile)
        {
            switch (pawnType)
            {
                case (PawnTypes.Pawn):
                    {
                        if (this.pawnColor == PawnColor.White)
                        {
                            if (selectedTile == currentTile.bottomTile)
                            {
                                previousTile = currentTile;
                                currentTile = selectedTile;
                                previousTile.isOccupiedWhite = false;
                                previousTile.occupyingPawn = null;
                            }
                            else if((selectedTile == currentTile.bottomTile.leftTile || selectedTile == currentTile.bottomTile.rightTile) && (selectedTile.isOccupiedBlack == true))
                            {
                                selectedTile.occupyingPawn.Delete();
                                previousTile = currentTile;
                                currentTile = selectedTile;
                                previousTile.isOccupiedWhite = false;
                                previousTile.occupyingPawn = null;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Error choosing destination tile");
                            }
                        }
                        else
                        {
                            if (selectedTile == currentTile.bottomTile && currentTile.bottomTile.isOccupiedBlack == false)
                            {
                                previousTile = currentTile;
                                currentTile = selectedTile;
                                previousTile.isOccupiedBlack = false;
                                previousTile.occupyingPawn = null;
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Error choosing destination tile");
                            }
                        }
                        break;
                    }
            }
        }

        public void Delete()
        {
            isDeleted = true;
        }

        public void Update()
        {
            if (isDeleted == false)
            {
                position.X = currentTile.bound.X + 5;
                position.Y = currentTile.bound.Y + 5;
                bound.X = (int)position.X;
                bound.Y = (int)position.Y;
                this.currentTile.occupyingPawn = this;
                this.currentTile.isOccupiedWhite = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isDeleted == false)
            {
                spriteBatch.Draw(texture, bound, Color.White);
            }
        }
    }
}
