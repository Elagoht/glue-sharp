namespace Glue
{
    public static class GroupInfo
    {
        public static List<int[]> Widths(string delimiter, InpFile[] inpFiles)
        {
            // Get widths in all list
            List<int[]> widths = new List<int[]>();
            foreach (InpFile inpFile in inpFiles) widths.Add(inpFile.Widths(delimiter));

            return widths;
        }

        public static int MaxLineCount(string delimiter, InpFile[] inpFiles)
        {
            // Get max file length
            List<int> lineNumbers = new List<int>();
            foreach (InpFile inpFile in inpFiles) lineNumbers.Add(inpFile.LineCount(delimiter));

            return lineNumbers.Max<int>();
        }
        public static int[] RowSizes(string delimiter, InpFile[] inpFiles)
        {
            List<int[]> widths = Widths(delimiter, inpFiles);
            int totalLines = MaxLineCount(delimiter, inpFiles);

            // Get row widths
            List<int> result = new List<int>();
            for (int fileNum = 0; fileNum < inpFiles.Count(); fileNum++)
            {
                List<int> currentMax = new List<int>();
                for (int line = 0; line < totalLines; line++)
                {
                    if (line < inpFiles[fileNum].LineCount(delimiter))
                        currentMax.Add(widths[fileNum][line]);
                }
                result.Add(currentMax.Max());
            }

            // Return result
            return result.ToArray();
        }
        public static int[] ColumnSizes(string delimiter, InpFile[] inpFiles)
        {
            List<int[]> widths = Widths(delimiter, inpFiles);
            int totalLines = MaxLineCount(delimiter, inpFiles);

            // Get column widths
            List<int> result = new List<int>();
            for (int line = 0; line < totalLines; line++)
            {
                List<int> currentMax = new List<int>();
                for (int fileNum = 0; fileNum < inpFiles.Count(); fileNum++)
                {
                    if (line < inpFiles[fileNum].LineCount(delimiter))
                        currentMax.Add(widths[fileNum][line]);
                }
                result.Add(currentMax.Max());
            }

            // Return result
            return result.ToArray<int>();
        }
    }
}