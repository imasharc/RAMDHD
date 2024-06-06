using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.ViewModels.GraphTask
{
    public class GraphTaskViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title = "Add new graph task";
        private string _editIcon;
        private string _deleteIcon;

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }
        public string EditIcon
        {
            get => _editIcon;
            set
            {
                if (_editIcon != value)
                {
                    _editIcon = value;
                    OnPropertyChanged(nameof(EditIcon));
                }
            }
        }
        public string DeleteIcon
        {
            get => _deleteIcon;
            set
            {
                if (_deleteIcon != value)
                {
                    _deleteIcon = value;
                    OnPropertyChanged(nameof(DeleteIcon));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
