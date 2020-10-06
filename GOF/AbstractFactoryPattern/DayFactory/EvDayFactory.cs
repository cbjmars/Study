using AbstractFactoryPattern.CameraBox;
using AbstractFactoryPattern.LensBox;

namespace AbstractFactoryPattern.DayFactory
{
    class EvDayFactory : IMakeCamera
    {
        public ITake MakeLens()
        {
            return new EvLens();
        }
     
        public Camera MakeCamera()
        {
            return new EvCamera();
        }        
    }
}
