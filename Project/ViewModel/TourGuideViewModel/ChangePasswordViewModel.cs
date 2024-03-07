using Project.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class ChangePasswordViewModel:CloseableViewModel
    {
        private string _current = string.Empty;
        public string Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                OnPropertyChanged(nameof(Current));
            }
        }

        private string _new = string.Empty;
        public string New
        {
            get
            {
                return _new;
            }
            set
            {
                _new = value;
                OnPropertyChanged(nameof(New));
            }
        }

        private string _confirm = string.Empty;
        public string Confirm
        {
            get
            {
                return _confirm;
            }
            set
            {
                _confirm = value;
                OnPropertyChanged(nameof(Confirm));
            }
        }

        public ChangePasswordViewModel()
        {
            
        }

        private RelayCommand submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new RelayCommand(param => this.Submit(), param => this.CanSubmit());
                }
                return submitCommand;
            }
        }

        private bool CanSubmit()
        {
            return true;
        }
        private void Submit()
        {
            if(Current == string.Empty)
            {
                DialogResult dialogResult1 = MessageBox.Show("You must enter current password", "Error", MessageBoxButtons.OK);
            }
            else if(New == string.Empty){
                DialogResult dialogResult2 = MessageBox.Show("You must enter new password", "Error", MessageBoxButtons.OK);
            }
            else if (Confirm == string.Empty)
            {
                DialogResult dialogResult3 = MessageBox.Show("You must confirm new password", "Error", MessageBoxButtons.OK);
            }
            else {
                DialogResult dialogResult = MessageBox.Show("You entered the wrong current password", "Error", MessageBoxButtons.OK);
            }

            


        }


        private RelayCommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(param => this.Close(), param => this.CanClose());
                }
                return closeCommand;
            }
        }

        private bool CanClose()
        {
            return true;
        }

        private void Close()
        {
            this.OnClosingRequest();
        }
    }
}
