namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColorFieldAttribute : Attribute
    {
        public bool isValue { get; private set; }

        public ColorFieldAttribute()
        {
            isValue = true;
        }
    }
}
