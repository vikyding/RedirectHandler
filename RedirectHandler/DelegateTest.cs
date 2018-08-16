using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace RedirectHandler
{

    public delegate String Pt(String msg);
    class DelegateTest
    {
        public String PrintMessage(String msg)
        {

            Debug.WriteLine(msg);
            msg = "Send to debug output.";
            return msg;
        }

        public String PrintMsg1(String msg)
        {
            Debug.WriteLine(msg);
            msg = "Send to debug output + 1.";
            return msg;
        }

        public String PrintMsg2(String msg)
        {
            
            Debug.WriteLine(msg);
            msg = "Send to debug output + 2.";
            return msg;
        }

        public static String PrintMsg3(String msg)
        {
            Debug.WriteLine(msg);
            msg = "Send to debug output + 2.";
            return msg;
        }

    }
}
