using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xamarin.Forms;
using SQLite;
using SQLitePCL;
using HC.Model;
using System.Data.SQLite;
using System.Net;
using System.Linq;
using System.Data;
using SQLiteCommand = SQLite.SQLiteCommand;
using Xamarin.Forms.PlatformConfiguration;
using Microsoft.AppCenter;

namespace HC
{
    public partial class login : ContentPage
    {
        //private SQLite.SQLiteConnection konn;
        //public st1 st;

        
        public login()
        {
            InitializeComponent();
            //konn = DependencyService.Get<ISQLite>().GetConnection();
            //konn.CreateTable<st1>();



        }

        private async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            //string connstr = "Server=tcp:hediacredit.database.windows.net,1433;Initial Catalog=HediaCredit;Persist Security Info=False;User ID=harsh;Password=Pan2001Har;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";             string on = "empty";             using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT NAMES FROM BANKINGUSERS WHERE NAMES='" + username.Text + "' ";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {
                            //Console.WriteLine(reader.GetString(0));
                            on = reader.GetString(0);                           }                     }
                    //int insertedproductid = (int)cmd.ExecuteScalar();
                    //cmd.ExecuteNonQuery();

                }             }
            bool eq = string.Equals(username.Text, on);

            if(eq==false)
            {
                await DisplayAlert("Error!", "Either the Username or Password is incorrect", "Return");

            }
            if(eq==true)
            {
                string of = "empty";
                using (var con = new SqlConnection(App.connstr))
                {
                    using (var cmd = con.CreateCommand())
                    {

                        cmd.CommandText = @"SELECT PASSWORDS FROM BANKINGUSERS WHERE NAMES='" + username.Text + "' ";


                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine(reader.GetString(0));
                                of = reader.GetString(0);


                            }
                        }
                        //int insertedproductid = (int)cmd.ExecuteScalar();
                        //cmd.ExecuteNonQuery();

                    }
                }
                bool eql = string.Equals(password.Text, of);
                if(eql==false)
                {
                    await DisplayAlert("Error!", "Either the Username or Password is incorrect", "Return");

                }
                if(eql==true)
                {
                    App.GlobalUsername = username.Text;
                    App.GlobalPassword = password.Text;


                    int bbl = 0;
                    using (var con = new SqlConnection(App.connstr))
                    {
                        using (var cmd = con.CreateCommand())
                        {

                            cmd.CommandText = @"SELECT BALLANCE FROM BANKINGUSERS WHERE NAMES='" + username.Text + "' ";


                            con.Open();
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //Console.WriteLine(reader.GetString(0));
                                    bbl = reader.GetInt32(0);


                                }
                            }
                            //int insertedproductid = (int)cmd.ExecuteScalar();
                            //cmd.ExecuteNonQuery();

                        }
                    }
                    App.GlobalCreditCoins = bbl;

                   

                    await Navigation.PushAsync(new home());

                }

            }



            //if (username.Text == null || password.Text == null)
            //{
            //   DisplayAlert("Error!", "Either the Username or Password is incorrect", "Return");
            //}

        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            /*
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST               // Get the IP               string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            //string devid = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            //string devid = Android.OS.Build.Serial;
            //string devid =  Settings.Secure.GetString(Forms.Context.ContentResolver, Settings.Secure.AndroidId);

    */
            
            string HostName = Dns.GetHostName();
            IPAddress[] ipaddress = Dns.GetHostAddresses(HostName);
            string myip = string.Empty;
            //Console.WriteLine("IP Address of Machine is");
            foreach (IPAddress ip in ipaddress)
            {
                myip = ip.ToString();
            }



            string oon = "empty";
            using (var con = new SqlConnection(App.connstr))
            {
                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = @"SELECT NAMES FROM BANKINGUSERS WHERE NAMES='" + username.Text + "' ";


                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine(reader.GetString(0));
                            oon = reader.GetString(0);


                        }
                    }
                    //int insertedproductid = (int)cmd.ExecuteScalar();
                    //cmd.ExecuteNonQuery();

                }
            }
            bool eq = string.Equals(username.Text, oon);

            if (eq == false)
            {
                DisplayAlert("Error!", "Either the Username or Password is incorrect", "Return");

            }
            else
            {
                string of = "empty";
                using (var con = new SqlConnection(App.connstr))
                {
                    using (var cmd = con.CreateCommand())
                    {

                        cmd.CommandText = @"SELECT PASSWORDS FROM BANKINGUSERS WHERE NAMES='" + username.Text + "' ";


                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine(reader.GetString(0));
                                of = reader.GetString(0);


                            }
                        }
                        //int insertedproductid = (int)cmd.ExecuteScalar();
                        //cmd.ExecuteNonQuery();

                    }
                }
                bool eql = string.Equals(password.Text, of);
                if (eql == false)
                {
                    DisplayAlert("Error!", "Either the Username or Password is incorrect", "Return");

                }
                else
                {
                    remember.BackgroundColor = Color.Green;
                    remember.TextColor = Color.White;

                    AppCenter.SetUserId(username.Text);

                    using (var con = new SqlConnection(App.connstr))
                    {
                        using (var cmd = con.CreateCommand())
                        {
                            cmd.CommandText = @"INSERT INTO REMEMBER " +
                                "(NAMES, IP, CONDITION) " +
                                "VALUES(@NAME, @IIP, @CONDIT)";

                            cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar);
                            cmd.Parameters.Add("@IIP", System.Data.SqlDbType.VarChar);
                            cmd.Parameters.Add("@CONDIT", System.Data.SqlDbType.Int);





                            cmd.Parameters["@NAME"].Value = username.Text;
                            cmd.Parameters["@IIP"].Value = myip;
                            cmd.Parameters["@CONDIT"].Value = 1;





                            con.Open();
                            //int insertedproductid = (int)cmd.ExecuteScalar();
                            cmd.ExecuteNonQuery();



                        }
                    }
                }







            }

                 
        }
    }
}
