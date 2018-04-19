using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spor.Mobile.Sayfalar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyTab2 : ContentPage
	{
		public MyTab2 ()
		{
			InitializeComponent ();
            this.Title = "Tab2";
        }
	}
}