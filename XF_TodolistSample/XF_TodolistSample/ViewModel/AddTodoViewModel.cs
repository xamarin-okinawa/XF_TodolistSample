using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XF_TodolistSample.ViewModel
{
    public class AddTodoViewModel: INotifyPropertyChanged
    {
        private INavigation navigation;
        public event PropertyChangedEventHandler PropertyChanged;
        private Action<string, DateTime, string> action;
        private string id;

        public AddTodoViewModel(INavigation nav, TodoItem item, Action<string, DateTime, string> callback)
        {
            this.navigation = nav;
            this.action = callback;
            if (item != null)
            {
                this.id = item.Id;
                this.InDate = item.InDate.DateTime;
                this.Todo = item.Todo;
            }
            else
            {
                this.InDate = DateTime.Today;
            }

            TodoCreationCommand = new Command(() => TodoCreation());
            
        }

        public ICommand TodoCreationCommand { get; }
        private void TodoCreation ()
        {
            action(id,InDate, Todo);

            this.navigation.PopAsync();
        }

        private DateTime inDate;
        public DateTime InDate
        {
            set
            {
                if (inDate != value)
                {
                    inDate = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("InDate"));
                    }

                }
            }
            get
            {
                return inDate;
            }
        }

        private string todo;
        public string Todo
        {
            set
            {
                if (todo != value)
                {
                    todo = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Todo"));
                    }
                }
            }
            get
            {
                return todo;
            }
        }
    }
}
