using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFinal
{
    public class Sequence
    {
        public String text;
        
        public Sequence(String text)
        {
            this.text = text;
        }

        public Sequence mutate()
        {
            Random rnd = new Random();
            int totalGaps = text.Count(c => c == '-');
            int r = rnd.Next(totalGaps) + 1;
            int randomGapIndex = getNthIndex(text, '-', r);
            int randomCharIndex = rnd.Next(text.Length);
            char temp = text[randomCharIndex];

            StringBuilder sb = new StringBuilder(text);
            sb[randomGapIndex] = temp;
            sb[randomCharIndex] = '-';
            text = sb.ToString();

            return new Sequence(text);
        }
        public int getNthIndex(string s, char t, int n)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public int indexof(char c , int start)
        {
            return this.text.IndexOf(c, start);
        }

        public int ld(Sequence seq)
        {
            return levenshteinDistance(text, seq.text);
        }

        private static int levenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            var d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            for (int i = 0; i <= n; ++i)
            {
                d[i, 0] = i;
            }
            for (int j = 0; j <= m; ++j)
            {
                d[j, 0] = j;
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[n, m];
        }

        public override string ToString() { 
            return this.text;
        }

        public int length()
        {
            return this.text.Length;
        }
    }
}
