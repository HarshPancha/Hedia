using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Xamarin.Forms;

namespace HC
{
    public partial class moreinfo : ContentPage
    {
        public moreinfo()
        {
            InitializeComponent();
            //sen.Text = App.every;
            string rome = App.every;
            string[] words = rome.Split(' ');

            string sss = words[0];
            string rrr = words[1];
            int a = Convert.ToInt32(words[2]);
            int b = a * 19700;
            string lll = "empty";
            string llll = string.Empty;             using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT LASTNAME FROM BANKINGUSERS WHERE NAMES='" + sss + "' ";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {
                            //Console.WriteLine(reader.GetString(0));
                            lll = reader.GetString(0);                           }                     }
                }
            }

            using (var con = new SqlConnection(App.connstr))
            {
                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = @"SELECT LASTNAME FROM BANKINGUSERS WHERE NAMES='" + rrr + "' ";


                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Console.WriteLine(reader.GetString(0));
                            llll = reader.GetString(0);


                        }
                    }
                }
            }



            send.Text = ("Sender: " + words[0] + " " + lll).ToString();
            recc.Text = ("Receiver: " + words[1] + " " + llll).ToString();
            am.Text = (words[2]+"CC"+" , "+ b + "$").ToString();
            d.Text = ("Date: " +  words[3]).ToString();
            t.Text = ("Time: " + words[4] + " " + words[5]).ToString();
        }
    }
}
