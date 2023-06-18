namespace Glue
{
    public class InpFile
    {
        private string Content { set; get; } = "";
        public InpFile(string path)
        {
            try
            { // Try to read file and assing its content
                using (StreamReader file = new StreamReader(path)) this.Content = file.ReadToEnd();
            }
            catch (System.IO.FileNotFoundException)
            { // If there is no file warn the user
                Console.Error.WriteLine("\x1b[31;1mThe file '{0}' is not accessible.\x1b[0;m", path);
                Environment.Exit(126);
            }
            catch (System.UnauthorizedAccessException)
            { // If there is no read permission warn the user
                Console.Error.WriteLine("\x1b[31;1mYou have no permission to read the file '{0}'.\x1b[0;m", path);
                Environment.Exit(126);
            }
            catch
            { // If the error is unknown tell them the truth
                Console.Error.WriteLine("\x1b[31;1mAn unexpected error occurred.\x1b[0;m");
                Environment.Exit(1);
            }
        }
        public void DeleteLastBlankSlice(string delimiter)
        { // If the last part is delimiter's itself delete it
            if (this.Content.Substring(this.Content.Length - delimiter.Length, delimiter.Length) == delimiter)
                this.Content = this.Content.Substring(0, this.Content.Length - delimiter.Length);
        }
        public string[] Items(string delimiter)
        { // Split with delimiter
            return this.Content.Split(delimiter);
        }
        public int[] Widths(string delimiter)
        { // Get all items width in an array
            string[] items = Items(delimiter);
            int[] widths = new int[items.Count()];
            // Get all width data one by one
            for (int index = 0; index < items.Count(); index++)
                widths[index] = items[index].Length;
            return widths;
        }
        public int LineCount(string delimiter)
        { // Count how many items
            return Items(delimiter).Count();
        }
    }
}
