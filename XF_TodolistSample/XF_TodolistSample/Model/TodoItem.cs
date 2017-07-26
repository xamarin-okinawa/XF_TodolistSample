using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XF_TodolistSample
{
    public class TodoItem: RealmObject, INotifyPropertyChanged
    {
        public string Id { get; set; }

        public DateTimeOffset InDate { get; set; }

        public String Todo { get; set; }
    }
}
