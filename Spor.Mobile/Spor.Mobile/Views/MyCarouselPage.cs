using Spor.Mobile.Sayfalar;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Spor.Mobile.Views
{
    public class MyCarouselPage:CarouselPage
    {
        public MyCarouselPage()
        {
            Children.Add(new MyTab1());
            Children.Add(new MyTab2());
        }
    }
}
