namespace Glue
{
    public static class GroupInfo
    {
        // Get widths in all list
        public static List<int[]> Widths(string delimiter, InpFile[] inpFiles)
        {
            List<int[]> widths = new List<int[]>();
            // Get data for all
            foreach (InpFile inpFile in inpFiles)
                widths.Add(inpFile.Widths(delimiter));

            return widths;
        }

        // Get max file length
        public static int MaxLineCount(string delimiter, InpFile[] inpFiles)
        {
            List<int> lineNumbers = new List<int>();
            // Get data fora all
            foreach (InpFile inpFile in inpFiles)
                lineNumbers.Add(inpFile.LineCount(delimiter));
            // return highest value
            return lineNumbers.Max<int>();
        }
        // Get row sizes of all
        public static int[] RowSizes(string delimiter, InpFile[] inpFiles)
        {
            List<int[]> widths = Widths(delimiter, inpFiles);
            int totalLines = MaxLineCount(delimiter, inpFiles);

            // Get row widths
            List<int> result = new List<int>();
            for (int fileNum = 0; fileNum < inpFiles.Count(); fileNum++)
            {
                List<int> currentMax = new List<int>();
                // Get current row's element sizes
                for (int line = 0; line < totalLines; line++)
                    if (line < inpFiles[fileNum].LineCount(delimiter))
                        currentMax.Add(widths[fileNum][line]);
                // Then add it's longest one to final result 
                result.Add(currentMax.Max());
            }

            // Return result
            return result.ToArray();
        }
        // Get column sizes of all
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
                    if (line < inpFiles[fileNum].LineCount(delimiter))
                        currentMax.Add(widths[fileNum][line]);
                result.Add(currentMax.Max());
            }

            // Return result
            return result.ToArray<int>();
        }
    }
}