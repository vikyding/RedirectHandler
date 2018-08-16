using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace RedirectHandler
{
    class RedirectDepth
    {

        private const int max = 5;

        public void validate(int number = 0) {
            if (number < max) {
                Debug.WriteLine(number);
                number++;
                validate(number);
            } 
        }
    }
}
