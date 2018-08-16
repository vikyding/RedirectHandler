using System;
using System.Diagnostics;


namespace RedirectHandler
{
    public class Program
    {

        
        static void Main(String[] args) {
            DelegateTest dt = new DelegateTest();
            Pt pt = dt.PrintMessage;
            Pt pt1 = dt.PrintMsg1;
            Pt pt2 = dt.PrintMsg2;

            Pt pt_s = DelegateTest.PrintMsg3;
            Debug.WriteLine(pt_s("test delegate"));
               
        }
    }
}
