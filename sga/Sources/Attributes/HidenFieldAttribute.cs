namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HidenFieldAttribute : Attribute
    {
        public bool isHidden { get; private set; }

        public HidenFieldAttribute()
        {
            isHidden = true;
        }
    }
}
