namespace sga
{
    public static class Extentions
    {
        public static object GetValObj(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        public static string GetValue(this Dictionary<string, object> dict, string key)
        {
            string value = "";
            foreach (KeyValuePair<string, object> pair in dict)
            {
                if (EqualityComparer<string>.Default.Equals(pair.Key, key))
                {
                    value = pair.Value.ToString();
                    break;
                }
            }
            return value;
        }

    }
}
