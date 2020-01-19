using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;

namespace HC
{
    public partial class home : TabbedPage
    {
        public home()
        {
            InitializeComponent();
            username_label.Text = App.GlobalUsername;
        }


         private async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            bool answer = await DisplayAlert("", "Are you sure you want to log out", "Yes", "No");
            if(answer==true)
            {
                 using (var con = new SqlConnection(App.connstr))                 {                     using (var cmd = con.CreateCommand())                     {                          cmd.CommandText = @"DELETE FROM REMEMBER WHERE NAMES='"+App.GlobalUsername+"' ";                           con.Open();                          //int insertedproductid = (int)cmd.ExecuteScalar();                         cmd.ExecuteNonQuery();                      }                 }

                await Navigation.PushAsync(new MainPage());

            }
        }
    }
}
