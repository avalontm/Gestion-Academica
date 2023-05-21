namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayFieldAttribute : Attribute
    {
        public string Display { get; private set; }
        public DisplayFieldAttribute(string display)
        {
            Display = display;
        }
    }
}
