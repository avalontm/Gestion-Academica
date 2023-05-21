namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FiltroFieldAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Display { get; private set; }

        public FiltroFieldAttribute(string name, string display = "")
        {
            Name = name;
            Display = display;
        }
    }
}
