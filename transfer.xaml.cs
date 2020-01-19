using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace HC
{
    public partial class transfer : ContentPage
    {
        public transfer()
        {
            InitializeComponent();
            username_label.Text = App.GlobalUsername;
            ballance.Text = ("Ballance: " + App.GlobalCreditCoins + " CC").ToString();

        }

        public async void Handle_Clicked(object sender, System.EventArgs e)
        {
            int x = 0;             int y = 0;             int z = 0;             int a = 0;
            //throw new NotImplementedException();
            string on = "empty";
            int newmoney = Convert.ToInt32(amtsent.Text);
             using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT NAMES FROM BANKINGUSERS WHERE NAMES='" + receiver.Text + "' ";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {
                            //Console.WriteLine(reader.GetString(0));
                            on = reader.GetString(0);                           }                     }
                    //int insertedproductid = (int)cmd.ExecuteScalar();
                    //cmd.ExecuteNonQuery();

                }             }             bool eq = string.Equals(receiver.Text, on);             if (eq == false)             {
                DisplayAlert("Error!", "The receiver does not exist", "Return");
              }
            if(eq==true)
            {


                using (var con = new SqlConnection(App.connstr))                 {                     using (var cmd = con.CreateCommand())                     {                          cmd.CommandText = @"SELECT BALLANCE FROM BANKINGUSERS WHERE NAMES='"+App.GlobalUsername+ "' ";                           con.Open();                         using (var reader = cmd.ExecuteReader())                         {                             while (reader.Read())                             {                                 Console.WriteLine(reader.GetInt32(0));                                 x = reader.GetInt32(0);                                                          }                         }                     //int insertedproductid = (int)cmd.ExecuteScalar();                     //cmd.ExecuteNonQuery();                      }                 }                 using (var con = new SqlConnection(App.connstr))                 {                     using (var cmd = con.CreateCommand())                     {                          cmd.CommandText = @"SELECT BALLANCE FROM BANKINGUSERS WHERE NAMES='" + receiver.Text + "' ";                           con.Open();                         using (var reader = cmd.ExecuteReader())                         {                             while (reader.Read())                             {                                 Console.WriteLine(reader.GetInt32(0));                                 y = reader.GetInt32(0);                               }                         }                     //int insertedproductid = (int)cmd.ExecuteScalar();                     //cmd.ExecuteNonQuery();                      }                 }                  a = x - newmoney;                 z = y + newmoney;

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

                using (var con = new SqlConnection(App.connstr))
                {
                    using (var cmd = con.CreateCommand())
                    {

                        cmd.CommandText = @"UPDATE BANKINGUSERS SET BALLANCE=" + z + " WHERE NAMES='" + receiver.Text + "' ";


                        con.Open();

                        //int insertedproductid = (int)cmd.ExecuteScalar();
                        cmd.ExecuteNonQuery();

                    }
                }
                App.GlobalCreditCoins = a;

                using (var con = new SqlConnection(App.connstr))
                {                     using (var cmd = con.CreateCommand())                     {
                        cmd.CommandText = @"INSERT INTO HISTORY " +
                        "(SENDER,RECEIVER,AMT) " +
                        "VALUES(@S, @R, @A)";
                        cmd.Parameters.Add("@S", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@R", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@A", System.Data.SqlDbType.Int);



                        cmd.Parameters["@S"].Value = App.GlobalUsername;
                        cmd.Parameters["@R"].Value = receiver.Text;
                        cmd.Parameters["@A"].Value = amtsent.Text;





                        con.Open();
                        //int insertedproductid = (int)cmd.ExecuteScalar();
                        cmd.ExecuteNonQuery();


                    }
                }                        

                //NotifyPropertyChanged();
                //OnPropertyChanged(new PropertyChangedEventArgs(App.GlobalCreditCoins);
                //this.OnPropertyChanged("IsValid");
                await DisplayAlert("Transaction Successful!!", " ", "Return");

                await Navigation.PushAsync(new transfer());
















            }
        }

        private async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            await Navigation.PushAsync(new home());


        }
    }
}
