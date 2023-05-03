namespace Glue
{
    public static class Merger
    {
        public static void Horizontal(string delimiter, string separator, bool outerBorder, InpFile[] inpFiles)
        {
            string result = "";
            foreach (InpFile inpFile in inpFiles) result +=
                (outerBorder ? separator : "") +
                string.Join(separator, inpFile.Items(delimiter)) +
                (outerBorder ? separator : "") +
                '\n';
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void HorizontalAligned(string delimiter, string separator, Alignment alignment, char filler, bool outerBorder, string headerDivider, InpFile[] inpFiles)
        {
            string result = "";
            int[] widths = GroupInfo.ColumnSizes(delimiter, inpFiles);
            foreach (InpFile inpFile in inpFiles)
            {
                string[] items = inpFile.Items(delimiter);
                result += (outerBorder ? separator : "");
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
                    else
                    {
                        result += Aligner.CenterAlign("", widths[index], filler) + separator;
                    }
                }
                // Check if it is first line 
                if (inpFile == inpFiles[0])
                {
                    // Check if header is available
                    if (headerDivider != "")
                    {
                        // Create divider line array
                        string[] dividerLine = new string[widths.Length];
                        for (int index = 0; index < widths.Length; index++)
                        {
                            dividerLine[index] = string.Concat(Enumerable.Repeat(
                                headerDivider != ""
                                    ? (headerDivider)
                                    : "-",
                                index == widths.Length - 1
                                    ? (outerBorder
                                        ? widths[index]
                                        : widths[index] + separator.Length)
                                    : widths[index]
                            )).Substring(0, widths[index]);
                        }
                        // Check the outer check because of adding two lines at once
                        if (!outerBorder)
                            result = result.Substring(0, result.Length - separator.Length);
                        // Then add the divider
                        result += '\n' + (outerBorder ? separator : ""); // Outer border
                        result += (string.Join(separator, dividerLine)); // Add the line
                        result += outerBorder ? separator : ""; // Outer border
                    }
                }
                if (!outerBorder)
                    result = result.Substring(0, result.Length - separator.Length);
                result += "\n";
            }
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void Vertical(string delimiter, string separator, bool outerBorder, InpFile[] inpFiles)
        {
            string result = "";
            int[] widths = GroupInfo.RowSizes(delimiter, inpFiles);
            int totalLines = GroupInfo.MaxLineCount(delimiter, inpFiles);
            for (int line = 0; line < totalLines; line++)
            {
                result += (outerBorder ? separator : "");
                for (int file = 0; file < inpFiles.Count(); file++)
                {
                    if (line < inpFiles[file].LineCount(delimiter))
                        result += inpFiles[file].Items(delimiter)[line] + separator;
                }
                result += '\n';
            }
            if (!outerBorder)
                result = result.Substring(0, result.Length - separator.Length) + '\n';
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void VerticalAligned(string delimiter, string separator, Alignment alignment, char filler, bool outerBorder, string headerDivider, InpFile[] inpFiles)
        {
            string result = "";
            int[] widths = GroupInfo.RowSizes(delimiter, inpFiles);
            int totalLines = GroupInfo.MaxLineCount(delimiter, inpFiles);
            for (int line = 0; line < totalLines; line++)
            {
                result += (outerBorder ? separator : "");
                for (int file = 0; file < inpFiles.Count(); file++)
                {
                    if (line < inpFiles[file].LineCount(delimiter))
                    {
                        switch (alignment)
                        {
                            case Alignment.Center:
                                result += Aligner.CenterAlign(inpFiles[file].Items(delimiter)[line], widths[file], filler);
                                break;
                            case Alignment.Right:
                                result += Aligner.RightAlign(inpFiles[file].Items(delimiter)[line], widths[file], filler);
                                break;
                            default:
                                result += Aligner.LeftAlign(inpFiles[file].Items(delimiter)[line], widths[file], filler);
                                break;
                        }
                        if (headerDivider != "")
                            result += file == 0
                                ? headerDivider
                                : separator;
                        else
                            result += separator;
                    }
                    else
                    {
                        result += Aligner.CenterAlign("", widths[file], filler) + separator;
                    }
                }
                result += (outerBorder ? separator : "");
                result = result.Substring(0, result.Length - separator.Length) + '\n';
            }
            Console.Write(result);
            Environment.Exit(0);
        }
    }
}
