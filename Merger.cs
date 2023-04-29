namespace Glue
{
    public static class Merger
    {
        public static string Vertical(string delimiter, string separator, InpFile[] inpFiles)
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
        public static string VerticalAligned(string delimiter, string separator, Alignment alignment, char filler, InpFile[] inpFiles)
        {
            string result = "";
            int[] widths = GroupInfo.ColumnSizes(delimiter, inpFiles);
            foreach (InpFile inpFile in inpFiles)
            {
                string[] items = inpFile.Items(delimiter);
                for (int index = 0; index < widths.Length; index++)
                {
                    if (index < items.Length)
                    {
                        switch (alignment)
                        {
                            case Alignment.Center:
                                result += Aligner.CenterAlign(items[index], widths[index], filler) + separator;
                                break;
                            case Alignment.Right:
                                result += Aligner.RightAlign(items[index], widths[index], filler) + separator;
                                break;
                            default:
                                result += Aligner.LeftAlign(items[index], widths[index], filler) + separator;
                                break;
                        }
                    }
                }
                result = result.Substring(0, result.Length - separator.Length);
                result += "\n";
            }
            return result.Substring(0, result.Length - 1);
        }
        public static string Horizontal(string delimiter, string separator, InpFile[] inpFiles)
        {
            string result = "";
            foreach (InpFile inpFile in inpFiles)
            {
                result += string.Join(separator, inpFile.Items(delimiter)) + '\n';
            }
            return result.Substring(0, result.Length - 1);
        }
        public static string HorizontalAligned(string delimiter, string separator, Alignment alignment, char filler, InpFile[] inpFiles)
        {
            string result = "";
            int[] widths = GroupInfo.RowSizes(delimiter, inpFiles);
            int totalLines = GroupInfo.MaxLineCount(delimiter, inpFiles);
            for (int line = 0; line < totalLines; line++)
            {
                for (int file = 0; file < inpFiles.Count(); file++)
                {
                    if (line < inpFiles[file].LineCount(delimiter))
                    {
                        switch (alignment)
                        {
                            case Alignment.Center:
                                result += Aligner.CenterAlign(inpFiles[file].Items(delimiter)[line], widths[file], filler) + separator;
                                break;
                            case Alignment.Right:
                                result += Aligner.RightAlign(inpFiles[file].Items(delimiter)[line], widths[file], filler) + separator;
                                break;
                            default:
                                result += Aligner.LeftAlign(inpFiles[file].Items(delimiter)[line], widths[file], filler) + separator;
                                break;
                        }
                    }
                }
                result = result.Substring(0, result.Length - separator.Length) + '\n';
            }
            return result.Substring(0, result.Length - 1);
        }
    }
}
