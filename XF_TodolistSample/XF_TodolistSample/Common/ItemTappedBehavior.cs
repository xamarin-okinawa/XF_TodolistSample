using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace XF_TodolistSample.Common
{
    public class ItemTappedBehavior : Behavior<ListView>
    {
        public static BindableProperty CommandProperty = BindableProperty.Create(
            "Command", typeof(ICommand), typeof(ItemTappedBehavior), null
        );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            // ↓↓↓ これ重要  
            bindable.BindingContextChanged += (sender, e) => {
                this.BindingContext = ((ListView)sender).BindingContext;
            };
            // ↑↑↑ これ重要  

            bindable.ItemTapped += (sender, e) => {
                this.Command.Execute(e.Item);
            };
        }
    }
}
