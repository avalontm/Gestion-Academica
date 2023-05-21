namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParentFieldAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Display { get; private set; }
        public ParentFieldAttribute(string name, string display = "")
        {
            Name = name;
            Display = display;
        }
    }
}
