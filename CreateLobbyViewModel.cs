using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
	internal class CreateLobbyViewModel : ViewModelBase
	{
		HttpWebResponse httpResponse;
		public InLobby inLobbyView;
		public InLobbyVM inLobbyVM;
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
			inLobbyView = new InLobby();
			inLobbyVM = new InLobbyVM();

			foreach (System.Windows.Window item in Application.Current.Windows)
			{
				if (item.DataContext == this) item.Close() ;
			}
			SendLobbyRequest();
			if (httpResponse.StatusCode == HttpStatusCode.OK)
			{
				inLobbyView.DataContext = inLobbyVM;
				inLobbyView.Show();
			}
			else
			{
				MessageBox.Show("Die Lobby kann momentan nicht erstellt werden, warten Sie Eventuell ein paar Sekunden und probieren sie es erneut, der Server scheint momentan Schwierigkeiten zu haben die Anfrage zu bearbeiten", "Error Occured", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
			}
		}

		private void SendLobbyRequest()
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://chessim.de");
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			CreateNewSessionJSON product = new CreateNewSessionJSON
			{
				name = _lobbyName,
				password = _passwort,
				nick = MainController.USERNAME
			};
			string output = JsonConvert.SerializeObject(product);

			using (var streamReader = new StreamWriter(httpWebRequest.GetRequestStream()))
			{
				streamReader.Write(output);
			}
			httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
		}
	}
}