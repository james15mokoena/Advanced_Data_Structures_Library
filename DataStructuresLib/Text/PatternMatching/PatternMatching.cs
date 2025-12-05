using DataStructuresLib.Models;

namespace DataStructuresLib.Text.PatternMatching;

/// <summary>
/// Implements pattern matching algorithms.
/// </summary>
public static class PatternMatching
{
    /// <summary>
    /// Uses the <strong>Brute-Force</strong> pattern-matching algorithm to return the lowest index
    /// at which subtring <strong>pattern</strong> begins in <em>text</em> or else <strong>-1</strong>.
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

    /// <summary>
    /// Uses the <strong>Boyer-Moore</strong> pattern-matching algorithm to return the lowest index
    /// at which substring <strong>pattern</strong> begins in <em>text</em> or else -1.
    /// </summary>
    /// <param name="text">A string to be searched.</param>
    /// <param name="pattern">A pattern to be searched for in the <em>text</em> string.</param>
    /// <returns></returns>
    public static int FindWithBoyerMoore(char[] text, char[] pattern)
    {
        int n = text.Length;
        int m = pattern.Length;

        // a search for an empty string
        if (m == 0)
            return 0;

        // stores the last occurrence of each character in the pattern.
        HashMap<char, int> last = new();

        // set -1 as default for all characters.
        for (int a = 0; a < n; ++a)
            last.Put(text[a], -1);

        // update rightmost occurrence based on appearance in pattern.
        for (int b = 0; b < m; ++b)
            last.Put(pattern[b], b);

        // start with the end of the pattern aligned at index m-1 of the text.
        int i = m - 1;          // an index into the text.
        int k = m - 1;          // an index into the pattern.

        while (i < n)
        {
            // there's a match
            if (text[i] == pattern[k])
            {
                // an entire pattern has been found
                if (k == 0)
                    return i;

                // otherwise, examine previous characters of the text and pattern.
                i--;
                k--;
            }
            else
            {
                /*// Case 1: j < k
                if (last.GetValue(text[i]) >= 0 && last.GetValue(text[i]) < k)
                    i += m - (last.GetValue(text[i]) + 1);
                else if (last.GetValue(text[i]) >= 0 && last.GetValue(text[i]) > k)
                    i += m - k;*/
                // implements the two jump cases
                i += m - Math.Min(k, 1 + last.GetValue(text[i]));
                // restart at the end of pattern
                k = m - 1;
            }
        }
        
        return -1;
    }
}