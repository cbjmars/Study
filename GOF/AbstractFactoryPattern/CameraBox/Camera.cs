using AbstractFactoryPattern.LensBox;

namespace AbstractFactoryPattern.CameraBox
{
    abstract class Camera
    {
        protected ITake Lens { get; set; }

        public Camera()
        {
            Lens = null;
        }
        
        //렌즈 장착
        public abstract bool PutInlens(ITake itake);

        //사진 찍기
        public virtual bool TakeAPicture() {
            if (Lens == null) return false;
            Lens.Take();
            return true;
        }
    }
}
