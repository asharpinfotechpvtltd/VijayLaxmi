using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Monash.Models
{
    public static class Sms
    {
        static readonly string UserName = "t5vijaylaxmimansol";
        static readonly string Key = "01095qu204Xjix7C60jh";
        static readonly string Sender = "VJLXML";
        static readonly Int64 entityid = 1201159194587537978;
        static readonly Int64 templateid = 1207166124049982362;
        
        public static void SendSMS(string number,string amount ,string msg)
        {
            string sURL;
            StreamReader objReader;
            //give the USERNAME,PASSWORD,moblienumbers.... on URL 

            string Text = "Dear Client Your 20 approvals pending for 20 days. Team [ VIJAY LAXMI MANSOL ]";
            //string Text = "We would like to thank you for giving the time to Bright Insurance. We look forward to a fruitful collaboration with you in the future. For further query contact +91 9990916001 or visit www.brightinsurance.in Team [ Bright Insurance ]";

            sURL = "http://sms.medianest.net.in/api/pushsms.php?user="+UserName+"&key="+Key+"&sender="+Sender+"&mobile="+number+"&text="+Text+"&entityid="+entityid+"&templateid="+templateid;
            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);
            try
            {
                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();
                objReader = new StreamReader(objStream);
                objReader.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        

    }
}
