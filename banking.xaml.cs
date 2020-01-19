using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace HC 
{
    
    public partial class banking : ContentPage 
    {
        public bool status = true;
        public banking()
        {
            InitializeComponent();
            
            ballance.Text = ("Ballance: " + App.GlobalCreditCoins + " CC").ToString();
           

        }

       

        private async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            if(status == true)
            {
                await Navigation.PushAsync(new transfer());

            }
            else
            {
                await DisplayAlert("Nah!", "Please connect to internet to access our services", "Return");

            }

        }

        private async void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            if (status == true)
            {
                await Navigation.PushAsync(new moredeposit());

            }
            else
            {
                await DisplayAlert("Nah!", "Please connect to internet to access our services", "Return");

            }

        }

        private async void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            if (status == true)
            {
                await Navigation.PushAsync(new his());

            }
            else
            {
                await DisplayAlert("Nah!", "Please connect to internet to access our services", "Return");

            }

        }

        private async void Handle_Clicked_4(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();

            //ActivityIndicator ac = new ActivityIndicator();

            //ac.Color = Color.Red;




            if (status == true)
            {
                await Navigation.PushAsync(new picturelist());

            }
            else
            {
                await DisplayAlert("Nah!", "Please connect to internet to access our services", "Return");
                ac.IsRunning = false;
                ac.IsVisible = false;
                ac.IsEnabled = false;

            }


        }

        void Handle_Pressed(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            ac.IsRunning = true;
            ac.IsVisible = true;
            ac.IsEnabled = true;
        }

        void Handle_Released(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            ac.IsRunning = true;
            ac.IsVisible = true;
            ac.IsEnabled = true;
        }

        private async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
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
                online_status_label.Text = "Offline!!";
                online_status_label.BackgroundColor = Color.Red;
                await online_status_label.FadeTo(1).ContinueWith((result) => { });
                status = false;

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
