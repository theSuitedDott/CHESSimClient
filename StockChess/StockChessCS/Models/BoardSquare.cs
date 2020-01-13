using StockChessCS.Interfaces;
using StockChessCS.Enums;

namespace StockChessCS.Models
{
    public class BoardSquare : IBoardItem
    {
        public int Rank { get; set; }
        public char File { get; set; }
        public ChessBoardItem ItemType { get; set; }
    }
}
