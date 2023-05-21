namespace sga.Sources.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EditorFieldAttribute : Attribute
    {
        public bool isEditor { get; private set; }
        public EditorFieldAttribute()
        {
            isEditor = true;
        }
    }
}
