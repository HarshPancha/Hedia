using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using System.Net;
using Xamarin.Essentials;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;



namespace HC
{
    public partial class App : Application
    {
        public static string connstr = "Enter your connection string!!";
        public static string GlobalUsername = string.Empty; 
        public static string GlobalPassword = string.Empty;

        public static DateTime global_dob;

        public static string GlobalLastname = string.Empty;

        public static int GlobalCreditCoins = 0;

        public static string globalto = string.Empty;

        public static string globalfrom = string.Empty;

        public static int globalm = 0;

        public static DateTime globaltime;

        public static string every = string.Empty;

        public static int statuss = 0;

        public static string for_more_image_info = string.Empty;

        public static byte[] the_image;

        public static bool if_internet;


        public App()
        {
            InitializeComponent();
            /*
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            // Get the IP  
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            */
            // public string Id = UIDevice.CurrentDevice.IdentifierForVendor.AsString();

            //UINib.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            //public string aaa => UIDevice.CurrentDevice.IdentifierForVendor.ToString();
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Connection to internet is available
                if_internet = true;
                try
                {
                    string HostName = Dns.GetHostName();
                    IPAddress[] ipaddress = Dns.GetHostAddresses(HostName);
                    string myIP = string.Empty;
                    //Console.WriteLine("IP Address of Machine is");
                    foreach (IPAddress ip in ipaddress)
                    {
                        myIP = ip.ToString();
                    }


                    int on = 0;
                    using (var con = new SqlConnection(connstr))
                    {
                        using (var cmd = con.CreateCommand())
                        {

                            cmd.CommandText = @"SELECT CONDITION FROM REMEMBER WHERE IP='" + myIP + "' ";


                            con.Open();
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //Console.WriteLine(reader.GetString(0));
                                    on = reader.GetInt32(0);


                                }
                            }
                            //int insertedproductid = (int)cmd.ExecuteScalar();
                            //cmd.ExecuteNonQuery();

                        }
                    }
                    if (on == 0)
                    {
                        MainPage = new NavigationPage(new MainPage());
                    }

                    if (on == 1)
                    {
                        string orange = string.Empty;

                        using (var con = new SqlConnection(connstr))
                        {
                            using (var cmd = con.CreateCommand())
                            {

                                cmd.CommandText = @"SELECT NAMES FROM REMEMBER WHERE IP='" + myIP + "' ";


                                con.Open();
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        //Console.WriteLine(reader.GetString(0));
                                        orange = reader.GetString(0);


                                    }
                                }
                                //int insertedproductid = (int)cmd.ExecuteScalar();
                                //cmd.ExecuteNonQuery();

                            }
                        }

                        GlobalUsername = orange;
                        AppCenter.SetUserId(GlobalUsername);


                        using (var conn = new SqlConnection(connstr))
                        {
                            using (var command = conn.CreateCommand())
                            {
                                command.CommandText = @"SELECT LASTNAME,PASSWORDS,BALLANCE FROM BANKINGUSERS WHERE NAMES='" + orange + "'";
                                conn.Open();

                                using (var reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        //Console.WriteLine(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetDateTime(3) + " " + reader.GetInt32(4));
                                        GlobalLastname = reader.GetString(0);
                                        GlobalPassword = reader.GetString(1);
                                        GlobalCreditCoins = reader.GetInt32(2);
                                    }
                                }

                            }
                        }






                        MainPage = new NavigationPage(new home());

                        
                       


                    }
                }
                catch (Exception)
                {
                    MainPage = new NavigationPage(new MainPage());

                }
            }

            else
            {
                if_internet = false;
                MainPage = new NavigationPage(new MainPage());
            }













        }

        protected override void OnStart()
        {
            // Handle when your app starts

            //for android
            AppCenter.Start("bcab69bf-e093-4822-b14a-9509e7272d3b", typeof(Push));

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
