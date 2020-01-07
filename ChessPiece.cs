using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using x.common.WPF;
using x.common.WPF.ViewModel;
using CHESSim.Classes;

namespace CHESSim
{
    class ChessPiece : ViewModelBase
    {
		private Point _Pos;
		public Point Pos
		{
			get { return this._Pos; }
			set { SetProperty(nameof(Pos), ref _Pos, value); }
		}

		private PieceType _Type;
		public PieceType Type
		{
			get { return this._Type; }
			set { SetProperty(nameof(Type), ref _Type, value); }
		}

		private Player _Player;
		public Player Player
		{
			get { return this._Player; }
			set { SetProperty(nameof(Player), ref _Player, value); }
		}
	}
}
