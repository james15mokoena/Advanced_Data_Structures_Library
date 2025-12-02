namespace DataStructuresLib.Text.PatternMatching;

/// <summary>
/// Implements pattern matching algorithms.
/// </summary>
public static class PatternMatching
{
    /// <summary>
    /// Returns the lowest index at which subtring <strong>pattern</strong> begins in <em>text</em> or
    /// else <strong>-1</strong>.
    /// </summary>
    /// <param name="text">A string to be searched.</param>
    /// <param name="pattern">A pattern to be searched for in the <em>text</em> string.</param>
    /// <returns></returns>
    public static int FindWithBrute(char[] text, char[] pattern)
    {
        int n = text.Length;
        int m = pattern.Length;

        // try every starting index in text.
        for (int i = 0; i <= n - m; ++i)
        {
            // k is index into pattern.
            int k = 0;
            while (k < m && text[i + k] == pattern[k])
                ++k;
            
            // if we reach the end of the pattern, then substring text[i..i+m-1] is a match.
            if (k == m)
                return i;
        }
        return -1;
    }
}