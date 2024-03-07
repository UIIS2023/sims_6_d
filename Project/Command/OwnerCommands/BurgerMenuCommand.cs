using Project.View.OwnerView;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.OwnerCommands
{
    public class BurgerMenuCommand : CommandBase
    {
        private readonly ViewModelBase viewModelBase;

        public BurgerMenuCommand(ViewModelBase viewModelBase)
        {
            this.viewModelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {

            MenuView menuView = new MenuView(viewModelBase.User);
            menuView.Show();
            viewModelBase.Window.Close();
        }
    }
}
