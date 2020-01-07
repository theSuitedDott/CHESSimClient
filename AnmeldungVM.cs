using System;
using x.common.WPF.Commands;
using x.common.WPF.ViewModel;

namespace CHESSim
{
	internal class AnmeldungVM : ViewModelBase
	{
		public RelayCommand GastBTN { get; set; }
		public RelayCommand AnmeldeBTN { get; set; }
		private string _userName;

		public string UserName
		{
			get { return _userName; }
			set { SetProperty(nameof(UserName), ref _userName, value); }
		}

		public AnmeldungVM()
		{
		}
	}
}