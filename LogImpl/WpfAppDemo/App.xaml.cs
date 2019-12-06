using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Toast toast = new Toast();
            Thread.Sleep(2000);
            Console.WriteLine("hello");
            toast.ShowNotification();
        }
    }
}
