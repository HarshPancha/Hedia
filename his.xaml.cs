using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Xamarin.Forms;

namespace HC
{
    public partial class his : ContentPage
    {

        /*
        public List<data> people = new List<data>()
            {
                new data{sender="Harsh",receiver="Sam",amt=9},
                new data{sender="Ron",receiver="Roger",amt=10},
                new data{sender="Nike",receiver="Harsh",amt=10}

            };
            */         
              
        public IList<data> people { get; private set; }

        public his()
        {
            InitializeComponent();
            username_label.Text = App.GlobalUsername;
            ballance.Text = ("Ballance: " + App.GlobalCreditCoins + " CC").ToString();

            people = new List<data>();

           


            //mylist.ItemsSource = people;



            using (var conn = new SqlConnection(App.connstr))             {                 using (var command = conn.CreateCommand())                 {                     command.CommandText = @"SELECT * FROM HISTORY WHERE SENDER='"+App.GlobalUsername+ "' OR RECEIVER='"+App.GlobalUsername+"'";
                                        conn.Open();                      using (var reader = command.ExecuteReader())                     {                         while (reader.Read())                         {
                            people.Add(new data
                            {
                                sender = reader["SENDER"].ToString(),
                                receiver = reader["RECEIVER"].ToString(),
                                amt = (int)reader["AMT"],
                                tim = (DateTime)reader["TIM"]
                            });


                         }
                        BindingContext = this;
                        mylist.ItemsSource = people;
                     }                  }             }


        }

        private async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();

            App.every = mylist.SelectedItem.ToString();




            await Navigation.PushAsync(new moreinfo());

        }


        private async void Handle_Clicked(object sender, System.EventArgs e) //this is for swiping
        {
            //throw new NotImplementedException();

            var mi = ((MenuItem)sender);


            App.every = mi.CommandParameter.ToString();




            await Navigation.PushAsync(new moreinfo());

        }
    }
}
