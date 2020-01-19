using System;
using Xamarin.Forms;


namespace HC
{
    public class data
    {
        public string sender { get; set; }
        public string receiver { get; set; }
        public int amt { get; set; }
        public DateTime tim { get; set; }

        public override string ToString()
        {
            return (sender + " " + receiver + " " + amt + " " + tim).ToString();
        }

        public string getsender()
        {
            return sender;
        }
        public string getreceiver()
        {
            return receiver;
        }
        public int getamt()
        {
            return amt;
        }
        public DateTime gettim()
        {
            return tim;
        }




    }
}



