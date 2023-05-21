namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneFieldAttribute : Attribute
    {
        public bool isValue { get; private set; }

        public PhoneFieldAttribute()
        {
            isValue = true;
        }
    }
}
