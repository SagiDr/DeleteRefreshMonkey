using DeleeRefreshMonkey.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DeleeRefreshMonkey.Services;
//using Android.Net.Wifi.Rtt;
//using Android.Util;

namespace DeleeRefreshMonkey.ViewModels
{
    public class MonkeyViewModel:ViewModelBase
    {
        private ObservableCollection<Monkey> monkeys;
        public ObservableCollection<Monkey> Monkeys
        {
            get
            {
                return this.monkeys;
            }
            set
            {
                this.monkeys = value;
                OnPropertyChanged();
            }
        }
        public MonkeyViewModel()
        {
            monkeys = new ObservableCollection<Monkey>();
            IsRefreshing = false;
            ReadMonkeys();
            Loc = new ObservableCollection<MLocation>();
            FillLoc();
        }

        private ObservableCollection<MLocation> loc;
        public ObservableCollection<MLocation> Loc
        {
            get
            {
                return this.loc;
            }
            set
            {
                this.loc = value;
                OnPropertyChanged();
            }
        }

        private MLocation selectedLoc;
        public MLocation SelectedLoc
        {
            get
            {
                return this.selectedLoc;
            }
            set
            {
                this.selectedLoc = value;
                OnPickerChanged();
                OnPropertyChanged();
            }
        }



        private async void ReadMonkeys()
        {
            MonkeyService service = new MonkeyService();
            List<Monkey> list = await service.GetMonkeys();
            this.Monkeys = new ObservableCollection<Monkey>(list);
        }

        public ICommand DeleteCommand => new Command<Monkey>(RemoveMonkey);

        void RemoveMonkey(Monkey st)
        {
            if (Monkeys.Contains(st))
            {
                Monkeys.Remove(st);
            }
        }

   
        #region Refresh View
        public ICommand RefreshCommand => new Command(Refresh);
        private async void Refresh()
        {

            ReadMonkeys();

            IsRefreshing = false;
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private Object selectedMonkey;
        
        public Object SelectedMonkey
        {
            get
            {
                return this.selectedMonkey;
            }
            set
            {
                this.selectedMonkey = value;
                OnPropertyChanged();
                OnPickerChanged();
            }
        }

        public ICommand SingleSelectCommand => new Command(OnSingleSelectMonkey);

        async void OnSingleSelectMonkey()
        {
            if (SelectedMonkey != null)
            {
                var navParam = new Dictionary<string, object>()
            {
                { "selectedMonkey",SelectedMonkey}
            };

                await Shell.Current.GoToAsync("monkeyDetails", navParam);

                SelectedMonkey = null;
            }
        }

        private void OnPickerChanged()
        {
            ReadMonkeys();
            if (SelectedLoc != null)
            {
                if(SelectedLoc.LocationM == "All")
                {
                    monkeys = new ObservableCollection<Monkey>();
                }
                List<Monkey> tobeRemoved = Monkeys.Where(s => s.Location != SelectedLoc.LocationM).ToList();
                foreach (Monkey monkey in tobeRemoved)
                {
                    Monkeys.Remove(monkey);
                }
            }
        }

        private void FillLoc()
        {
            Loc.Add(new MLocation { Id = 0, LocationM = "All" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Africa & Asia" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Central & South America" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Central and East Africa" });
            Loc.Add(new MLocation { Id = 0, LocationM = "South America" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Japan" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Southern Cameroon, Gabon, Equatorial Guinea, and Congo" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Borneo" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Vietnam, Laos" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Vietnam" });
            Loc.Add(new MLocation { Id = 0, LocationM = "China" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Indonesia" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Sri Lanka" });
            Loc.Add(new MLocation { Id = 0, LocationM = "Ethiopia" });
        }
    }
}
