using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HC
{
    public partial class signup : ContentPage
    {
        public signup()
        {
            InitializeComponent();
        }

        private async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            App.GlobalUsername = username.Text;
            App.GlobalLastname = lastname.Text;
            App.GlobalPassword = password.Text;
            //App.global_dob = bd.Text;
            App.global_dob = the_date.Date;

            if(username.Text==null || lastname.Text == null || password.Text==null || the_date==null)
            {
                await DisplayAlert("Error!", "Please complete the form by filling all the details", "Return");
            }
            else
            {
                if(password.Text == password2.Text)
                {
                    bool answer = await DisplayAlert("Congrats!!", "Your account was successfully created!","Make changes to account", "Deposit Money");
                    if (answer == false)
                    {
                        await Navigation.PushAsync(new ndp());

                    }
                }
                else
                {
                    await DisplayAlert("Error!", "The passwords do not match!", "Return");

                }





            }

        }

       
    }
}
