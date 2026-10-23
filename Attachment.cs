using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace trpo_pract5_WPF_Savitsin
{
    public class Attachment : INotifyPropertyChanged
    {
        string name = "Название вложение";
        double unitWeightKg = 0;
        int count = 0;
        double itemWeightKg = 0;

        public string Name
        { 
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public double UnitWeightKg
        {
            get { return unitWeightKg; }
            set
            {
                unitWeightKg = value;
                OnPropertyChanged();
            }
        }

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }
        public double ItemWeightKg
        {
            get { return itemWeightKg; }
            set
            {
                itemWeightKg = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
