#if !UNITY_WEBPLAYER

#if UNITY_WINRT && !UNITY_EDITOR
using File = UnityEngine.Windows.File;
#else
using File = System.IO.File;
#endif

public static class FileTools {

    public static string Read(string path) {

        string result = "";

        #if UNITY_WINRT && !UNITY_EDITOR
            byte[] bytes = File.ReadAllBytes(path);
            result = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        #else
            result = File.ReadAllText(path);
        #endif

        return result;
    }

    
    public static void Write(string path, string contents) {

        #if UNITY_WINRT && !UNITY_EDITOR
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(contents);
            File.WriteAllBytes(path, bytes);
        #else
            File.WriteAllText(path, contents);
        #endif
    }

    public static bool Exists(string path) {
        return File.Exists(path);
    }
}
#endif