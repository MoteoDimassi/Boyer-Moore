using System;
using System.Collections.Generic;

namespace Boyer_Moore
{
    class Program
    {
        static void Main(string[] args)
        {
			string str = "bbbbbbbb";
			string sub_str = "aaa";
			int[] index = SearchString(str, sub_str);

			foreach (var item in index)
            {
				Console.WriteLine("Вхождение подстроки на " + (item + 1) + " месте");
            }
        }
		public static int[] SearchString(string base_line, string sub_line)
		{
			List<int> retVal = new List<int>();
			int subLength = sub_line.Length;
			int baseLength = base_line.Length;

			int[] badChar = new int[subLength+1];

			int counter = 0;

			BadCharHeuristic(sub_line, subLength, ref badChar);

			int index = 0;
			int j = subLength - 1;
			while (index <= (baseLength - subLength))
			{
				
				j = subLength - 1;

				do
				{
					//Console.WriteLine($"{sub_line[j]} = {base_line[index + j]}");
					--j;
					counter++;
				} while (j >= 0 && sub_line[j] == base_line[index + j]);


				if (j < 0)
				{
					retVal.Add(index);
					index += badChar[subLength-1];
				}
				else
				{
                    if (sub_line.Contains(base_line[index+j]))
                    {
						//Console.WriteLine("Сейчас рассматривается элемент "+ base_line[index + j]+ " сдвиг произойдёт на "+ badChar[sub_line.IndexOf(base_line[index + j])]);
						index += badChar[sub_line.IndexOf(base_line[index+j])];
                    }
                    else
                    {
							index += badChar[subLength];
					}
					
				}
				//counter++;
			}

			Console.WriteLine(counter + " количество вхождений");
			return retVal.ToArray();
		}

		private static void BadCharHeuristic(string sub_line, int size, ref int[] badChar)
		{
			string sub_line1 = sub_line.Substring(0, size - 1);
			badChar[badChar.Length-1] = badChar.Length-1;
			
			for (int i = sub_line1.Length-1; i >=0; i--)
            {
				
                    if (badChar[sub_line1.LastIndexOf(sub_line1[i])] != 0)
                    {
					
						
						badChar[i] = badChar[sub_line1.LastIndexOf(sub_line1[i])];

					}

                    else
                    {

						badChar[i] = sub_line.Length - 1 - i;
                    }
				
               
            }
			badChar[size - 1] = badChar[sub_line1.IndexOf(sub_line[size - 1])];
			foreach (var item in badChar)
				Console.Write(item + " ");
		}
	}
}
