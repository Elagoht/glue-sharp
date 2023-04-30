namespace Glue
{
    public static class Merger
    {
        public static void Vertical(string delimiter, string separator, InpFile[] inpFiles)
        {
            string result = "";
            foreach (InpFile inpFile in inpFiles)
            {
                result += string.Join(separator, inpFile.Items(delimiter)) + '\n';
            }
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void VerticalAligned(string delimiter, string separator, Alignment alignment, char filler, InpFile[] inpFiles)
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
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void Horizontal(string delimiter, string separator, InpFile[] inpFiles)
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
                        result += inpFiles[file].Items(delimiter)[line] + separator;
                    }
                }
                result += '\n';
            }
            result = result.Substring(0, result.Length - separator.Length) + '\n';
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void HorizontalAligned(string delimiter, string separator, Alignment alignment, char filler, InpFile[] inpFiles)
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
            Console.Write(result);
            Environment.Exit(0);
        }
    }
}
