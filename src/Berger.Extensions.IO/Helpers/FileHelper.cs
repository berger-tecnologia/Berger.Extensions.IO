using System.Reflection;

namespace Berger.Extensions.IO
{
    public static class FileHelper
    {
        public static string GetFile(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException("The specified file does not exists.");

            using var stream = new StreamReader(file);

            return stream.ReadToEnd();
        }
        public static string[] GetFiles(string path)
        {
            var files = new List<string>();

            var directories = Directory.GetDirectories(path);

            foreach (var directory in directories)
            {
                files.AddRange(Directory.GetFiles(directory));
            }

            return files.ToArray();
        }
        public static string GetEntryAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }
        public static string GetExecutingAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        public static string GetCallingAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        }
        public static string GetDirectoryPath(string directory, AssemblyType assemblyType)
        {
            var path = assemblyType switch
            {
                AssemblyType.Entry 
                    => GetEntryAssemblyPath(), 
                AssemblyType.Executing 
                    => GetExecutingAssemblyPath(), AssemblyType.Calling => GetCallingAssemblyPath(),
                _ 
                    => throw new ArgumentException("Invalid assembly type ", nameof(assemblyType))
            };

            return Path.Combine(path, directory);
        }
    }
}