using System.Text.RegularExpressions;

namespace CodeBehind
{
    public static class ExtensionMethodsClass
    {
        public static string GetTextAfterValue(this string Text, string Value)
        {
            if (Text.Length < Value.Length)
                return Text;

            if (!Text.Contains(Value))
                return Text;

            return Text.Substring(Text.IndexOf(Value) + Value.Length);
        }

        public static string GetTextAfterLastValue(this string Text, string Value)
        {
            if (Text.Length < Value.Length)
                return Text;

            if (!Text.Contains(Value))
                return Text;

            return Text.Substring(Text.LastIndexOf(Value) + Value.Length);
        }

        public static string GetTextBeforeValue(this string Text, string Value)
        {
            if (Text.Length < Value.Length)
                return Text;

            if (!Text.Contains(Value))
                return Text;

            return Text.Substring(0, Text.IndexOf(Value));
        }

        public static string GetTextBeforeLastValue(this string Text, string Value)
        {
            if (Text.Length < Value.Length)
                return Text;

            if (!Text.Contains(Value))
                return Text;

            return Text.Substring(0, Text.LastIndexOf(Value));
        }

        public static string ToMethodNameClean(this string Text)
        {
            Regex regex = new Regex("[^a-zA-Z0-9_]");
            return regex.Replace(Text, "_");
        }

        public static bool ClassPathIsFine(this string Text)
        {
            Regex regex = new Regex("[^a-zA-Z0-9_.]");
            return !regex.IsMatch(Text);
        }

        public static int GetNumberOfCharacter(this string Text, char Character)
        {
            if (Text.Length < 1)
                return 0;

            int count = 0;

            for (int i = 0; i < Text.Length; i++)
                if (Text[i] == Character)
                    count++;

            return count;
        }
    }
}
