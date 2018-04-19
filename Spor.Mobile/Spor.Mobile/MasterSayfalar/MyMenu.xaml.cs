using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spor.Mobile.MasterSayfalar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyMenu : ContentPage
	{
		public MyMenu ()
		{
			InitializeComponent ();
            Title = "Menü";
		}
	}
}