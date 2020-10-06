using AbstractFactoryPattern.LensBox;

namespace AbstractFactoryPattern.CameraBox
{
    class EvCamera : Camera
    {
        public override bool PutInlens(ITake itake)
        {
            //as 연산자가 itake의 타입이 EvLens 인지 확인 후 다를경우 null 리턴
            EvLens evLens = itake as EvLens;
            if (evLens == null) return false;
            Lens = itake;
            return true;
        }

        public override bool TakeAPicture()
        {
            EvLens evLens = Lens as EvLens;
            if (evLens is null) return false;
            evLens.AutoFocus();
            /*
             if (Lens == null) return false;
             Lens.Take();
             return true
            */
            return base.TakeAPicture();
        }
    }
}
