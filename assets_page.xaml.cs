using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xamarin.Forms;
using System.IO;
using System.Linq;
using Xamarin.Forms.Extended;
using System.Collections.ObjectModel;
using System.Threading.Tasks;



namespace HC
{
    public partial class assets_page : ContentPage
    {
        //private const int PageSize = 20;
        bool isLoading;
        //Page page;
        int num_load = 0;
        int id_toload_l = 1;
        int id_toload_u = 0;

        int total_count = 0;

        public IList<data2> people3 { get; private set; }


        public assets_page()
        {

            InitializeComponent();

            name_label.Text = App.GlobalUsername;
            string ln = string.Empty;
            DateTime doo = default(DateTime);

            using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT lastname,dateofbirth FROM BANKINGUSERS WHERE names='" + App.GlobalUsername + "' ";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {
                            //Console.WriteLine(reader.GetString(0));
                            ln = reader.GetString(0);                             doo = reader.GetDateTime(1);                           }                     }
                    //int insertedproductid = (int)cmd.ExecuteScalar();
                    //cmd.ExecuteNonQuery();

                }             }
            string l = doo.ToString();
            string[] ll = l.Split(' ');

            lastname_label.Text = ln;
            dob_label.Text = ll[0];


            ballance_label.Text = App.GlobalCreditCoins + "CC";
            ballance_dollar.Text = (App.GlobalCreditCoins * 1967.35) + "$";


            using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT COUNT(*) FROM BUYERSGUIDE WHERE OWN='"+ App.GlobalUsername + "';";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {
                            //Console.WriteLine(reader.GetString(0));
                            total_count = reader.GetInt32(0);                           }                     }
                }
            }

            people3 = new List<data2>();

            using (var conn = new SqlConnection(App.connstr))             {                 using (var command = conn.CreateCommand())                 {                     command.CommandText = @"SELECT * FROM BUYERSGUIDE WHERE OWN='"+App.GlobalUsername+"'";



                    conn.Open();                      using (var reader = command.ExecuteReader())                     {
                         while (reader.Read())                         {

                            byte[] img2 = (byte[])(reader["PICTURE"]);

                            string des2 = reader["DESCRIPTIONS"].ToString();
                            if (des2.Length < 50)
                            {
                                people3.Add(new data2
                                {
                                    name = reader["NM"].ToString(),
                                    des = des2,

                                    img = ImageSource.FromStream(() => new MemoryStream(img2)),

                                    price = (int)reader["PRICE"]


                                });
                            }
                            else
                            {
                                string des3 = des2.Substring(0, 49);
                                des3 = des3 + " Click Here to read more";


                                people3.Add(new data2
                                {
                                    name = reader["NM"].ToString(),
                                    des = des3,

                                    img = ImageSource.FromStream(() => new MemoryStream(img2)),

                                    price = (int)reader["PRICE"]


                                });
                            }




                        }


                        BindingContext = this;
                        mylist3.ItemsSource = people3;
                     }                  }             }



        }













        private async Task loadMoreData()
        {

            isLoading = true;
            //page.Title = "Loading";

            //hello.Text = "loading....";


            id_toload_l = num_load * 3;


            int id2 = id_toload_l + 1;
            int id3 = id2 + 1;
           



            if (id_toload_l > total_count)
            {
                //hello.Text = "Happy Buying!!";
                return;
            }


            id_toload_u = (num_load + 1) * 3;




            //lol.IsEnabled = true;
            //lol.IsRunning = true;
            //lol.IsVisible = true;

            Device.StartTimer(TimeSpan.FromSeconds(2), () => {

                using (var conn = new SqlConnection(App.connstr))
                {
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = @"SELECT * FROM BUYERSGUIDE WHERE OWN='" + App.GlobalUsername + "' AND ID IN ( " + id_toload_l + " , " + id2 + " , " + id3 + ")"; 




                        conn.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            int i = 0;

                            while (reader.Read())
                            {

                                byte[] img2 = (byte[])(reader["PICTURE"]);

                                string des2 = reader["DESCRIPTIONS"].ToString();
                                if (des2.Length < 50)
                                {
                                    people3.Add(new data2
                                    {
                                        name = reader["NM"].ToString(),
                                        des = des2,

                                        img = ImageSource.FromStream(() => new MemoryStream(img2)),

                                        price = (int)reader["PRICE"]


                                    });
                                    i++;
                                }
                                else
                                {
                                    string des3 = des2.Substring(0, 49);
                                    des3 = des3 + " Click Here to read more";


                                    people3.Add(new data2
                                    {
                                        name = reader["NM"].ToString(),
                                        des = des3,

                                        img = ImageSource.FromStream(() => new MemoryStream(img2)),

                                        price = (int)reader["PRICE"]


                                    });
                                    i++;
                                }

                                //i++;

                                if (i >= 3)
                                {
                                    break;
                                }


                            }
                            BindingContext = this;
                            mylist3.ItemsSource = people3;

                            //var pop = people2;
                            //return pop;


                        }

                    }
                }


                //page.Title = "Done";
                //hello.Text = "Happy Buying!!";



                //lol.IsEnabled = false;
                //lol.IsRunning = false;
                //lol.IsVisible = false;
                isLoading = false;
                //stop timer
                return false;
            });

        }

    }
}
