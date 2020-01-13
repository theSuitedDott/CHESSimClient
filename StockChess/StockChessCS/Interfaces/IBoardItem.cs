using StockChessCS.Enums;

namespace StockChessCS.Interfaces
{
    public interface IBoardItem
    {
        int Rank { get; set; }
        char File { get; set; }
        ChessBoardItem ItemType { get; set; }
    }
}
