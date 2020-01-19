using System;
using System.Collections.Generic;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using System.IO;
using System.Data.SqlClient;


namespace HC
{
    public partial class the_grid : ContentPage
    {
        public MediaFile[] mf1 = new MediaFile[4];



           



        public the_grid()
        {
            InitializeComponent();
        }

        private async void Handle_Clicked(object sender, System.EventArgs e) //picking photos
        {
            //throw new NotImplementedException();
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Photo Available", " ", "OK");

                return;
            }
            //int k = 0;

            

            for(int i=0;i<4;i++)
            {
                mf1[i] = await CrossMedia.Current.PickPhotoAsync();
                
                bool answer = await DisplayAlert("Do you want to add more pictures", " ","Yes", "No");
                if(answer==true)
                {
                    continue;
                }
                if(answer==false)
                {
                    break;
                }



            }






            if (mf1[0] == null)
                return;



            //LocalPathLabel.Text = mf.Path;

            emage1.Source = ImageSource.FromStream(() =>
            {
                return mf1[0].GetStream();
            });


            emage2.Source = ImageSource.FromStream(() =>
            {
                return mf1[1].GetStream();
            });
            emage3.Source = ImageSource.FromStream(() =>
            {
                return mf1[2].GetStream();
            });
            emage4.Source = ImageSource.FromStream(() =>
            {
                return mf1[3].GetStream();
            });


        }
    }
}
