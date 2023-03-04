using safari.Core;
using safari.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace safari.Model
{
    class MainView : ObservableObject
    {
        private object _current;

        public RelayCommand OpenEmpty { get; set; }
        public RelayCommand OpenContact { get; set; }
        public RelayCommand OpenLesson { get; set; }

        public EmptyViewModel emptyModel { get; set; }
        public ContactViewModel contactModel { get; set; } 
        public LessonViewModel lessonModel { get; set; }

        public object Current
        {
            get { return _current; }
            set 
            { 
                _current = value;
                OnPropertyChanged();
            }
        }

        public MainView() 
        {
            emptyModel = new EmptyViewModel();
            contactModel = new ContactViewModel();
            lessonModel = new LessonViewModel();
            Current = emptyModel;

            OpenEmpty = new RelayCommand(o => 
            {
                Current = emptyModel;
            });

            OpenContact = new RelayCommand(o =>
            {
                Current = contactModel;
            });

            OpenLesson = new RelayCommand(o =>
            {
                Current = lessonModel;
            });
        }
    }
}
