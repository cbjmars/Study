using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryPattern.LensBox
{
    class EvLens : ITake
    {
        public void Take() {
            Console.WriteLine("부드럽다"); 
        }

        public void AutoFocus() {
            Console.WriteLine("자동 초점 조절");
        }
    }
}
