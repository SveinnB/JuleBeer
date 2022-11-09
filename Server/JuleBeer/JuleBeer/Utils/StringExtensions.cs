namespace JuleBeer.Utils;

public static class StringExtensions
{
    public static string MaxLengt(this string s, int maxLengt)
    {
        if (s.Length > maxLengt)
        {
            return s.Substring(0, maxLengt);
        }
        else
        {
            return s;
        }
    }

    public static string FirstLetterToUpper(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return s;
        }
        else
        {
            var firstLetter = s.First().ToString().ToUpper();
            if (s.Length > 1)
            {
                return firstLetter + s.Substring(1);
            }
            else
            {
                return firstLetter;
            }
        }
    }
}
