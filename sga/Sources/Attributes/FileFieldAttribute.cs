namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FileFieldAttribute : Attribute
    {
        public bool isValue { get; private set; }

        public FileFieldAttribute()
        {
            isValue = true;
        }
    }
}
