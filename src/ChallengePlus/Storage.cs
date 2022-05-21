using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public struct StorageResult<T>
    {
        public T Data;
        public bool Success;
    }

    public static class Storage
    {
        public static IScriptStorage Instance;

        public static void Initialize()
        {
            Instance = Game.GetSharedStorage(Constants.SCRIPT_NAME);
        }
        public static string StorageKey(string key)
        {
            return Constants.STORAGE_KEY_PREFIX + key;
        }

        public static void SetItem(string key, int value)
        {
            Instance.SetItem(StorageKey(key), value);
        }
        public static void SetItem(string key, string[] value)
        {
            Instance.SetItem(StorageKey(key), value);
        }
        public static void SetItem(string key, string value)
        {
            Instance.SetItem(StorageKey(key), value);
        }

        public static StorageResult<int> GetInt(string key)
        {
            int value;
            if (Instance.TryGetItemInt(StorageKey(key), out value))
            {
                return new StorageResult<int> { Data = value, Success = true };
            }
            return new StorageResult<int> { Data = default(int), Success = false };
        }

        public static StorageResult<string> GetString(string key)
        {
            string value;
            if (Instance.TryGetItemString(StorageKey(key), out value))
            {
                return new StorageResult<string> { Data = value, Success = true };
            }
            return new StorageResult<string> { Data = default(string), Success = false };
        }

        public static StorageResult<string[]> GetStringArr(string key)
        {
            string[] value;
            if (Instance.TryGetItemStringArr(StorageKey(key), out value))
            {
                return new StorageResult<string[]> { Data = value, Success = true };
            }
            return new StorageResult<string[]> { Data = new string[] { }, Success = false };
        }
    }
}
