using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace HC
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public bool status = true;

        public MainPage()
        {
            InitializeComponent();

            

            if(App.if_internet == false)
            {
                online_status_label.Text = "Offline!!";
                online_status_label.FadeTo(1).ContinueWith((result) => { });
            }

           

        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            DisplayAlert("Help!", "Press login if you are an exising user or press sign up if you are new", "Return");
        }


        private async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            if(status == true)
            {
                await Navigation.PushAsync(new signup());

            }
            else
            {
                await DisplayAlert("Nah!", "Please connect to internet to access our services", "Return");

            }
        }

        private async void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            if(status == true)
            {
                await Navigation.PushAsync(new login());

            }
            else
            {
                await DisplayAlert("Nah!", "Please connect to internet to access our services", "Return");

            }

        }
        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if(e.NetworkAccess == NetworkAccess.Internet)
            {
                status = true;
                //await online_status_label.FadeTo(1).ContinueWith((result) => { });

                online_status_label.Text = "Back Online!!";
                online_status_label.TextColor = Color.White;
                online_status_label.BackgroundColor = Color.Green;
                await online_status_label.FadeTo(1).ContinueWith((result) => { });

                await Task.Delay(3000);



               

                await online_status_label.FadeTo(0).ContinueWith((result) => { });
            }
            else
            {
                status = false;
                online_status_label.Text = "Offline!!";
                online_status_label.BackgroundColor = Color.Red;
                await online_status_label.FadeTo(1).ContinueWith((result) => { });

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

        }

       


    }

    
}
