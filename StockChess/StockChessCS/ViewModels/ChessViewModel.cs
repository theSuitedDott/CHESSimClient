using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StockChessCS.Interfaces;
using StockChessCS.Commands;
using StockChessCS.Enums;
using StockChessCS.Helpers;
using StockChessCS.Models;
using System.Windows.Input;

namespace StockChessCS.ViewModels
{
    public class ChessViewModel : ViewModelBase
    {
        private IEngineService engine;
        private StringBuilder moves = new StringBuilder();
        private short deepAnalysisTime = 5000;
        private short moveValidationTime = 1;
        private TaskFactory ctxTaskFactory;

        public ChessViewModel(IEngineService es)
        {
            engine = es;
            BoardItems = Chess.BoardSetup();
            ctxTaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
            engine.EngineMessage += EngineMessage;
            engine.StartEngine();
        }

        private ObservableCollection<IBoardItem> _boardItems;
        public ObservableCollection<IBoardItem> BoardItems
        {
            get { return _boardItems; }
            set
            {
                _boardItems = value;
                OnPropertyChanged();
            }
        }

        private bool _isEngineThinking;
        public bool IsEngineThinking
        {
            get { return _isEngineThinking; }
            set
            {
                _isEngineThinking = value;
                OnPropertyChanged();
            }
        }

        private bool _checkMate;
        public bool CheckMate
        {
            get { return _checkMate; }
            set
            {
                _checkMate = value;
                OnPropertyChanged();
            }
        }

        private PieceColor _playerColor;
        public PieceColor PlayerColor
        {
            get { return _playerColor; }
            set
            {
                _playerColor = value;
                OnPropertyChanged();
            }
        }

        private bool playerWantsToMovePiece;
        private bool playerWantsToCapturePiece;
        private ChessPiece selectedPiece;
        private BoardSquare targetSquare;
        private ChessPiece targetPiece;        

        public IBoardItem SelectedBoardItem
        {
            set
            {
                if (value is ChessPiece)
                {
                    var piece = (ChessPiece)value;
                    if (piece.Color == PlayerColor)
                    {
                        selectedPiece = piece;
                    }
                    else if (piece.Color != PlayerColor && selectedPiece != null)
                    {
                        playerWantsToCapturePiece = true;
                        targetPiece = piece;
                        ValidateMove(targetPiece);
                    }
                }
                else if (value is BoardSquare && selectedPiece != null)
                {
                    playerWantsToMovePiece = true;
                    targetSquare = (BoardSquare)value;
                    ValidateMove(targetSquare);
                }
            }
        }

        private ICommand _newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (_newGameCommand == null) _newGameCommand = new RelayCommand(o => NewGame());
                return _newGameCommand;
            }
        }

        private void NewGame()
        {
            BoardItems = Chess.BoardSetup();
            if (moves.Length > 0) moves.Clear();
            if (CheckMate) CheckMate = false;            
            if (IsEngineThinking) IsEngineThinking = false;
            ResetSomeMembers();
            engine.SendCommand(UciCommands.ucinewgame);

            if (PlayerColor == PieceColor.Black)
            {
                engine.SendCommand(UciCommands.position);
                engine.SendCommand(UciCommands.go_movetime + " " + deepAnalysisTime.ToString());
                IsEngineThinking = true;
            }
        }

        private ICommand _stopEngineCommand;
        public ICommand StopEngineCommand
        {
            get
            {
                if (_stopEngineCommand == null) _stopEngineCommand = new RelayCommand(o => StopEngine());
                return _stopEngineCommand;
            }
        }

        private void StopEngine()
        {
            engine.EngineMessage -= EngineMessage;
            engine.StopEngine();
        }

        private ICommand _changePlayerColorCommand;
        public ICommand ChangePlayerColorCommand
        {
            get
            {
                if (_changePlayerColorCommand == null)
                {
                    _changePlayerColorCommand = new RelayCommand<PieceColor>(ChangePlayerColor);
                }
                return _changePlayerColorCommand;
            }
        }

        private void ChangePlayerColor(PieceColor color)
        {
            PlayerColor = color;
            NewGame();
        }

        private void EngineMessage(string message)
        {            
            if (message.Contains(UciCommands.bestmove)) // Message is in the form: bestmove <move> ponder <move>
            {
                if (!message.Contains("ponder")) CheckMate = true;

                var move = message.Split(' ').ElementAt(1);
                var position1 = move.Take(2);
                var position2 = move.Skip(2);                       
                var enginePiece = BoardItems.OfType<ChessPiece>().
                    Where(p => p.Rank == int.Parse(position1.ElementAt(1).ToString()) &
                    p.File == position1.ElementAt(0)).Single();
                
                if (enginePiece.Color == PlayerColor) // Player made illegal move
                {
                    RemoveLastMove();
                    ResetSomeMembers();                   
                }
                else
                {
                    if (playerWantsToMovePiece)
                    {
                        ctxTaskFactory.StartNew(() => Chess.MovePiece(selectedPiece, targetSquare, BoardItems)).Wait();                        
                        DeeperMoveAnalysis();
                    }
                    else if (playerWantsToCapturePiece)
                    {
                        ctxTaskFactory.StartNew(() => Chess.CapturePiece(selectedPiece, targetPiece, BoardItems)).Wait();                        
                        DeeperMoveAnalysis();
                    }
                    else // Engine move
                    {
                        moves.Append(" " + move);
                        var pieceToCapture = BoardItems.OfType<ChessPiece>().
                            Where(p => p.Rank == int.Parse(position2.ElementAt(1).ToString()) &
                            p.File == position2.ElementAt(0)).SingleOrDefault();

                        if (pieceToCapture != null)
                        {
                            ctxTaskFactory.StartNew(() => Chess.CapturePiece(enginePiece, pieceToCapture, BoardItems)).Wait();
                        }
                        else
                        {
                            targetSquare = BoardItems.OfType<BoardSquare>().
                                Where(s => s.Rank == int.Parse(position2.ElementAt(1).ToString()) &
                                s.File == position2.ElementAt(0)).SingleOrDefault();
                            ctxTaskFactory.StartNew(() => Chess.MovePiece(enginePiece, targetSquare, BoardItems)).Wait();
                        }
                        IsEngineThinking = false;
                    }
                }
            }
        }

        private void ResetSomeMembers()
        {
            if (selectedPiece != null) selectedPiece = null;
            if (playerWantsToCapturePiece) playerWantsToCapturePiece = false;
            if (playerWantsToMovePiece) playerWantsToMovePiece = false;
        }

        private void DeeperMoveAnalysis()
        {
            SendMovesToEngine(deepAnalysisTime);
            IsEngineThinking = true;
            ResetSomeMembers();
        }

        private void RemoveLastMove()
        {
            if (moves.Length > 0)
            {
                var length = moves.Length;
                var start = moves.Length - 5;
                moves.Remove(start, 5);
            }
        }        

        private void ValidateMove(IBoardItem item)
        {
            var position1 = selectedPiece.File.ToString() + selectedPiece.Rank.ToString();
            var position2 = item.File.ToString() + item.Rank.ToString();

            var move = position1 + position2;
            moves.Append(" " + move);
            SendMovesToEngine(moveValidationTime);
        }

        private void SendMovesToEngine(short time)
        { 
            var command = UciCommands.position + moves.ToString();
            engine.SendCommand(command);
            command = UciCommands.go_movetime + " " + time.ToString();
            engine.SendCommand(command);
        }
    }
}