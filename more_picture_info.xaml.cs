using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xamarin.Forms;
using System.IO;
using Stormlion.PhotoBrowser;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PinchZoomImage;





namespace HC
{
    public partial class more_picture_info : ContentPage
    {
        byte[] i1;
        byte[] i2;
        byte[] i3;
        byte[] i4;

        public more_picture_info()
        {
            InitializeComponent();

            string rome = App.for_more_image_info;
            string[] words = rome.Split('@');

            string sss = words[0];

            nn.Text = sss;

            int ron = 0;

            



            using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT DESCRIPTIONS FROM BUYERSGUIDE WHERE NM = '" + sss + "'";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {
                            dd.Text = reader.GetString(0);





                        }                     }


                }             }
            using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT PRICE FROM BUYERSGUIDE WHERE NM = '" + sss + "'";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {


                            pp.Text = Convert.ToString(reader.GetInt32(0)) + "CC";




                        }                     }


                }             }


            using (var con = new SqlConnection(App.connstr))             {                 using (var cmd = con.CreateCommand())                 {                      cmd.CommandText = @"SELECT NUMBER_PIC FROM BUYERSGUIDE WHERE NM = '" + sss + "'";                       con.Open();                     using (var reader = cmd.ExecuteReader())                     {                         while (reader.Read())                         {

                            ron = reader.GetInt32(0);


                        }                     }


                }             }
            if(ron==1)
            {
                using (var con = new SqlConnection(App.connstr))
                {
                    using (var cmd = con.CreateCommand())
                    {

                        cmd.CommandText = @"SELECT PICTURE FROM BUYERSGUIDE WHERE NM = '" + sss + "'";


                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] imgg = (byte[])(reader["PICTURE"]);
                                i1 = (byte[])(reader["PICTURE"]);



                                //ii.Source = ImageSource.FromStream(() => new MemoryStream(imgg));

                                emage1.Source = ImageSource.FromStream(() => new MemoryStream(imgg));
                        



                            }
                        }


                    }
                }
            }

            if(ron==2)
            {
                using (var con = new SqlConnection(App.connstr))
                {
                    using (var cmd = con.CreateCommand())
                    {

                        cmd.CommandText = @"SELECT PICTURE,PIIC1 FROM BUYERSGUIDE WHERE NM = '" + sss + "'";


                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] imgg = (byte[])(reader["PICTURE"]);
                                byte[] imgg1 = (byte[])(reader["PIIC1"]);

                                i1 = (byte[])(reader["PICTURE"]);
                                i2  = (byte[])(reader["PIIC1"]);


                                //ii.Source = ImageSource.FromStream(() => new MemoryStream(imgg));

                                emage1.Source = ImageSource.FromStream(() => new MemoryStream(imgg));
                                emage2.Source = ImageSource.FromStream(() => new MemoryStream(imgg1));
                              



                            }
                        }


                    }
                }
            }

            if(ron==3)
            {
                using (var con = new SqlConnection(App.connstr))
                {
                    using (var cmd = con.CreateCommand())
                    {

                        cmd.CommandText = @"SELECT PICTURE,PIIC1,PIIC2 FROM BUYERSGUIDE WHERE NM = '" + sss + "'";


                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] imgg = (byte[])(reader["PICTURE"]);
                                byte[] imgg1 = (byte[])(reader["PIIC1"]);
                                byte[] imgg2 = (byte[])(reader["PIIC2"]);

                                i1 = (byte[])(reader["PICTURE"]);
                                i2 = (byte[])(reader["PIIC1"]);
                                i3 = (byte[])(reader["PIIC2"]);



                                //ii.Source = ImageSource.FromStream(() => new MemoryStream(imgg));

                                emage1.Source = ImageSource.FromStream(() => new MemoryStream(imgg));
                                emage2.Source = ImageSource.FromStream(() => new MemoryStream(imgg1));
                                emage3.Source = ImageSource.FromStream(() => new MemoryStream(imgg2));



                            }
                        }


                    }
                }
            }

            if(ron==4)
            {
                using (var con = new SqlConnection(App.connstr))
                {
                    using (var cmd = con.CreateCommand())
                    {

                        cmd.CommandText = @"SELECT PICTURE,PIIC1,PIIC2,PIIC3 FROM BUYERSGUIDE WHERE NM = '" + sss + "'";


                        con.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] imgg = (byte[])(reader["PICTURE"]);
                                byte[] imgg1 = (byte[])(reader["PIIC1"]);
                                byte[] imgg2 = (byte[])(reader["PIIC2"]);
                                byte[] imgg3 = (byte[])(reader["PIIC3"]);

                                i1 = (byte[])(reader["PICTURE"]);
                                i2 = (byte[])(reader["PIIC1"]);
                                i3 = (byte[])(reader["PIIC2"]);
                                i4 = (byte[])(reader["PIIC3"]);


                                //ii.Source = ImageSource.FromStream(() => new MemoryStream(imgg));

                                emage1.Source = ImageSource.FromStream(() => new MemoryStream(imgg));
                                emage2.Source = ImageSource.FromStream(() => new MemoryStream(imgg1));
                                emage3.Source = ImageSource.FromStream(() => new MemoryStream(imgg2));
                                emage4.Source = ImageSource.FromStream(() => new MemoryStream(imgg3));



                            }
                        }


                    }
                }
            }
            
        }
      

        private async void Handle_Clicked_1(object sender, System.EventArgs e) //image 1
        {
            //throw new NotImplementedException();
            App.the_image = i1;
            await Navigation.PushAsync(new imageviewer());

        }

        private async void Handle_Clicked(object sender, System.EventArgs e) //image 2
        {
            //throw new NotImplementedException();
            App.the_image = i2;
            await Navigation.PushAsync(new imageviewer());

        }

        private async void Handle_Clicked_2(object sender, System.EventArgs e) //image 3
        {
            //throw new NotImplementedException();
            App.the_image = i3;
            await Navigation.PushAsync(new imageviewer());

        }

        private async void Handle_Clicked_3(object sender, System.EventArgs e) //image 4
        {
            //throw new NotImplementedException();
            App.the_image = i4;
            await Navigation.PushAsync(new imageviewer());

        }
    }
}
