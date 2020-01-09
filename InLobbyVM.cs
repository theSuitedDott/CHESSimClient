using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
    class InLobbyVM : ViewModelBase
    {
		public RelayCommand StartGame { get; set; }
		public RelayCommand LeaveLobby { get; set; }
    }
}
