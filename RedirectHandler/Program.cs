using System;
using System.Diagnostics;


namespace RedirectHandler
{
    public class Program
    {

        
        static void Main(String[] args) {
            RedirectDepth rd = new RedirectDepth();
            rd.validate(3);
               
        }
    }
}
