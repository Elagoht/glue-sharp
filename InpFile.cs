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
        public string[] GetItems(char delimiter)
        {
            return Content.Split(delimiter);
        }
        public int[] GetWidths(char delimiter)
        {
            List<string> items = GetItems(delimiter).ToList<string>();
            int[] widths = new int[items.Count()];
            for (int index = 0; index < items.Count(); index++)
            {
                widths[index] = items[index].Length;
            }
            return widths;
        }
        public string GetMerged(char delimiter, string separator)
        {
            string result = "";
            foreach (string item in GetItems(delimiter))
            {
                result += item + separator;
            }
            // Remove last separator
            return result.Substring(0, result.Length - separator.Length);
        }
    }
}
