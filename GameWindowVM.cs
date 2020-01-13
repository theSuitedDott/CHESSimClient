using System;
using System.Collections.ObjectModel;
using System.Windows;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
	internal class GameWindowVM : ViewModelBase
	{
		public RelayCommand LeaveGame;
		public RelayCommand SendMessage;
		public ObservableCollection<string> ChatNachrichtenLB { get; set; }
		private string chatBoxMessage;

		public string ChatNachricht
		{
			get { return chatBoxMessage; }
			set { SetProperty(nameof(ChatNachricht), ref chatBoxMessage, value); }
		}

		public GameWindowVM()
		{
			LeaveGame = new RelayCommand(LeaveActualGame);
			SendMessage = new RelayCommand(SendeNachricht);
			ChatNachrichtenLB = new ObservableCollection<string>();
		}
		//Method to add ChatMessage to ChatBox
		private void SendeNachricht()
		{
			ChatNachrichtenLB.Add(ChatNachricht);
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