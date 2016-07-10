using System;
using System.IO;
using System.Reflection;

namespace UITests.Helpers
{
    public static class FileHelpers
    {
        public static String GetAbsolutePath(String relativePath)
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var uri = new UriBuilder(basePath);
            string path = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            Uri result = new Uri(new Uri(path), relativePath);
            return Path.GetFullPath(result.LocalPath);
        }

        public static String GetFileContent(String path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                // Read the stream to a string, and write the string to the console.
                return sr.ReadToEnd();
            }
        }
    }
}

