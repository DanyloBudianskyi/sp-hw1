namespace task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //•	Напишите приложение, которое будет:
            //o Проверять наличие подкаталогов для контрактов и отчетов.
            //o Перемещать файлы с расширением.pdf в папку "Contracts" и файлы с расширением .docx в папку "Reports".
            Console.WriteLine("Input file name with extention(file.exe, file.pdf)");
            string fileName = Console.ReadLine();
            string path = GetProjectRootPath();
            if (fileName.EndsWith(".pdf")){
                if (!Directory.Exists(path += "Contracts\\")){
                    Directory.CreateDirectory(path);
                }
                File.Create(path += fileName).Dispose();
            }
            else if (fileName.EndsWith(".docx"))
            {
                if (!Directory.Exists(path += "Reports\\"))
                {
                    Directory.CreateDirectory(path);
                }
                File.Create(path += fileName).Dispose();
            }
            else
            {
                if (!Directory.Exists(path += "Other\\"))
                {
                    Directory.CreateDirectory(path);
                }
                File.Create(path += fileName).Dispose();
            }
        }

        static string GetProjectRootPath()
        {
            var directory = new DirectoryInfo(Environment.CurrentDirectory);
            while (directory != null && directory.Name != "task1")
            {
                directory = directory.Parent;
            }
            return directory?.FullName + "\\" ?? Environment.CurrentDirectory;
        }
    }
}
