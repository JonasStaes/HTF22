using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTF22_1 {
    public static class Answer {

        readonly static Dictionary<char, int> romanNumerals = new() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };

        //directionary to hold the roman numerals
        readonly static Dictionary<string, int> m_RomanNumerals = new() {
            { "I", 1 },
            { "IV", 4},
            { "V", 5 },
            { "IX", 9 },
            { "X", 10 },
            { "XL", 40 },
            { "L", 50 },
            { "XC", 90 },
            { "C", 100 },
            { "CD", 400 },
            { "D", 500 },
            { "CM", 900 },
            { "M", 1000 }
        };

        public static string GetAnswer(List<string> numerals) {
            
            int output = 0;
            numerals.ForEach(x => {
                int sum = 0;
                for(int i = 0; i <= x.Length-1; i++) {
                    char current = x[i];
                    romanNumerals.TryGetValue(current, out int num);
                    if(i+1 < x.Length && romanNumerals[x[i+1]] > romanNumerals[current]) {
                        sum -= num;
                    } else {
                        sum += num;
                    }
                }
                output += sum;
            });

            return Convert(output);
        }

        public static string Convert(int value) {
            string toReturn = "";

            if(value < 1 || value > 3999) {
                throw new ArgumentOutOfRangeException("value", "Value must be between 1 and 3999");
            }

            foreach(var numeral in m_RomanNumerals.Reverse()) {
                if(value <= 0)
                    break;

                while(value >= numeral.Value) {
                    value -= numeral.Value;
                    toReturn += numeral.Key;
                }
            }

            return toReturn;
        }
    }
}
