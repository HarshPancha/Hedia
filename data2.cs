using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HC
{
    public class data2
    {
        public string name { get; set; }
        public string des { get; set; }
        public ImageSource img { get; set; }
        public int price { get; set; }



        public override string ToString()
        {
            return (name + "@" + des + "@" + img + "@" + price).ToString();

        }
        
       

        public string return_name()
        {
            return name;

        }



    }
}
