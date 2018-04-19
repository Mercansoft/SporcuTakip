using Spor.Mobile.MasterSayfalar;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Spor.Mobile.Views
{
    public class MyMasterPage:MasterDetailPage
    {
        public MyMasterPage()
        {
            Detail = new MyDetail();
            Master = new MyMenu();
        }
    }
}
