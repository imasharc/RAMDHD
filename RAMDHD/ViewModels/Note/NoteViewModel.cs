using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMDHD.ViewModels.Note
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _headline = "Add new note";
        private string _editIcon;
        private string _deleteIcon;

        public string Headline
        {
            get => _headline;
            set
            {
                if (_headline != value)
                {
                    _headline = value;
                    OnPropertyChanged(nameof(Headline));
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
