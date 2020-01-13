Public Class ChessPiece
    Inherits ViewModelBase
    Implements IBoardItem

    Private _color As PieceColor
    Public Property Color As PieceColor
        Get
            Return _color
        End Get
        Set(value As PieceColor)
            _color = value
            OnPropertyChanged()
        End Set
    End Property

    Private _piece As PieceType
    Public Property Piece As PieceType
        Get
            Return _piece
        End Get
        Set(value As PieceType)
            _piece = value
            OnPropertyChanged()
        End Set
    End Property

    Private _rank As Integer
    Public Property Rank As Integer Implements IBoardItem.Rank
        Get
            Return _rank
        End Get
        Set(value As Integer)
            _rank = value
            OnPropertyChanged()
        End Set
    End Property

    Private _file As Char
    Public Property File As Char Implements IBoardItem.File
        Get
            Return _file
        End Get
        Set(value As Char)
            _file = value
            OnPropertyChanged()
        End Set
    End Property

    Public Property ItemType As ChessBoardItem Implements IBoardItem.ItemType
End Class
