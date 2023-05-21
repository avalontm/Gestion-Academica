namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailFieldAttribute : Attribute
    {
        public bool isEmail { get; private set; }

        public EmailFieldAttribute()
        {
            isEmail = true;
        }
    }
}
