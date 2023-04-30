using NDesk.Options;

namespace Glue
{
    public static class Program
    {
        private static string delimiter = "\n";
        private static string separator = " ";
        private static char filler = ' ';
        private static Alignment alignment = Alignment.Left;
        private static bool align = true;
        private static bool transpose = false;
        private static bool help = false;

        private static void Main(string[] args)
        {
            // If there is no argument, close
            if (args.Length == 0 && !Console.IsInputRedirected)
            {
                Console.Error.WriteLine("\x1b[31;1mInsufficent arguments. Try --help for more information.\x1b[0m");
                Environment.Exit(1);
            }
            // Check arguments
            OptionSet optionSet = new OptionSet() {
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
                { "t|transpose", "Swap columns and rows", (string value) => { transpose = value == "transpose" || value == "t"; } },
                { "csv", "Csv with semicolon, same as -t -n -s \";\"", (string value) => {
                    if (transpose = value == "csv") {
                        separator= ";";
                        align=false;
                        transpose=true;
                    }}
                },
                { "csv2", "Csv with comma, same as -t -n -s \",\"", (string value) => {
                    if (transpose = value == "csv2") {
                        separator= ",";
                        align=false;
                        transpose=true;
                    }}
                }
            };


            // Get unregistered arguments as files
            List<string> files = new List<string>();

            try
            {
                files = optionSet.Parse(args);
            }
            catch (OptionException)
            {
                // If cannot parse options show help and exit
                Console.Error.WriteLine("\x1b[31;1mUnexpected error with commandline arguments. Try --help for more information.\x1b[0m");
                Environment.Exit(1);
            }

            // Check if data piped from another process
            if (Console.IsInputRedirected)
            {
                // Read piped data from the standard input
                using (var reader = new StreamReader(Console.OpenStandardInput()))
                {
                    files.AddRange(
                        reader.ReadToEnd().Trim() // Pipe data
                        .Split(' ', '\n') // Piped file names
                    ); // Add to existing files
                }
            }

            // If has no file input, close
            if (files.Count() == 0)
            {
                Console.Error.WriteLine("\x1b[31;1mThere is no file input. Try --help for more information\x1b[0m");
                Environment.Exit(0);
            }

            // Show help page
            if (help)
            {
                ShowHelp(optionSet);
                Environment.Exit(0);
            }

            // Default action
            if (transpose)
            {
                if (align)
                {
                    Merger.HorizontalAligned(delimiter, separator, alignment, filler, InpFiles(files));
                }
                Merger.Horizontal(delimiter, separator, InpFiles(files));
            }
            if (align)
            {
                Merger.VerticalAligned(delimiter, separator, alignment, filler, InpFiles(files));
            }
            Merger.Vertical(delimiter, separator, InpFiles(files));

            // Default Exit
            Environment.Exit(0);
        }
        // Get all input files
        private static InpFile[] InpFiles(List<string> fileNames)
        {
            InpFile[] inpFiles = new InpFile[fileNames.Count()];
            for (int index = 0; index < fileNames.Count(); index++)
            {
                inpFiles[index] = new InpFile(fileNames[index]);
            }
            return inpFiles;
        }
        private static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: sharpglue [OPTIONS] [INPUT FILES]");
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
