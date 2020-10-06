using AbstractFactoryPattern.LensBox;
using AbstractFactoryPattern.CameraBox;
using AbstractFactoryPattern.DayFactory;
using System;

namespace AbstractFactoryPattern
{
    class Tester
    {
        IMakeCamera[] factories = new IMakeCamera[2];
        public Tester() {
            factories[0] = new EvDayFactory();
            factories[1] = new HoDayFactory();
        }

        private void TestCase(Camera camera, ITake lens) {
            Console.WriteLine("호환성 테스트");
            if (!camera.PutInlens(lens)) {
                Console.WriteLine("카메라에 렌트가 장착되지 않음");
            }
            if (!camera.TakeAPicture()) {
                Console.WriteLine("사진이 찍히지 않음");
            }
        }

        public void Test() {
            //TestDirect();
            TestUsingFactory();
        }

        private void TestDirect() {
            Camera camera = new EvCamera();
            ITake lens = new HoLens();
            TestCase(camera, lens);
        }

        private void TestUsingFactory() {
            Camera camera = null;
            ITake lens = null;
            foreach (IMakeCamera factory in factories) {
                camera = factory.MakeCamera();
                lens = factory.MakeLens();
                TestCase(camera, lens);
            }
        }
    }
}
