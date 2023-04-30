namespace Glue
{
    public class InpFile
    {
        private string Content { set; get; } = "";
        public InpFile(string path)
        {
            try
            {
                using (StreamReader file = new StreamReader(path)) this.Content = file.ReadToEnd();
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.Error.WriteLine("\x1b[31;1mThe file '{0}' is not accessible.\x1b[0;m", path);
                Environment.Exit(126);
            }
            catch (System.UnauthorizedAccessException)
            {
                Console.Error.WriteLine("\x1b[31;1mYou have no permission to read the file '{0}'.\x1b[0;m", path);
                Environment.Exit(126);
            }
            catch
            {
                Console.Error.WriteLine("\x1b[31;1mAn unexpected error occurred.");
                Environment.Exit(126);
            }
        }
        public string[] Items(string delimiter)
        {
            return this.Content.Split(delimiter);
        }
        public int[] Widths(string delimiter)
        {
            List<string> items = Items(delimiter).ToList<string>();
            int[] widths = new int[items.Count()];
            for (int index = 0; index < items.Count(); index++) widths[index] = items[index].Length;
            return widths;
        }
        public int LineCount(string delimiter)
        {
            return Items(delimiter).Count();
        }
        public string Merged(string delimiter, string separator)
        {
            string result = "";
            foreach (string item in Items(delimiter)) result += item + separator;

            // Remove last separator
            return result.Substring(0, result.Length - separator.Length);
        }
    }
}
