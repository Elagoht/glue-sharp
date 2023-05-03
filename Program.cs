using NDesk.Options;

namespace Glue
{
    public static class Program
    {
        // Create option variables 
        private static string delimiter = "\n";
        private static string separator = " ";
        private static char filler = ' ';
        private static Alignment alignment = Alignment.Left;
        private static bool align = true;
        private static bool transpose = false;
        private static bool deleteLastBlank = false;
        private static bool outerBorder = false;
        private static bool help = false;
        private static string headerDivider = "";
        private static void Main(string[] args)
        {
            if (args.Length == 0 && !Console.IsInputRedirected)
            { // If there is no argument, warn the user
                Console.Error.WriteLine("\x1b[31;1mInsufficent arguments. Try --help for more information.\x1b[0m");
                Environment.Exit(1);
            }
            // Check arguments and set options
            OptionSet optionSet = new OptionSet()
            {
                {"h|help",  "Show this message and exit", value => help = value != null },
                {"a=|alignment=",
@"Determine what alignment will be used
Valid values : 
   To left   : (default)
   To center : 0 | center
   To right  : 1 | right",
                    (string value) => {
                        alignment = value == "0" || value.ToLower() =="center"
                            ? Alignment.Center : (value == "1" || value.ToLower() == "right"
                                ? Alignment.Right : Alignment.Left );
                    }
                },
                { "n|noalign", "Do not align fields, overwrites alignment option", (string value)=> { align = value != "no-align" && value !="n"; } },
                { "d=|delimiter=", "String value that will split the file contents", (string value) => { delimiter = value; } },
                { "s=|separator=", "String value that will bind the new parts", (string value) => { separator = value; } } ,
                { "f=|filler=", "Determine what empty areas will be filled with", (string value) => { filler = char.Parse(value.Substring(0,1)); } },
                { "H=|header-divider=", "Add a divider after first column/row, overwrites alignment", (string value) => {
                    headerDivider = value;
                    align = true; }
                },
                { "t|transpose", "Swap columns and rows", (string value) => {transpose = value == "transpose" || value == "t"; } },
                { "r|remove-last-blank", "Delete last blank lines to minimize the output", (string value) => {deleteLastBlank = value == "remove-last-blank" || value == "r"; } },
                { "b|border", "Add extra separators at the beginning and end of each line", (string value) => {outerBorder = value == "border" || value == "b"; } },
                { "csv", "Csv with semicolon, same as -r -t -n -s \";\"", (string value) => {
                    if (transpose = value == "csv") {
                        separator = ";";
                        align = false;
                        transpose = true;
                        deleteLastBlank = true;
                    } }
                },
                { "csv2", "Csv with comma, same as -r -t -n -s \",\"", (string value) => {
                    if (transpose = value == "csv2") {
                        separator = ",";
                        align = false;
                        transpose = true;
                        deleteLastBlank = true;
                    } }
                },
                { "m|markdown", "Create markdown table formatted output, same as -r -t -s \" | \" -b", (string value)=> {
                        separator = " | ";
                        transpose = true;
                        deleteLastBlank = true;
                        headerDivider = "-";
                        outerBorder = true;
                    }
                }
            };

            // Get unregistered arguments as files
            List<string> files = new List<string>();

            try
            { // Get other arguments as files
                files = optionSet.Parse(args);
            }
            catch (OptionException)
            { // If cannot parse options show help and exit
                Console.Error.WriteLine("\x1b[31;1mUnexpected error with commandline arguments. Try --help for more information.\x1b[0m");
                Environment.Exit(1);
            }

            // Check if data piped from another process
            if (Console.IsInputRedirected)
                using (var reader = new StreamReader(Console.OpenStandardInput()))
                { // Read piped data from the standard input
                    files.AddRange(
                        reader.ReadToEnd().Trim() // Pipe data
                        .Split(' ', '\n') // Piped file names
                    ); // Add to existing files
                }

            if (help) // Show help page
                ShowHelp(optionSet);

            if (files.Count() == 0)
            { // If has no file input, close
                Console.Error.WriteLine("\x1b[31;1mThere is no file input. Try --help for more information\x1b[0m");
                Environment.Exit(0);
            }

            // Default action
            if (transpose)
            { // If transposed
                if (align) // if aligned call the method and exit
                    Merger.HorizontalAligned(delimiter, separator, alignment, filler, outerBorder, headerDivider, InpFiles(files));
                // If not aligned call the method and exit
                Merger.Horizontal(delimiter, separator, outerBorder, InpFiles(files));
            }
            if (align) // If aligned call the method and exit
                Merger.VerticalAligned(delimiter, separator, alignment, filler, outerBorder, headerDivider, InpFiles(files));
            // If not aligned call the method and exit
            Merger.Vertical(delimiter, separator, outerBorder, InpFiles(files));

            // Default Exit
            Environment.Exit(0);
        }
        private static InpFile[] InpFiles(List<string> fileNames)
        { // Get all input files
            InpFile[] inpFiles = new InpFile[fileNames.Count()]; // Get all files
            for (int fileName = 0; fileName < fileNames.Count(); fileName++)
            { // Loop all file names
                inpFiles[fileName] = new InpFile(fileNames[fileName]); // Create file objects
                if (deleteLastBlank) // If last blank lines not wanted
                    inpFiles[fileName].DeleteLastBlankSlice(delimiter); // delete them
            }
            return inpFiles;
        }
        private static void ShowHelp(OptionSet options)
        { // Show help and exit
            Console.WriteLine("Usage: sharpglue [OPTIONS] [INPUT FILES]\nOptions:");
            options.WriteOptionDescriptions(Console.Out);
            Console.WriteLine("\nDev Homepage : https://github.com/Elagoht/sharpglue\nBug Reports  : https://github.com/Elagoht/sharpglue/issues");
            Environment.Exit(0);
        }
    }
}
