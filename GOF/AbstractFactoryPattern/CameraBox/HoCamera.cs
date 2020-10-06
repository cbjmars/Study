using AbstractFactoryPattern.LensBox;

namespace AbstractFactoryPattern.CameraBox
{
    class HoCamera : Camera
    {
        public override bool PutInlens(ITake itake)
        {
            //as 연산자가 itake의 타입이 HoLens 인지 확인 후 다를경우 null 리턴
            HoLens hoLens = itake as HoLens;
            if (hoLens == null) return false;
            Lens = hoLens;
            return true;
        }

        public override bool TakeAPicture()
        {
            HoLens hoLens = Lens as HoLens;
            if (hoLens == null) return false;
            hoLens.ManualFocus();
            /*
             if (Lens == null) return false;
             Lens.Take();
             return true
            */
            return base.TakeAPicture();
        }
    }
}
