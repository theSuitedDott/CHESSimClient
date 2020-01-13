using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
	class ChatVM : ViewModelBase
	{
		public RelayCommand LeaveGame { get; set; }
		public RelayCommand SendMessage { get; set; }
		public ObservableCollection<string> ChatNachrichtenLB { get; set; }
		private string chatBoxMessage;

		public string ChatNachricht
		{
			get { return chatBoxMessage; }
			set { SetProperty(nameof(ChatNachricht), ref chatBoxMessage, value); }
		}

		public ChatVM()
		{
			LeaveGame = new RelayCommand(LeaveActualGame);
			SendMessage = new RelayCommand(SendeNachricht);
			ChatNachrichtenLB = new ObservableCollection<string>();
		}
		//Method to add ChatMessage to ChatBox
		private void SendeNachricht()
		{
			ChatNachrichtenLB.Add(MainController.USERNAME + ": " + chatBoxMessage);
			chatBoxMessage = "";
		}
		private void LeaveActualGame()
		{
			foreach (Window item in Application.Current.Windows)
			{
				if (item.DataContext == this)
				{
					item.Close();
				}
			}
		}
	}
}
