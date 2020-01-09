using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHESSim
{
    class MainController
    {
		LobbyAuswahlVM lobbyVM;
		LobbyAuswahl lobbyView;
		Anmeldung anmeldungView;
		AnmeldungVM anmeldungVM;
		public static string USERNAME;

		public MainController()
		{
			lobbyView = new LobbyAuswahl();
			lobbyVM = new LobbyAuswahlVM();
			anmeldungView = new Anmeldung();
			anmeldungVM = new AnmeldungVM
			{
				AnmeldeBTN = new x.common.WPF.Commands.RelayCommand(MeldeAnAlsGast),
				GastBTN = new x.common.WPF.Commands.RelayCommand(MeldeAnAlsGast)
			};
		}

		public void ProgrammStart()
		{
			anmeldungView.DataContext = anmeldungVM;
			anmeldungView.Show();
		}
		private void MeldeAnAlsGast()
		{
			lobbyVM.UserRegistered = true;
			lobbyVM.UserName = anmeldungVM.UserName;
			anmeldungView.Close();
			lobbyView.DataContext = lobbyVM;
			lobbyView.Show();
			USERNAME = anmeldungVM.UserName;
	}

	}
}
