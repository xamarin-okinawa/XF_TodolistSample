using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_TodolistSample.ViewModel;

namespace XF_TodolistSample.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTodo : ContentPage
	{
		public AddTodo (TodoItem item, Action<string, DateTime, string> a)
		{
			InitializeComponent ();
            this.BindingContext = new AddTodoViewModel(Navigation, item, a);
		}
	}
}