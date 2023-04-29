using NDesk.Options;

namespace Glue
{
    public static class Program
    {
        static string delimiter = "\n";
        static string separator = " ";
        static bool help = false;

        static int Main(string[] args)
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
            {"d=|delimiter=", "Determine what the delimiter should be", (string value) => delimiter = value },
            {"s=|separator=", "Determine what the separator should be", (string value) => separator = value }};
            string[] files = { };
            try
            {
                // Get remaining arguments as files
                files = optionSet.Parse(args).ToArray();
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `--help` for more information.");
                Environment.Exit(1);
            }
            // Help Page
            if (help)
            {
                ShowHelp(optionSet);
                Environment.Exit(0);
            }
            // Default action
            {
                /* Console.WriteLine(
                    Merger.Vertical(delimiter, separator, GetInpFiles(files))
                ); */
                Console.WriteLine(
                    Merger.VerticalAligned(delimiter, separator, Merger.Direction.Center, ' ', GetInpFiles(files))
                );
            }
            // Default Exit
            return 0;
        }
        // Get all input files
        static InpFile[] GetInpFiles(string[] fileNames)
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
