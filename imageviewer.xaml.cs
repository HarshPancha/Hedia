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
    public partial class imageviewer : CarouselPage
    {
        public imageviewer()
        {
            

            InitializeComponent();
            Thickness padding;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                    padding = new Thickness(0, 40, 0, 0);
                    break;
                default:
                    padding = new Thickness();
                    break;
            }

            int how_many = 0;

            string rome = App.for_more_image_info;
            string[] words = rome.Split('@');

            string sss = words[0];

            //lol.Source = ImageSource.FromStream(() => new MemoryStream(App.the_image));

            using (var con = new SqlConnection(App.connstr))
            {
                using (var cmd = con.CreateCommand())
                {

                    cmd.CommandText = @"SELECT NUMBER_PIC FROM BUYERSGUIDE WHERE NM = '" + sss + "'";


                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            how_many = reader.GetInt32(0);



                        }
                    }


                }
            }

            
            

            

            if (how_many == 1)
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

                                var one = new ContentPage
                                {
                                    Padding = padding,
                                    BackgroundColor = Color.Black,

                                    
                                    Content = new StackLayout
                                    {
                                        
                                        Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content = new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,

                                                }
                                            }

                                            



                                        }
                                    }
                                };
                                Children.Add(one);








                            }
                        }


                    }


                }



            }
            if (how_many == 2)
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

                                var one = new ContentPage
                                {
                                    Padding = padding,
                                    BackgroundColor = Color.Black,
                                    Content = new StackLayout
                                    {
                                        Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content=new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,
                                                }
                                            }
                                            



                                        }
                                    }
                                };

                                var two = new ContentPage
                                {
                                    Padding = padding,
                                    BackgroundColor = Color.Black,
                                    Content = new StackLayout
                                    {
                                        Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content=new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg1)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,
                                                }
                                            }
                                            



                                        }
                                    }
                                };
                                if(imgg.SequenceEqual(App.the_image))
                                {
                                    Children.Add(one);
                                    Children.Add(two);
                                }
                                if(imgg1.SequenceEqual(App.the_image))
                                {
                                    Children.Add(two);
                                    Children.Add(one);
                                }
                                









                            }
                        }


                    }
                }
            }




            if (how_many == 3)
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

                                    var one = new ContentPage
                                    {
                                        Padding = padding,
                                        BackgroundColor = Color.Black,

                                        Content = new StackLayout
                                        {
                                            Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content= new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,
    
                                                }
                                            }
                                           



                                        }
                                        }
                                    };
                                    var two = new ContentPage
                                    {
                                        Padding = padding,
                                        BackgroundColor = Color.Black,

                                        Content = new StackLayout
                                        {
                                            Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content = new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg1)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,

                                                }
                                            }
                                            



                                        }
                                        }
                                    };

                                    var three = new ContentPage
                                    {
                                        Padding = padding,
                                        BackgroundColor = Color.Black,

                                        Content = new StackLayout
                                        {
                                            Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content=new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg2)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,

                                                }
                                            }
                                            



                                        }
                                        }
                                    };
                                if(imgg.SequenceEqual(App.the_image))
                                {
                                    Children.Add(one);
                                    Children.Add(two);
                                    Children.Add(three);
                                }
                                if(imgg1.SequenceEqual(App.the_image))
                                {
                                    Children.Add(two);
                                    Children.Add(one);
                                    Children.Add(three);
                                }
                                    
                                if(imgg2.SequenceEqual(App.the_image))
                                {
                                    Children.Add(three);
                                    Children.Add(two);
                                    Children.Add(one);
                                }
                                }
                            }


                        }
                    }
            }

            if (how_many == 4)
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



                                var one = new ContentPage
                                {
                                    Padding = padding,
                                    BackgroundColor = Color.Black,

                                    Content = new StackLayout
                                    {
                                        Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content = new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,

                                                }
                                            }
                                            



                                        }
                                    }
                                };
                                var two = new ContentPage
                                {
                                    Padding = padding,
                                    BackgroundColor = Color.Black,

                                    Content = new StackLayout
                                    {
                                        Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content= new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg1)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,

                                                }
                                            }
                                            



                                        }
                                    }
                                };
                                var three = new ContentPage
                                {
                                    Padding = padding,
                                    BackgroundColor = Color.Black,

                                    Content = new StackLayout
                                    {
                                        Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content = new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg2)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,

                                                }
                                            }
                                            



                                        }
                                    }
                                };

                                var four = new ContentPage
                                {
                                    Padding = padding,
                                    BackgroundColor = Color.Black,

                                    Content = new StackLayout
                                    {
                                        Children =
                                        {
                                            new PinchZoom
                                            {
                                                Content = new Image
                                                {
                                                    Source =  ImageSource.FromStream(() => new MemoryStream(imgg3)),
                                                    VerticalOptions = LayoutOptions.Center,
                                                    HeightRequest = 669,

                                                }
                                            }
                                            



                                        }
                                    }
                                };
                                if(imgg.SequenceEqual(App.the_image))
                                {
                                    Children.Add(one);
                                    Children.Add(two);
                                    Children.Add(three);
                                    Children.Add(four);
                                }
                                if(imgg1.SequenceEqual(App.the_image))
                                {
                                    Children.Add(two);
                                    Children.Add(one);
                                    Children.Add(three);
                                    Children.Add(four);
                                }
                                if (imgg2.SequenceEqual(App.the_image))
                                {
                                    Children.Add(three);
                                    Children.Add(two);
                                    Children.Add(one);
                                    Children.Add(four);
                                }
                                if (imgg3.SequenceEqual(App.the_image))
                                {
                                    Children.Add(four);
                                    Children.Add(two);
                                    Children.Add(one);
                                    Children.Add(three);
                                }
                               





                            }
                        }




                    }
                }
            }



       
                                    

               
                                   


            



        }
    }
}
