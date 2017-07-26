using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XF_TodolistSample.View;

namespace XF_TodolistSample
{
    public class HomePageViewModel: INotifyPropertyChanged
    {
        private INavigation navigation;
        public event PropertyChangedEventHandler PropertyChanged;

        
        public HomePageViewModel(INavigation nav)
        {
            this.navigation = nav;

            SelectAll();

            this.AddTaskCommand = new Command(() => AddTask());
            this.ClearTaskCommand = new Command(() => ClearTask());
            this.DeleteTodoCommand = new Command<object>((x) => DeleteTodo(x));
            this.ItemTappedCommand = new Command<object>((item) => ItemTapped(item));
        }

        public void CallBack(string id, DateTime inDate, string task)
        {
            using (var realm = Realm.GetInstance())
            {
                if (id != null)
                {
                    var updateTask = realm.All<TodoItem>().Where(s => s.Id == id).First();
                    realm.Write(() =>
                    {
                        inDate = DateTime.SpecifyKind(inDate, DateTimeKind.Utc);
                        updateTask.InDate = inDate;
                        updateTask.Todo = task;
                    });
                }
                else
                {
                    realm.Write(() =>
                    {
                        var addTask = realm.CreateObject("TodoItem");
                        addTask.Id = Guid.NewGuid().ToString();
                        inDate = DateTime.SpecifyKind(inDate, DateTimeKind.Utc);
                        addTask.InDate = inDate;
                        addTask.Todo = task;
                        realm.Add(addTask);
                    });
                }
            }
        }

        public ICommand ItemTappedCommand { get; set; }
        private void ItemTapped(object item)
        {
            TodoItem todoItem = (TodoItem)item;

            Action<string, DateTime, string> callback = CallBack;

            this.navigation.PushAsync(new AddTodo(todoItem, callback));
        }

        public ICommand AddTaskCommand { get; }
        private void AddTask()
        {
            Action<string, DateTime, string> callback = CallBack;

            this.navigation.PushAsync(new AddTodo(null, callback));
        }

        public ICommand ClearTaskCommand { get; }
        private void ClearTask()
        {
            var realm = Realm.GetInstance();
            using (var trans = realm.BeginWrite())
            {
                realm.RemoveAll<TodoItem>();
                trans.Commit();
            }
        }

        public ICommand DeleteTodoCommand { get; }
        private void DeleteTodo(object parameter)
        {
            TodoItem delItem = (TodoItem)parameter;

            var realm = Realm.GetInstance();
            using (var trans = realm.BeginWrite())
            {
                realm.Remove(delItem);
                trans.Commit();
            }
        }
        
        public void SelectAll()
        {
            Realm _realm = Realm.GetInstance();
            this.TodoItemsData = _realm.All<TodoItem>();
        }

        private IEnumerable<TodoItem> todoItemsData;
        public IEnumerable<TodoItem> TodoItemsData
        {
            set
            {
                if (todoItemsData != value)
                {
                    todoItemsData = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TodoItemsData"));
                    }
                }
            }
            get
            {
                return todoItemsData;
            }   
        }
    }
}
