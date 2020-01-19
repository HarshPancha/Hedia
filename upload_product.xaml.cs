using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;

namespace HC
{
    public partial class upload_product : ContentPage
    {
        public upload_product()
        {
            InitializeComponent();
        }



        void Handle_Clicked(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            //var something;
            using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT PICTURE FROM BUYERSGUIDE WHERE NM = '"+the_entry.Text+"'";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {
                            byte[] imgg = (byte[])(reader["PICTURE"]);

                            uploaded_image.Source = ImageSource.FromStream(() => new MemoryStream(imgg));





                        }                     }


                }             }
        }
    }
}
