using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Xamarin.Forms;

namespace HC
{
    public partial class ndp : ContentPage
    {
        double conversion = 1967.35;

        public ndp()
        {
            InitializeComponent();
            username_label.Text = App.GlobalUsername;
            todays_rate.Text = ("Todays conversion rate: " + conversion).ToString();
        }
        int cc = 0;
        double dollars = 0;
        int temp_cc = 0;

        //add

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            temp_cc++;
            dollars = conversion * temp_cc;
            amount_label.Text = (temp_cc + " CC").ToString();
            dollarmoney.Text = (dollars + "$").ToString();

        }

        //subtract

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            temp_cc--;
            if(temp_cc<0)
            {
                DisplayAlert("Error!", "Credit Coins cannot be negative", "Return");
                temp_cc = 0;
            }
            dollars = conversion * temp_cc;
            amount_label.Text = (temp_cc + " CC").ToString();
            dollarmoney.Text = (dollars + "$").ToString();

        }


        private async void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();

            if (temp_cc==0)
            {
                await DisplayAlert("Error!", "You cannot deposit 0 CC", "Return");


            }
            else
            {
                cc = temp_cc;
                App.GlobalCreditCoins=cc;


                using (var con = new SqlConnection(App.connstr))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO BANKINGUSERS " +
                        "(NAMES, LASTNAME, PASSWORDS,DATEOFBIRTH,BALLANCE) " +
                        "VALUES(@NAME, @LN, @PASS,@DO,@BALL)";

                        cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@LN", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@PASS", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@DO", System.Data.SqlDbType.DateTime);
                        cmd.Parameters.Add("@BALL", System.Data.SqlDbType.Int);



                        cmd.Parameters["@NAME"].Value = App.GlobalUsername;
                        cmd.Parameters["@LN"].Value = App.GlobalLastname;
                        cmd.Parameters["@PASS"].Value = App.GlobalPassword;
                        cmd.Parameters["@DO"].Value = App.global_dob;
                        cmd.Parameters["@BALL"].Value = App.GlobalCreditCoins;




                        con.Open();
                        //int insertedproductid = (int)cmd.ExecuteScalar();
                        cmd.ExecuteNonQuery();
                    }
                }                                               






                await DisplayAlert("Perfect!", "Your Credit Coins are deposited", "GO!!");
                await Navigation.PushAsync(new home());

            }

        }
    }
}
