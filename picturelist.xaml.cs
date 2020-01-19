using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HC
{
    
    public partial class picturelist : ContentPage
    {

        private const int PageSize = 20;
        bool isLoading;
        //Page page;
        int num_load = 0;
        int id_toload_l = 0;
        int id_toload_u = 0;

        int total_count = 0;

        public ObservableCollection<data2> people2 { get; private set; }
        //ilist
        public picturelist()
        {
            

            InitializeComponent();
            InitSearchBar();

            using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT COUNT(*) FROM BUYERSGUIDE;";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {
                            //Console.WriteLine(reader.GetString(0));
                            total_count = reader.GetInt32(0);                           }                     }
                }
            }


            people2 = new ObservableCollection<data2>();

            

            mylist2.ItemsSource = people2;

            mylist2.ItemAppearing += (sender, e) =>
            {
                if (isLoading || people2.Count == 0)
                    return;

                //hit bottom!
                if (e.Item.ToString() == people2[people2.Count-1].ToString())
                {
                    
                    loadMoreData();
                    num_load++;
                   
                }
            };

            loadMoreData();
            num_load++;
            //                if (e.Item.ToString() == people2[people2.Count - 1].ToString())








        }

        
       


        private async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();

            App.for_more_image_info = mylist2.SelectedItem.ToString();

            await Navigation.PushAsync(new more_picture_info());


        }

        private async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            await Navigation.PushAsync(new buypage());

        }

        void InitSearchBar()
        {
            search.TextChanged += (s, e) => FilterItem(search.Text);
            search.SearchButtonPressed += (s, e) => FilterItem(search.Text);

        }

        private void FilterItem(string filter)
        {
            mylist2.BeginRefresh();
            if(string.IsNullOrWhiteSpace(filter))
            {
                mylist2.ItemsSource = people2;

            }
            else
            {
                mylist2.ItemsSource = people2.Where(x => x.name.ToLower().Contains(filter.ToLower()));

            }
            mylist2.EndRefresh();
        }

        private async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            await Navigation.PushAsync(new home());

        }

        private async Task loadMoreData()
        {

            isLoading = true;
            //page.Title = "Loading";

            hello.Text = "loading....";


            id_toload_l = num_load * 5;

            
            int id2 = id_toload_l + 1;
            int id3 = id2 + 1;
            int id4 = id3 + 1;
            int id5 = id4 + 1;
            int id6 = id5 + 1;
            int id7 = id6 + 1;



            if (id_toload_l > total_count)
            {
                hello.Text = "Happy Buying!!";
                return;
            }


            id_toload_u = (num_load + 1) * 5;

            


            lol.IsEnabled = true;
            lol.IsRunning = true;
            lol.IsVisible = true;

            Device.StartTimer(TimeSpan.FromSeconds(2), () => {
                
                    using (var conn = new SqlConnection(App.connstr))
                    {
                        using (var command = conn.CreateCommand())
                        {
                            command.CommandText = @"SELECT * FROM BUYERSGUIDE WHERE ID IN ( "+id_toload_l+" , "+id2+ " , " + id3 + " ," + id4 + " ," + id5 + ")";



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
                                        people2.Add(new data2
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


                                        people2.Add(new data2
                                        {
                                            name = reader["NM"].ToString(),
                                            des = des3,

                                            img = ImageSource.FromStream(() => new MemoryStream(img2)),

                                            price = (int)reader["PRICE"]


                                        });
                                    }

                                    i++;

                                    if (i >=5)
                                    {
                                      break;
                                    }


                                }
                                BindingContext = this;
                                mylist2.ItemsSource = people2;

                                //var pop = people2;
                                //return pop;


                            }

                        }
                    }
                

                //page.Title = "Done";
                hello.Text = "Happy Buying!!";

                

                lol.IsEnabled = false;
                lol.IsRunning = false;
                lol.IsVisible = false;
                isLoading = false;
                //stop timer
                return false;
            });

        }



    }

    
    
}
