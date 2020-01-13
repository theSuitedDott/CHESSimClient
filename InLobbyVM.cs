using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
    class InLobbyVM : ViewModelBase
    {
		public GameWindow gameWindowView;
		public GameWindowVM gameWindowVM;
		public RelayCommand StartGame { get; set; }
		public RelayCommand LeaveLobby { get; set; }
		public RelayCommand JoinBlack { get; set; }
		public RelayCommand JoinWhite { get; set; }
		public RelayCommand JoinWatcher { get; set; }

		private string _playersUsername;
		public ObservableCollection<string> AllBlackPlayer { get; set; }

		public string UserName
		{
			get { return _playersUsername; }
			set { SetProperty(nameof(UserName), ref _playersUsername, value); }
		}

		public InLobbyVM()
		{
			StartGame = new RelayCommand(OpenSessionGame);
			LeaveLobby = new RelayCommand(LeaveThisLobby);
			JoinBlack = new RelayCommand(AddPlayerToTeamBlack);
			_playersUsername = MainController.USERNAME;
			AllBlackPlayer = new ObservableCollection<string>();
			gameWindowView = new GameWindow();
			gameWindowVM = new GameWindowVM();
		}

		private void AddPlayerToTeamBlack()
		{
			AllBlackPlayer.Add(_playersUsername);
		}

		private void LeaveThisLobby()
		{
			foreach (Window item in Application.Current.Windows)
			{
				if (item.DataContext == this)
				{
					item.Close();
				}
			}
		}

		private void OpenSessionGame()
		{
			Chat chat = new Chat();
			ChatVM chatVM = new ChatVM();
			StockChessCS.Programm nw = new StockChessCS.Programm();
			string yes = nw.returnPath();
			Process.Start(yes + "\\StockChessCS.exe");
			chat.DataContext = chatVM;
			chat.Show();
			LeaveThisLobby();
		}
	}
}
