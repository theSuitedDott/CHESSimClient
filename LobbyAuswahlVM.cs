using System.Windows;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
	public class LobbyAuswahlVM : ViewModelBase
	{
		public RelayCommand CloseBTN { get; set; }
		public RelayCommand LobbyBeitreten { get; set; }
		public RelayCommand LobbyErstellen { get; set; }
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
		}

		private void JoinLoby()
		{
			//Todo
		}

		private void CreateLobby()
		{
			//Todo
		}

		private void CloseApplicaiton()
		{
			Application.Current.Shutdown();
		}
	}
}
