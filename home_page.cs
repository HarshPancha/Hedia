using System;

using Xamarin.Forms;

namespace HC
{
    public class home_page : ContentPage
    {
        public home_page()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

