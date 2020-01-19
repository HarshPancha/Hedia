using System;

using Xamarin.Forms;

namespace HC
{
    public class assets : ContentPage
    {
        public assets()
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

