namespace Glue
{
    public class InpFile
    {
        private string Content { set; get; } = "";
        public InpFile(string path)
        {
            try
            {
                using (StreamReader file = new StreamReader(path))
                {
                    this.Content = file.ReadToEnd();
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.Error.WriteLine("The file '{0}' is not accessible.", path);
                Environment.Exit(126);
            }
        }
        public string[] Items(char delimiter)
        {
            return this.Content.Split(delimiter);
        }
        public int[] Widths(char delimiter)
        {
            List<string> items = Items(delimiter).ToList<string>();
            int[] widths = new int[items.Count()];
            for (int index = 0; index < items.Count(); index++)
            {
                widths[index] = items[index].Length;
            }
            return widths;
        }
        public int LineCount(char delimiter)
        {
            return this.Content.Split(delimiter).Count();
        }
        public string Merged(char delimiter, string separator)
        {
            string result = "";
            foreach (string item in Items(delimiter))
            {
                result += item + separator;
            }
            // Remove last separator
            return result.Substring(0, result.Length - separator.Length);
        }
    }
}
