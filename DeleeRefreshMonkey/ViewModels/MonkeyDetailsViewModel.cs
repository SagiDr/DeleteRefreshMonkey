using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeleeRefreshMonkey.Models;

namespace DeleeRefreshMonkey.ViewModels
{
    [QueryProperty(nameof(SelectedMonkey), "selectedMonkey")] 
    public class MonkeyDetailsViewModel : ViewModelBase
    {
        private Monkey selectedMonkey;
        public Monkey SelectedMonkey
        {
            get
            {
                return this.SelectedMonkey;
            }
            set
            {
                this.SelectedMonkey = value;
                OnPropertyChanged();
            }
        }

    }
}
