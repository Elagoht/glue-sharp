namespace Glue
{
    public static class Merger
    {
        public enum Direction
        {
            Left,
            Center,
            Right
        }
        public static string Vertical(char delimiter, string separator, InpFile[] inpFiles)
        {
            string result = "";
            foreach (InpFile inpFile in inpFiles)
            {
                // Add items seperated
                string line = inpFile.Merged(delimiter, separator);
                result += line + "\n";
            }
            // Remove last line
            return result.Substring(0, result.Length - 1);
        }
        // Align as vertical lines
        public static string VerticalAligned(char delimiter, string separator, Direction direction, char filler, InpFile[] inpFiles)
        {
            string result = "";
            int[] widths = ColumnSizes(delimiter, inpFiles);
            foreach (InpFile inpFile in inpFiles)
            {
                string[] items = inpFile.Items(delimiter);
                for (int index = 0; index < widths.Length; index++)
                {
                    if (index < items.Length)
                    {
                        switch (direction)
                        {
                            case Direction.Center:
                                result += Aligner.CenterAlign(items[index], widths[index], filler) + separator;
                                break;
                            case Direction.Right:
                                result += Aligner.RightAlign(items[index], widths[index], filler) + separator;
                                break;
                            default:
                                result += Aligner.LeftAlign(items[index], widths[index], filler) + separator;
                                break;
                        }
                    }
                }
                result = result.Substring(0, result.Length - separator.Length);
                result += '\n';
            }
            return result.Substring(0, result.Length - 1);
        }
        private static int[] ColumnSizes(char delimiter, InpFile[] inpFiles)
        {
            // Get widths in all list
            List<int[]> widths = new List<int[]>();
            foreach (InpFile inpFile in inpFiles)
            {
                widths.Add(inpFile.Widths(delimiter));
            }

            // Get max file length
            List<int> lineNumbers = new List<int>();
            foreach (InpFile inpFile in inpFiles)
            {
                lineNumbers.Add(inpFile.LineCount(delimiter));
            }
            int totalLines = lineNumbers.Max<int>();

            // Get Column Widths
            List<int> result = new List<int>();
            for (int line = 0; line < totalLines; line++)
            {
                List<int> currentMax = new List<int>();
                for (int fileNum = 0; fileNum < inpFiles.Count(); fileNum++)
                {
                    if (line < inpFiles[fileNum].LineCount(delimiter))
                    {
                        currentMax.Add(widths[fileNum][line]);
                    }
                }
                result.Add(currentMax.Max());
            }
            // Return result
            return result.ToArray<int>();
        }
    }
}
