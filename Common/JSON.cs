using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MultiplayerSFS.Common
{
    public static class JSON
    {
        public static void ToFile(object o, string path, out bool success)
        {
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(o, Formatting.None));
                success = true;
            }
            catch
            {
                success = false;
            }
        }

        public static bool FromFile<T>(string path, out T value)
        {
            try
            {
                value = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }
    }
}