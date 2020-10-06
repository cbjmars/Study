using AbstractFactoryPattern.CameraBox;
using AbstractFactoryPattern.LensBox;

namespace AbstractFactoryPattern.DayFactory
{
    class HoDayFactory : IMakeCamera
    {
        public ITake MakeLens()
        {
            return new HoLens();
        }

        public Camera MakeCamera()
        {
            return new HoCamera();
        }       
    }
}
