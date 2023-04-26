namespace Glue
{
    public static class Merger
    {
        public static string Vertical(char delimiter, string separator, InpFile[] inpFiles)
        {
            string result = "";
            foreach (InpFile inpFile in inpFiles)
            {
                // Add items seperated
                string line = inpFile.GetMerged(delimiter, separator);
                result += line + "\n";
            }
            // Remove last line
            return result.Substring(0, result.Length - 1);
        }
    }
}
