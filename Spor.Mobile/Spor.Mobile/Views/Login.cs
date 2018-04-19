using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Spor.Mobile.Views
{
    public class Login : ContentPage
    {
        public Login()
        {
            Label _lblPage = new Label();
            _lblPage.Text = "2nci Syfa";
            _lblPage.FontSize = 30;
            _lblPage.HorizontalOptions = LayoutOptions.Center;
            _lblPage.VerticalOptions = LayoutOptions.Center;

            Content = _lblPage;
        }

    }
}
