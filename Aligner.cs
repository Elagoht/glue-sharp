namespace Glue
{
    public enum Alignment
    {
        Left,
        Center,
        Right
    }
    public static class Aligner
    {
        // Align Left
        public static string LeftAlign(string text, int width, char filler = ' ')
        {
            return text.PadRight(width, filler)
                .Substring(0, width);
        }
        // Align Center
        public static string CenterAlign(string text, int width, char filler = ' ')
        {
            int remaining = width - text.Length;
            int left = remaining / 2;
            int right = remaining - left;

            return text
                .PadLeft(text.Length + left, filler)
                .PadRight(width, filler);
        }
        // Align Right
        public static string RightAlign(string text, int width, char filler = ' ')
        {
            return text
                .PadLeft(width, filler)
                .Substring(0, width);
        }
    }
}