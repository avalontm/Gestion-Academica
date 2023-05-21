namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OnlyReadAttribute : Attribute
    {
        public bool osOnlyRead { get; private set; }

        public OnlyReadAttribute()
        {
            osOnlyRead = true;
        }
    }
}
