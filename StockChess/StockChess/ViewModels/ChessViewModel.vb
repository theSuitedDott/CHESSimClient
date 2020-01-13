Imports System.Collections.ObjectModel
Imports System.Text

Public Class ChessViewModel
    Inherits ViewModelBase

    Private engine As IEngineService
    Private moves As New StringBuilder
    Private deepAnalysisTime As Short = 5000
    Private moveValidationTime As Short = 1
    Private ctxTaskFactory As TaskFactory

    Public Sub New(es As IEngineService)
        engine = es
        BoardItems = Chess.BoardSetup()
        ctxTaskFactory = New TaskFactory(TaskScheduler.FromCurrentSynchronizationContext)
        AddHandler engine.EngineMessage, AddressOf EngineMessage
        engine.StartEngine()
    End Sub

    Private _boardItems As ObservableCollection(Of IBoardItem)
    Public Property BoardItems As ObservableCollection(Of IBoardItem)
        Get
            Return _boardItems
        End Get
        Set(value As ObservableCollection(Of IBoardItem))
            _boardItems = value
            OnPropertyChanged()
        End Set
    End Property

    Private _isEngineThinking As Boolean
    Public Property IsEngineThinking As Boolean
        Get
            Return _isEngineThinking
        End Get
        Set(value As Boolean)
            _isEngineThinking = value
            OnPropertyChanged()
        End Set
    End Property

    Private _checkMate As Boolean
    Public Property CheckMate As Boolean
        Get
            Return _checkMate
        End Get
        Set(value As Boolean)
            _checkMate = value
            OnPropertyChanged()
        End Set
    End Property

    Private _playerColor As PieceColor
    Public Property PlayerColor As PieceColor
        Get
            Return _playerColor
        End Get
        Set(value As PieceColor)
            _playerColor = value
            OnPropertyChanged()
        End Set
    End Property

    Private playerWantsToMovePiece As Boolean
    Private playerWantsToCapturePiece As Boolean
    Private selectedPiece As ChessPiece
    Private targetSquare As BoardSquare
    Private targetPiece As ChessPiece

    Public WriteOnly Property SelectedBoardItem As IBoardItem
        Set(value As IBoardItem)
            If TypeOf value Is ChessPiece Then
                Dim piece = CType(value, ChessPiece)
                If piece.Color = PlayerColor Then
                    selectedPiece = piece
                ElseIf piece.Color <> PlayerColor AndAlso selectedPiece IsNot Nothing Then
                    playerWantsToCapturePiece = True
                    targetPiece = piece
                    ValidateMove(targetPiece)
                End If
            ElseIf TypeOf value Is BoardSquare AndAlso selectedPiece IsNot Nothing Then
                playerWantsToMovePiece = True
                targetSquare = CType(value, BoardSquare)
                ValidateMove(targetSquare)
            End If
        End Set
    End Property

    Private _newGameCommand As ICommand
    Public ReadOnly Property NewGameCommand As ICommand
        Get
            If _newGameCommand Is Nothing Then
                _newGameCommand = New RelayCommand(AddressOf NewGame)
            End If
            Return _newGameCommand
        End Get
    End Property

    Private Sub NewGame()
        BoardItems = Chess.BoardSetup()
        If moves.Length > 0 Then moves.Clear()
        If CheckMate Then CheckMate = False
        If IsEngineThinking Then IsEngineThinking = False
        ResetSomeMembers()
        engine.SendCommand(UciCommands.ucinewgame)

        If PlayerColor = PieceColor.Black Then
            engine.SendCommand(UciCommands.position)
            engine.SendCommand(UciCommands.go_movetime & " " & deepAnalysisTime.ToString)
            IsEngineThinking = True
        End If
    End Sub

    Private _stopEngineCommand As ICommand
    Public ReadOnly Property StopEngineCommand As ICommand
        Get
            If _stopEngineCommand Is Nothing Then
                _stopEngineCommand = New RelayCommand(AddressOf StopEngine)
            End If
            Return _stopEngineCommand
        End Get
    End Property

    Private Sub StopEngine()
        RemoveHandler engine.EngineMessage, AddressOf EngineMessage
        engine.StopEngine()
    End Sub

    Private _changePlayerColorCommand As ICommand
    Public ReadOnly Property ChangePlayerColorCommand As ICommand
        Get
            If _changePlayerColorCommand Is Nothing Then
                _changePlayerColorCommand = New RelayCommand(Of PieceColor)(AddressOf ChangePlayerColor)
            End If
            Return _changePlayerColorCommand
        End Get
    End Property

    Private Sub ChangePlayerColor(color As PieceColor)
        PlayerColor = color
        NewGame()
    End Sub

    Private Sub EngineMessage(message As String)
        If message.Contains(UciCommands.bestmove) Then ' Message is in the form: bestmove <move> ponder <move>
            If Not message.Contains("ponder") Then CheckMate = True

            Dim move = message.Split(" ").ElementAt(1)
            Dim position1 = move.Take(2)
            Dim position2 = move.Skip(2)
            Dim enginePiece = BoardItems.OfType(Of ChessPiece).Where(Function(p) p.Rank = CInt(position1(1).ToString()) And
                                                                         p.File = position1(0)).Single

            If enginePiece.Color = PlayerColor Then ' Player made illegal move
                RemoveLastMove()
                ResetSomeMembers()
            Else
                If playerWantsToMovePiece Then
                    ctxTaskFactory.StartNew(Sub() Chess.MovePiece(selectedPiece, targetSquare, BoardItems)).Wait()
                    DeeperMoveAnalysis()
                ElseIf playerWantsToCapturePiece Then
                    ctxTaskFactory.StartNew(Sub() Chess.CapturePiece(selectedPiece, targetPiece, BoardItems)).Wait()
                    DeeperMoveAnalysis()
                Else ' Engine move
                    moves.Append(" " & move)
                    Dim pieceToCapture = BoardItems.OfType(Of ChessPiece).Where(Function(p) p.Rank = CInt(position2(1).ToString()) And
                                                                                    p.File = position2(0)).SingleOrDefault
                    If pieceToCapture IsNot Nothing Then
                        ctxTaskFactory.StartNew(Sub() Chess.CapturePiece(enginePiece, pieceToCapture, BoardItems)).Wait()
                    Else
                        targetSquare = BoardItems.OfType(Of BoardSquare).Where(Function(s) s.Rank = CInt(position2(1).ToString()) And
                                                                                       s.File = position2(0)).SingleOrDefault
                        ctxTaskFactory.StartNew(Sub() Chess.MovePiece(enginePiece, targetSquare, BoardItems)).Wait()
                    End If
                    IsEngineThinking = False
                End If
            End If
        End If
    End Sub

    Private Sub ResetSomeMembers()
        If selectedPiece IsNot Nothing Then selectedPiece = Nothing
        If playerWantsToMovePiece Then playerWantsToMovePiece = False
        If playerWantsToCapturePiece Then playerWantsToCapturePiece = False
    End Sub

    Private Sub DeeperMoveAnalysis()
        SendMovesToEngine(deepAnalysisTime)
        IsEngineThinking = True
        ResetSomeMembers()
    End Sub

    Private Sub RemoveLastMove()
        If moves.Length > 0 Then
            Dim length = moves.Length
            Dim start = moves.Length - 5
            moves.Remove(start, 5)
        End If
    End Sub

    Private Sub ValidateMove(item As IBoardItem)
        Dim position1 = selectedPiece.File.ToString() & selectedPiece.Rank.ToString()
        Dim position2 = item.File.ToString() & item.Rank.ToString()

        Dim move = position1 & position2
        moves.Append(" " & move)
        SendMovesToEngine(moveValidationTime)
    End Sub

    Private Sub SendMovesToEngine(time As Short)
        Dim command = UciCommands.position & moves.ToString
        engine.SendCommand(command)
        command = UciCommands.go_movetime & " " & time.ToString
        engine.SendCommand(command)
    End Sub
End Class
