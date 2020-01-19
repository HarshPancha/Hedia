using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Xamarin.Forms;

namespace HC
{
    public partial class moredeposit : ContentPage
    {
        public moredeposit()
        {
            InitializeComponent();
            username_label.Text = App.GlobalUsername;
            ballance.Text = ("Ballance: " + App.GlobalCreditCoins + " CC").ToString();

        }

        private async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            int newmoney = Convert.ToInt32(amtdep.Text);
            int y = 0;
            int a;
            using (var con = new SqlConnection(App.connstr))
            {
                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = @"SELECT BALLANCE FROM BANKINGUSERS WHERE NAMES='" + App.GlobalUsername + "' ";


                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader.GetInt32(0));
                            y = reader.GetInt32(0);


                        }
                    }
                    //int insertedproductid = (int)cmd.ExecuteScalar();
                    //cmd.ExecuteNonQuery();

                }
            }

            a = newmoney + y;

            using (var con = new SqlConnection(App.connstr))
            {
                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = @"UPDATE BANKINGUSERS SET BALLANCE=" + a + " WHERE NAMES='" + App.GlobalUsername + "' ";


                    con.Open();

                    //int insertedproductid = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();



                }
            }
            App.GlobalCreditCoins = a;
            await DisplayAlert("Deposit Successful!!", " ", "Return");
            await Navigation.PushAsync(new moredeposit());


        }

        private async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            await Navigation.PushAsync(new home());

        }
    }
}
