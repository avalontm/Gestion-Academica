namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordFieldAttribute : Attribute
    {
        public bool isPassword { get; private set; }

        public PasswordFieldAttribute()
        {
            isPassword = true;
        }
    }
}
