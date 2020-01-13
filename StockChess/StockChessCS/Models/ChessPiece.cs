using StockChessCS.Enums;
using StockChessCS.Interfaces;
using StockChessCS.ViewModels;

namespace StockChessCS.Models
{
    public class ChessPiece : ViewModelBase, IBoardItem
    {

        private PieceColor _color;
        public PieceColor Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        private PieceType _piece;
        public PieceType Piece
        {
            get { return _piece; }
            set
            {
                _piece = value;
                OnPropertyChanged();
            }
        }

        private int _rank;
        public int Rank
        {
            get { return _rank; }
            set
            {
                _rank = value;
                OnPropertyChanged();
            }
        }

        private char _file;
        public char File
        {
            get { return _file; }
            set
            {
                _file = value;
                OnPropertyChanged();
            }
        }

        public ChessBoardItem ItemType { get; set; }
    }
}
