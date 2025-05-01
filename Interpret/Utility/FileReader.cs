namespace Interpret.Utility
{
    internal static class FileReader
    {
        /// <param name="where">Path to the file</param>
        /// <returns>Contents of the file (with \r\n → \n replaced) or null if it does not exist</returns>
        public static string ReadFile(string where)
        {
            if (!File.Exists(where)) {
                return null;
            }

            var contents = File.ReadAllText(where);
            return contents.Replace("\r\n", "\n") + "\n";
        }

        /// <param name="where">Path to the directory</param>
        /// <returns>Collection of paths to .yappt files in given directory or null if the directory does not exist</returns>
        public static string[] GetDirectoryTestFileCollection(string where)
        {
            if (!Directory.Exists(where)) {
                return null;
            }

            var files = Directory.GetFiles(where, "*.yappt");
            return files;
        }
    }
}
