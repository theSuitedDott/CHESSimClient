Imports System.Collections.ObjectModel

Public NotInheritable Class Chess
    Public Shared Function BoardSetup() As ObservableCollection(Of IBoardItem)
        Dim items As New ObservableCollection(Of IBoardItem)
        Dim files = ("abcdefgh").ToArray()

        ' Board squares
        For Each fl In files
            For rank As Integer = 1 To 8
                items.Add(New BoardSquare With {.Rank = rank, .File = fl, .ItemType = ChessBoardItem.Square})
            Next
        Next
        ' Pawns
        For Each fl In files
            items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                           .Piece = PieceType.Pawn, .Rank = 7, .File = fl})
            items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                           .Piece = PieceType.Pawn, .Rank = 2, .File = fl})
        Next
        ' Black pieces
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                       .Piece = PieceType.Rook, .Rank = 8, .File = "a"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                       .Piece = PieceType.Knight, .Rank = 8, .File = "b"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                       .Piece = PieceType.Bishop, .Rank = 8, .File = "c"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                       .Piece = PieceType.Queen, .Rank = 8, .File = "d"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                       .Piece = PieceType.King, .Rank = 8, .File = "e"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                       .Piece = PieceType.Bishop, .Rank = 8, .File = "f"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                       .Piece = PieceType.Knight, .Rank = 8, .File = "g"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.Black,
                       .Piece = PieceType.Rook, .Rank = 8, .File = "h"})
        ' White pieces
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                       .Piece = PieceType.Rook, .Rank = 1, .File = "a"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                       .Piece = PieceType.Knight, .Rank = 1, .File = "b"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                       .Piece = PieceType.Bishop, .Rank = 1, .File = "c"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                       .Piece = PieceType.Queen, .Rank = 1, .File = "d"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                       .Piece = PieceType.King, .Rank = 1, .File = "e"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                       .Piece = PieceType.Bishop, .Rank = 1, .File = "f"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                       .Piece = PieceType.Knight, .Rank = 1, .File = "g"})
        items.Add(New ChessPiece With {.ItemType = ChessBoardItem.Piece, .Color = PieceColor.White,
                       .Piece = PieceType.Rook, .Rank = 1, .File = "h"})

        Return items
    End Function

    Public Shared Sub MovePiece(selectedPiece As ChessPiece, selectedSquare As BoardSquare,
                                items As ObservableCollection(Of IBoardItem))
        Select Case selectedPiece.Piece
            Case PieceType.King
                KingMove(selectedPiece, selectedSquare, items)
                Exit Select
            Case PieceType.Pawn
                PawnMove(selectedPiece, selectedSquare, items)
                Exit Select
            Case Else
                Move(selectedPiece, selectedSquare)
                Exit Select
        End Select
    End Sub

    Private Shared Sub Move(piece As ChessPiece, square As BoardSquare)
        piece.Rank = square.Rank
        piece.File = square.File
    End Sub

    Private Shared Sub KingMove(piece As ChessPiece, targetSquare As BoardSquare, items As ObservableCollection(Of IBoardItem))
        If piece.File = "e" And targetSquare.File = "g" Then ' Short castle
            Dim rook = items.OfType(Of ChessPiece).Where(Function(p) p.Color = piece.Color AndAlso
                                                             p.Piece = PieceType.Rook AndAlso
                                                             p.File = "h").FirstOrDefault
            piece.File = "g"
            rook.File = "f"
        ElseIf piece.File = "e" And targetSquare.File = "c" Then ' Long castle
            Dim rook = items.OfType(Of ChessPiece).Where(Function(p) p.Color = piece.Color AndAlso
                                                            p.Piece = PieceType.Rook AndAlso
                                                            p.File = "a").FirstOrDefault
            piece.File = "c"
            rook.File = "d"
        Else
            Move(piece, targetSquare)
        End If
    End Sub

    Private Shared Sub PawnMove(piece As ChessPiece, targetSquare As BoardSquare, items As ObservableCollection(Of IBoardItem))
        ' Promotion
        Select Case piece.Color
            Case PieceColor.Black
                If targetSquare.Rank = 1 Then piece.Piece = PieceType.Queen
                Exit Select
            Case PieceColor.White
                If targetSquare.Rank = 8 Then piece.Piece = PieceType.Queen
                Exit Select
        End Select
        ' En passant
        If piece.File <> targetSquare.File Then
            Dim opponentPawn = items.OfType(Of ChessPiece).Where(Function(p) p.Color <> piece.Color AndAlso
                                                                             p.Piece = PieceType.Pawn AndAlso
                                                                             p.Rank = piece.Rank AndAlso
                                                                             p.File = targetSquare.File).FirstOrDefault
            items.Remove(opponentPawn)
        End If
        Move(piece, targetSquare)
    End Sub

    Public Shared Sub CapturePiece(selectedPiece As ChessPiece, otherPiece As ChessPiece,
                                   items As ObservableCollection(Of IBoardItem))
        selectedPiece.Rank = otherPiece.Rank
        selectedPiece.File = otherPiece.File
        items.Remove(otherPiece)
    End Sub
End Class
