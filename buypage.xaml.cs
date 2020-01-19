using System;
using System.Collections.Generic;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using System.IO;
using System.Data.SqlClient;


namespace HC
{
    public partial class buypage : ContentPage
    {
        public MediaFile mf;
        public MediaFile[] mf1 = new MediaFile[4];
        string[] tom = new string[4];
        int l = 0;

        bool abs = false;

        public buypage()
        {
            InitializeComponent();
            username_label.Text = App.GlobalUsername;
            abs = false;
            //ballance.Text = ("Ballance: " + App.GlobalCreditCoins + " CC").ToString();


        }

        private async void Handle_Clicked(object sender, System.EventArgs e) //to pick photo
        {
            //BindingSource so = new BindingSource();


            //throw new NotImplementedException();
            //throw new NotImplementedException();
            await CrossMedia.Current.Initialize();


            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Photo Available", " ", "OK");

                return;
            }
            for(int k=0;k<4;k++)
            {
                tom[k] = null;
            }


            for (int i = 0; i < 4; i++)
            {

                mf1[i] = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small
                });
               
                

                


                tom[i] = mf1[i].Path;
                l++;

                bool answer = await DisplayAlert("Do you want to add more pictures","Picture "+l.ToString()+" of 4 added", "Yes", "No");
                    //ItemsSource = "{Binding people2}"
                if (answer == true)
                {
                    continue;
                }
                if (answer == false)
                {
                    break;
                }



            }






            if (mf1[0] == null && mf1[1] == null && mf1[2] == null && mf1[3] == null)
                return;


            //LocalPathLabel.Text = mf.Path;
            

            emage1.Source = ImageSource.FromStream(() =>
            {
                return mf1[0].GetStream();
            });


            emage2.Source = ImageSource.FromStream(() =>
            {
                return mf1[1].GetStream();
            });
            emage3.Source = ImageSource.FromStream(() =>
            {
                return mf1[2].GetStream();
            });
            emage4.Source = ImageSource.FromStream(() =>
            {
                return mf1[3].GetStream();
            });



        }

        private async void Handle_Clicked_1(object sender, System.EventArgs e) //to take photo
        {
            //throw new NotImplementedException();
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            for (int k = 0; k < 4; k++)
            {
                tom[k] = null;
            }

            for(int i=0;i<4;i++)
            {
                mf1[i] = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Pictures",
                    PhotoSize = PhotoSize.Small,
                    Name = "my_images.jpg",
                    SaveToAlbum = false

                });

                tom[i] = mf1[i].Path;
                l++;

                bool answer = await DisplayAlert("Do you want to add more pictures", "Picture " + l.ToString() + " of 4 added", "Yes", "No");
                if (answer == true)
                {
                    continue;
                }
                if (answer == false)
                {
                    break;
                }

            }

           

            if (mf1[0] == null)
                return;

            //await DisplayAlert("File Location", file.Path, "OK");
            //LocalPathLabel.Text = mf.Path;


            emage1.Source = ImageSource.FromStream(() =>
            {
                return mf1[0].GetStream();
            });


            emage2.Source = ImageSource.FromStream(() =>
            {
                return mf1[1].GetStream();
            });
            emage3.Source = ImageSource.FromStream(() =>
            {
                return mf1[2].GetStream();
            });
            emage4.Source = ImageSource.FromStream(() =>
            {
                return mf1[3].GetStream();
            });



        }

        private async void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            abs = false;

            if(LocalPathLabel == null)
            {
                bc.IsRunning = false;
                bc.IsVisible = false;
                bc.IsEnabled = false;
                await DisplayAlert("Error", "Please upload picture", "Return");

            }
            else
            {
                if(product_name.Text == null)
                {
                    bc.IsRunning = false;
                    bc.IsVisible = false;
                    bc.IsEnabled = false;
                    await DisplayAlert("Error", "Please add your product name", "Return");

                }
                else
                {
                    if(product_description.Text == null)
                    {
                        bc.IsRunning = false;
                        bc.IsVisible = false;
                        bc.IsEnabled = false;
                        await DisplayAlert("Error", "Please add product descriptions to make your product popp!!", "Return");

                    }
                    else
                    {
                        if(product_price.Text == null)
                        {
                            bc.IsRunning = false;
                            bc.IsVisible = false;
                            bc.IsEnabled = false;
                            await DisplayAlert("Error", "Please add product price!!", "Return");

                        }
                        else
                        {
                            byte[] imageBt = null;
                            byte[] imageBt1 = null;
                            byte[] imageBt2 = null;
                            byte[] imageBt3 = null;




                            FileStream fstream = new FileStream(tom[0], FileMode.Open, FileAccess.Read);
                            BinaryReader bro = new BinaryReader(fstream);
                            imageBt = bro.ReadBytes((int)fstream.Length);

                            if(l>1)
                            {
                                FileStream fstream1 = new FileStream(tom[1], FileMode.Open, FileAccess.Read);
                                BinaryReader bro1 = new BinaryReader(fstream1);
                                imageBt1 = bro1.ReadBytes((int)fstream1.Length);

                                if(l>2)
                                {
                                    FileStream fstream2 = new FileStream(tom[2], FileMode.Open, FileAccess.Read);
                                    BinaryReader bro2 = new BinaryReader(fstream2);
                                    imageBt2 = bro2.ReadBytes((int)fstream2.Length);

                                    if(l>3)
                                    {
                                        FileStream fstream3 = new FileStream(tom[3], FileMode.Open, FileAccess.Read);
                                        BinaryReader bro3 = new BinaryReader(fstream3);
                                        imageBt3 = bro3.ReadBytes((int)fstream3.Length);
                                    }

                                }
                            }

                            if(l==1)
                            {
                                using (var con = new SqlConnection(App.connstr))
                                {
                                    using (var cmd = con.CreateCommand())
                                    {
                                        cmd.CommandText = @"INSERT INTO Buyersguide " +
                                        "(NM, DESCRIPTIONS, PICTURE,PRICE,NUMBER_PIC,OWN) " +
                                        "VALUES(@NAME, @D, @PIC,@PRI,@NP,@O)";

                                        cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters.Add("@D", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters.Add("@PIC", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@PRI", System.Data.SqlDbType.Int);
                                        cmd.Parameters.Add("@NP", System.Data.SqlDbType.Int);
                                        cmd.Parameters.Add("@O", System.Data.SqlDbType.VarChar);





                                        cmd.Parameters["@NAME"].Value = product_name.Text;
                                        cmd.Parameters["@D"].Value = product_description.Text;
                                        cmd.Parameters["@PIC"].Value = imageBt;
                                        cmd.Parameters["@PRI"].Value = product_price.Text;
                                        cmd.Parameters["@NP"].Value = l;
                                        cmd.Parameters["@O"].Value = App.GlobalUsername;






                                        con.Open();
                                        //int insertedproductid = (int)cmd.ExecuteScalar();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            if(l==2)
                            {
                                using (var con = new SqlConnection(App.connstr))
                                {
                                    using (var cmd = con.CreateCommand())
                                    {
                                        cmd.CommandText = @"INSERT INTO Buyersguide " +
                                        "(NM, DESCRIPTIONS, PICTURE,PRICE,PIIC1,NUMBER_PIC,OWN) " +
                                        "VALUES(@NAME, @D, @PIC,@PRI,@PIC1,@NP,@O)";

                                        cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters.Add("@D", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters.Add("@PIC", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@PRI", System.Data.SqlDbType.Int);
                                        cmd.Parameters.Add("@PIC1", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@NP", System.Data.SqlDbType.Int);
                                        cmd.Parameters.Add("@O", System.Data.SqlDbType.VarChar);





                                        cmd.Parameters["@NAME"].Value = product_name.Text;
                                        cmd.Parameters["@D"].Value = product_description.Text;
                                        cmd.Parameters["@PIC"].Value = imageBt;
                                        cmd.Parameters["@PRI"].Value = product_price.Text;
                                        cmd.Parameters["@PIC1"].Value = imageBt1;
                                        cmd.Parameters["@NP"].Value = l;
                                        cmd.Parameters["@O"].Value = App.GlobalUsername;






                                        con.Open();
                                        //int insertedproductid = (int)cmd.ExecuteScalar();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (l == 3)
                            {
                                using (var con = new SqlConnection(App.connstr))
                                {
                                    using (var cmd = con.CreateCommand())
                                    {
                                        cmd.CommandText = @"INSERT INTO Buyersguide " +
                                        "(NM, DESCRIPTIONS, PICTURE,PRICE,PIIC1,PIIC2,NUMBER_PIC,OWN) " +
                                        "VALUES(@NAME, @D, @PIC,@PRI,@PIC1,@PIC2,@NP,@O)";

                                        cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters.Add("@D", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters.Add("@PIC", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@PRI", System.Data.SqlDbType.Int);
                                        cmd.Parameters.Add("@PIC1", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@PIC2", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@NP", System.Data.SqlDbType.Int);
                                        cmd.Parameters.Add("@O", System.Data.SqlDbType.VarChar);





                                        cmd.Parameters["@NAME"].Value = product_name.Text;
                                        cmd.Parameters["@D"].Value = product_description.Text;
                                        cmd.Parameters["@PIC"].Value = imageBt;
                                        cmd.Parameters["@PRI"].Value = product_price.Text;
                                        cmd.Parameters["@PIC1"].Value = imageBt1;
                                        cmd.Parameters["@PIC2"].Value = imageBt2;
                                        cmd.Parameters["@NP"].Value =l;
                                        cmd.Parameters["@O"].Value = App.GlobalUsername;






                                        con.Open();
                                        //int insertedproductid = (int)cmd.ExecuteScalar();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            if (l == 4)
                            {
                                using (var con = new SqlConnection(App.connstr))
                                {
                                    using (var cmd = con.CreateCommand())
                                    {
                                        cmd.CommandText = @"INSERT INTO Buyersguide " +
                                        "(NM, DESCRIPTIONS, PICTURE,PRICE,PIIC1,PIIC2,PIIC3,NUMBER_PIC,OWN) " +
                                        "VALUES(@NAME, @D, @PIC,@PRI,@PIC1,@PIC2,@PIC3,@NP,@O)";

                                        cmd.Parameters.Add("@NAME", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters.Add("@D", System.Data.SqlDbType.VarChar);
                                        cmd.Parameters.Add("@PIC", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@PRI", System.Data.SqlDbType.Int);
                                        cmd.Parameters.Add("@PIC1", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@PIC2", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@PIC3", System.Data.SqlDbType.VarBinary);
                                        cmd.Parameters.Add("@NP", System.Data.SqlDbType.Int);
                                        cmd.Parameters.Add("@O", System.Data.SqlDbType.VarChar);





                                        cmd.Parameters["@NAME"].Value = product_name.Text;
                                        cmd.Parameters["@D"].Value = product_description.Text;
                                        cmd.Parameters["@PIC"].Value = imageBt;
                                        cmd.Parameters["@PRI"].Value = product_price.Text;
                                        cmd.Parameters["@PIC1"].Value = imageBt1;
                                        cmd.Parameters["@PIC2"].Value = imageBt2;
                                        cmd.Parameters["@PIC3"].Value = imageBt3;
                                        cmd.Parameters["@NP"].Value = l;
                                        cmd.Parameters["@O"].Value = App.GlobalUsername;






                                        con.Open();
                                        //int insertedproductid = (int)cmd.ExecuteScalar();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }






















                            abs = true;
                            bc.IsRunning = false;
                            bc.IsVisible = false;
                            bc.IsEnabled = false;
                            await DisplayAlert("Perfect!", "Your product is uploaded.", "OK");
                            
                            await Navigation.PushAsync(new picturelist());


                        }
                    }
                }
            }
             
        }

        private async void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            if(abs == true)
            {
                await Navigation.PushAsync(new picturelist());

            }
            else
            {
                ac.IsRunning = false;
                ac.IsVisible = false;
                ac.IsEnabled = false;
                await Navigation.PopAsync();

            }

        }

        void Handle_Pressed(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            ac.IsRunning = true;
            ac.IsVisible = true;
            ac.IsEnabled = true;
        }

        void Handle_Released(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            ac.IsRunning = true;
            ac.IsVisible = true;
            ac.IsEnabled = true;
        }

        void Handle_Released_1(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            bc.IsRunning = true;
            bc.IsVisible = true;
            bc.IsEnabled = true;
        }
    }
}
