﻿using SPZO.Commands;
using System.Windows.Input;

namespace SPZO.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new EmptyStartViewModel(); //Define what selectedViewModel is and assign empty on start

		public BaseViewModel SelectedViewModel
		{
			get { return _selectedViewModel; }
			set { 
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
		}

        public MainWindowViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }

        public ICommand UpdateViewCommand { get; set; }
    }
}
