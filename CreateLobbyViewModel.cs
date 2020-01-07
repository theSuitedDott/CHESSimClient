using System;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
	internal class CreateLobbyViewModel : ViewModelBase
	{

		private string _lobbyName;
		private string _passwort;
		public RelayCommand LobbyErstellen { get; set; }
		public RelayCommand ClosePage { get; set; }

		public string LobbyName
		{
			get { return _lobbyName; }
			set { SetProperty(nameof(LobbyName), ref _lobbyName, value); }
		}

		public string Passwort
		{
			get { return _passwort; }
			set { SetProperty(nameof(Passwort), ref _passwort, value); }
		}

		public CreateLobbyViewModel()
		{
			LobbyErstellen = new RelayCommand(OpenInLobby);
		}

		private void OpenInLobby()
		{
			//Create Lobby TODO
		}
	}
}