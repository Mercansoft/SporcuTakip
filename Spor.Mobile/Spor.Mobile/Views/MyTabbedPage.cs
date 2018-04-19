using Spor.Mobile.Sayfalar;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Spor.Mobile.Views
{
    public class MyTabbedPage:TabbedPage
    {
        public MyTabbedPage()
        {
            Children.Add(new MyTab1());
            Children.Add(new MyTab2());
        }
    }
}
