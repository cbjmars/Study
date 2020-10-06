using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryPattern.LensBox
{
    class HoLens : ITake
    {
        public void Take() {
            Console.WriteLine("자연스럽다");
        }

        public void ManualFocus() {
            Console.WriteLine("사용자가 수동으로 조점을 조정");
        }
    }
}
