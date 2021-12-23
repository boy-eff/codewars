using System;
using System.Text.RegularExpressions;
using System.Text;

public class MorseCodeDecoder
{
    public static string DecodeBits(string bits)
        {
            Console.WriteLine(bits);
            bits = bits.Trim('0');
            Console.WriteLine(bits);
            int currentSequence = 0;
            int minSequence = 0;
            bool minFounded = false;
            int i = 0;
            while (i < bits.Length && currentSequence <= minSequence && !minFounded)
            {
                if (bits[i] == '1')
                {
                    currentSequence = 0;
                    do
                    {
                        currentSequence++;
                        i++;
                    }
                    while (i < bits.Length && bits[i] == '1');
                    if (minSequence == 0)
                    {
                        minSequence = currentSequence;
                    }
                    else if (currentSequence < minSequence)
                    {
                        minSequence = currentSequence;
                        minFounded = true;
                    }
                }
                else 
                  {
                  currentSequence = 0;
                    do
                    {
                        currentSequence++;
                        i++;
                    }
                    while (i < bits.Length && bits[i] == '0');
                    if (minSequence == 0)
                    {
                        minSequence = currentSequence;
                    }
                    else if (currentSequence < minSequence)
                    {
                        minSequence = currentSequence;
                        minFounded = true;
                    }
                }
            }
      


            StringBuilder sb = new StringBuilder();
            i = 0;
            while (i < bits.Length)
            {
                if (bits[i] == '1')
                {
                    if (i + minSequence < bits.Length && bits[i + minSequence] == '1' )
                    {
                        sb.Append('-');
                        i += minSequence * 3;
                    }
                    else
                    {
                        sb.Append('.');
                        i += minSequence;
                    }
                }
                else
                {
                    if (bits[i + minSequence] == '0')
                    {
                        sb.Append(' ');
                        i += minSequence * 3;
                    }
                    else i += minSequence;
                }
            }
            
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

    public static string DecodeMorse(string morseCode)
    {
        morseCode = morseCode.Trim();
            StringBuilder sb = new StringBuilder();
            int beginning = 0;
            int ending = 0;
            while (ending < morseCode.Length)
            {
                if (morseCode[ending] == ' ')
                {
                    sb.Append(MorseCode.Get(morseCode.Substring(beginning, ending - beginning)));
                    ending++;
                    if (morseCode[ending] == ' ')
                    {
                        ending++;
                        sb.Append(' ');
                    }
                        
                    beginning = ending;

                }
                else ending++;
            }
            sb.Append(MorseCode.Get(morseCode.Substring(beginning, ending - beginning)));
            return sb.ToString();
    }
}
