using NDesk.Options;

namespace Glue
{
    public static class Program
    {
        static string delimiter = "\n";
        static string separator = " ";
        static char filler = ' ';
        static Alignment alignment = Alignment.Left;
        static bool align = true;
        static bool help = false;

        static void Main(string[] args)
        {
            // If there is no argument, close
            if (args.Length == 0)
            {
                Console.WriteLine("Insufficent agruments.");
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
                            ? Alignment.Center
                            : (value == "1" || value.ToLower() == "right"
                                ? Alignment.Right
                                : Alignment.Left );
                    }
                },
                { "n|no-align", "Do not align fields, overwrites alignment optin", (string value)=> { align = value != "no-align" && value !="n"; } },
                { "d=|delimiter=", "String value that will split the file contents", (string value) => { delimiter = value; } },
                { "s=|separator=", "String value that will bind the new parts", (string value) => { separator = value; } } ,
                { "f=|filler=", "Determine what empty areas will be filled with", (string value) => { filler = char.Parse(value.Substring(0,1)); } },
            };

            // Get unregistered arguments as files
            string[] files = { };

            try
            {
                files = optionSet.Parse(args).ToArray();
            }
            catch (OptionException e)
            {
                // If cannot parse options show help and exit
                Console.WriteLine(e.Message);
                ShowHelp(optionSet);
                Environment.Exit(1);
            }

            // Show help page
            if (help)
            {
                ShowHelp(optionSet);
                Environment.Exit(0);
            }

            // Default action
            {
                if (align)
                {
                    Console.WriteLine(Merger.VerticalAligned(delimiter, separator, alignment, filler, InpFiles(files)));
                }
                else
                {
                    Console.WriteLine(Merger.Vertical(delimiter, separator, InpFiles(files)));
                }
            }

            // Default Exit
            Environment.Exit(0);
        }
        // Get all input files
        static InpFile[] InpFiles(string[] fileNames)
        {
            InpFile[] inpFiles = new InpFile[fileNames.Length];
            for (int index = 0; index < fileNames.Length; index++)
            {
                inpFiles[index] = new InpFile(fileNames[index]);
            }
            return inpFiles;
        }
        static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: sharpglue [OPTIONS] [INPUT FILES]");
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}
