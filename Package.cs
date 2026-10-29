using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace trpo_pract5_WPF_Savitsin
{
    public class Package : INotifyPropertyChanged 
    {        
        public ObservableCollection<Attachment> Items { get; set; } = new();
        int id = 0;
        string recipient = "Получатель";
        string address = "Адрес";
        DateTime sendDate = DateTime.Now;
        double packageWeightKg = 0;
        public int Id 
        { 
            get { return id; }
            set 
            { 
                id = value;
                OnPropertyChanged();
            }
        }
        public string Recipient 
        { 
            get { return recipient; }
            set 
            { 
                recipient = value;
                OnPropertyChanged();
            }
        }
        public string Address
        { 
            get { return address; }
            set
            { 
                address = value;
                OnPropertyChanged();
            }
        }
        public DateTime SendDate
        {
            get { return sendDate; }
            set
            { 
                sendDate = value;
                OnPropertyChanged();
            }
        }
        public double PackageWeightKg
        {
            get
            {
                double total = 0;
                for (int i = 0; i < Items.Count; i++)
                    total = total + Items[i].ItemWeightKg;
                return total;
            }
        }
        public void RefreshWeight()
        {
            OnPropertyChanged(nameof(PackageWeightKg));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
