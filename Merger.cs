namespace Glue
{
    public static class Merger
    {
        public static void Horizontal(string delimiter, string separator, bool outerBorder, InpFile[] inpFiles)
        { // Serve horizontal, not aligned output
            string result = "";

            foreach (InpFile inpFile in inpFiles)
                result +=
                    (outerBorder ? separator : "") + // If border wanted, add it 
                    string.Join(separator, inpFile.Items(delimiter)) + // Join items with separator
                    (outerBorder ? separator : "") + // If border wanted, add it 
                    '\n'; // New line means new file here

            // Do the job and exit
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void HorizontalAligned(string delimiter, string separator, Alignment alignment, char filler, bool outerBorder, string headerDivider, InpFile[] inpFiles)
        {
            string result = "";

            // Get required data
            int[] widths = GroupInfo.ColumnSizes(delimiter, inpFiles);

            foreach (InpFile inpFile in inpFiles)
            {
                string[] items = inpFile.Items(delimiter);
                result += (outerBorder ? separator : ""); // If border wanted, add it
                for (int index = 0; index < widths.Length; index++)
                { // Loop all columns
                    if (index < items.Length) // Check if there is enough items in file
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
                    else
                    { // If items are not enough use placeholder created with filler
                        result += Aligner.CenterAlign("", widths[index], filler) + separator;
                    }
                }
                // Check if it is first line 
                if (inpFile == inpFiles[0])
                    // Check if header is available
                    if (headerDivider != "")
                    {
                        // Create divider line array
                        string[] dividerLine = new string[widths.Length];
                        for (int index = 0; index < widths.Length; index++)
                            // Create a repeated array from headerDivider
                            dividerLine[index] = string.Concat(Enumerable.Repeat(
                                // Repeat item
                                headerDivider,
                                // if it is last line
                                index == widths.Length - 1
                                    // and if border wanted
                                    ? (outerBorder
                                        // use default width
                                        ? widths[index]
                                        // else add an extra separator to keep one later
                                        : widths[index] + separator.Length)
                                    // else use default width
                                    : widths[index]
                            // Then get exact number of chars needed
                            )).Substring(0, widths[index]);
                        // Check the outer check because of adding two lines at once
                        if (!outerBorder)
                            result = result.Substring(0, result.Length - separator.Length);
                        // Then add the divider
                        result += '\n' + (outerBorder ? separator : ""); // If border wanted, add it
                        result += (string.Join(separator, dividerLine)); // Add lines joined with separator
                        result += outerBorder ? separator : ""; // If border wanted, add it
                    }
                if (!outerBorder) // if border not wanted delete the last separator on line
                    result = result.Substring(0, result.Length - separator.Length);
                result += "\n";
            }

            // Do the job and exit
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void Vertical(string delimiter, string separator, bool outerBorder, InpFile[] inpFiles)
        {
            string result = "";

            // Get required data
            int[] widths = GroupInfo.RowSizes(delimiter, inpFiles);
            int totalLines = GroupInfo.MaxLineCount(delimiter, inpFiles);

            for (int line = 0; line < totalLines; line++)
            { // Loop all lines
                result += (outerBorder ? separator : ""); // If border wanted, add it
                for (int file = 0; file < inpFiles.Count(); file++) // Loop "line'th" item of all files
                    if (line < inpFiles[file].LineCount(delimiter)) // If there are enough lines
                        result += inpFiles[file].Items(delimiter)[line] + // add items 
                        separator; // and separator
                result += '\n'; // Then add new line
            }
            if (!outerBorder) // If border not wanted, remove the last separator
                result = result.Substring(0, result.Length - separator.Length) + '\n';

            // Do the job and exit
            Console.Write(result);
            Environment.Exit(0);
        }
        public static void VerticalAligned(string delimiter, string separator, Alignment alignment, char filler, bool outerBorder, string headerDivider, InpFile[] inpFiles)
        {
            string result = "";

            // Get required data
            int[] widths = GroupInfo.RowSizes(delimiter, inpFiles);
            int totalLines = GroupInfo.MaxLineCount(delimiter, inpFiles);

            for (int line = 0; line < totalLines; line++)
            { // Loop al lines
                result += (outerBorder ? separator : ""); // If border wanted, add it
                for (int file = 0; file < inpFiles.Count(); file++)
                { // Loop all files
                    if (line < inpFiles[file].LineCount(delimiter))
                    { // Check if there are enough lines in file
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
                        if (headerDivider != "") // if headerDivider assisgned
                            result += file == 0 // and it its first line
                                ? headerDivider // use headerDivider as separator
                                : separator; // else default the action
                        else // Use default separator
                            result += separator;
                    }
                    else  // If there are not enough lines, use placehoder created with filler
                        result += Aligner.CenterAlign("", widths[file], filler) + separator;
                }
                if (!outerBorder) // If border not wanted, remove the last separator
                    result = result.Substring(0, result.Length - separator.Length) + '\n';
            }

            // Do the job and exit
            Console.Write(result);
            Environment.Exit(0);
        }
    }
}
