namespace SWECVI.ApplicationCore.Common
{
    public static class DictionaryExtension
    {
        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, TV defaultValue = default(TV))
        {
            TV value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TV SetValue<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, TV value = default(TV))
        {
            return dictionary.SetValue(key, value);
        }
    }
}
