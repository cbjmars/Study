using AbstractFactoryPattern.CameraBox;
using AbstractFactoryPattern.LensBox;

namespace AbstractFactoryPattern.DayFactory
{
    interface IMakeCamera
    {
        ITake MakeLens();
        Camera MakeCamera();
    }
}
