using System;
using System.Collections.ObjectModel;
using System.Windows;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
	public class LobbyAuswahlVM : ViewModelBase
	{
		CreateLobby clView;
		CreateLobbyViewModel clVM;
		public RelayCommand CloseBTN { get; set; }
		public RelayCommand LobbyBeitreten { get; set; }
		public RelayCommand LobbyErstellen { get; set; }
		public RelayCommand FindSessions { get; set; }
		public ObservableCollection<string> LobbyList {get;set;}

		private string _userName;
		private bool _userRegistered;

		public static LobbyAuswahlVM Instance { get; } = new LobbyAuswahlVM();

		public bool UserRegistered
		{
			get { return _userRegistered; }
			set { SetProperty(nameof(UserRegistered), ref _userRegistered, value); }
		}

		public string UserName
		{
			get { return _userName; }
			set { SetProperty(nameof(UserName), ref _userName, value); }
		}

		public LobbyAuswahlVM()
		{
			_userRegistered = false;
			Anmeldung view = new Anmeldung();
			CloseBTN = new RelayCommand(CloseApplicaiton);
			LobbyBeitreten = new RelayCommand(JoinLoby);
			LobbyErstellen = new RelayCommand(CreateLobby);
			FindSessions = new RelayCommand(FindeSession);
			LobbyList = new ObservableCollection<string>();
		}

		private void FindeSession()
		{
			//RequestServerAboutSessions
			if (true)
			{
				if (!LobbyList.Contains("Keine erstellten Lobbys"))
				{
					LobbyList.Add("Keine erstellten Lobbys");
				}
			}
		}

		private void JoinLoby()
		{
			//SendServer Request who is in Lobby
			//Has Password?
		}

		private void CreateLobby()
		{
			clView = new CreateLobby();
			clVM = new CreateLobbyViewModel();
			clVM.ClosePage = new RelayCommand(CloseWindow);
			clView.DataContext = clVM;
			clView.Show();
		}

		private void CloseWindow()
		{
			clView.Close();
		}

		private void CloseApplicaiton()
		{
			Application.Current.Shutdown();
		}
	}
}
