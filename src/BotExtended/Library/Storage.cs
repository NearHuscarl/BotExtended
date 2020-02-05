using SFDGameScriptInterface;
using static SFDScript.Library.Mocks.MockObjects;

namespace SFDScript.Library
{
    public static class Storage
    {
        private static IScriptStorage m_storage = Game.LocalStorage;

        internal static bool Get(string key, out string value)
        {
            return m_storage.TryGetItemString(key, out value);
        }
        internal static bool Get(string key, out int value)
        {
            return m_storage.TryGetItemInt(key, out value);
        }
        internal static bool Get(string key, out bool value)
        {
            return m_storage.TryGetItemBool(key, out value);
        }
        internal static bool Get(string key, out float value)
        {
            return m_storage.TryGetItemFloat(key, out value);
        }
        internal static bool Get(string key, out long value)
        {
            return m_storage.TryGetItemLong(key, out value);
        }
        internal static bool Get(string key, out byte value)
        {
            return m_storage.TryGetItemByte(key, out value);
        }
        internal static bool Get(string key, out string[] value)
        {
            return m_storage.TryGetItemStringArr(key, out value);
        }
        internal static bool Get(string key, out int[] value)
        {
            return m_storage.TryGetItemIntArr(key, out value);
        }
        internal static bool Get(string key, out bool[] value)
        {
            return m_storage.TryGetItemBoolArr(key, out value);
        }
        internal static bool Get(string key, out float[] value)
        {
            return m_storage.TryGetItemFloatArr(key, out value);
        }
        internal static bool Get(string key, out long[] value)
        {
            return m_storage.TryGetItemLongArr(key, out value);
        }
        internal static bool Get(string key, out byte[] value)
        {
            return m_storage.TryGetItemByteArr(key, out value);
        }


        internal static void Set(string key, string value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, int value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, bool value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, float value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, long value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, byte value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, string[] value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, int[] value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, bool[] value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, float[] value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, long[] value)
        {
            m_storage.SetItem(key, value);
        }
        internal static void Set(string key, byte[] value)
        {
            m_storage.SetItem(key, value);
        }

        internal static void Remove(string key)
        {
            m_storage.RemoveItem(key);
        }
    }
}
