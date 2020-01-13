Public Class BoardSquare
    Implements IBoardItem

    Public Property Rank As Integer Implements IBoardItem.Rank
    Public Property File As Char Implements IBoardItem.File
    Public Property ItemType As ChessBoardItem Implements IBoardItem.ItemType
End Class
