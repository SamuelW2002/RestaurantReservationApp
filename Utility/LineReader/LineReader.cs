using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.ReadLine
{
    public class LineReader : ILineReader
    {
        public string ReadLine()
        {
            return CheckInput(Console.ReadLine());
        }

        public string ReadOptionalLine()
        {
            return CheckOptionalInput(Console.ReadLine());
        }

        private string CheckInput(string input)
        {
            while (input == null || input == "")
            {
                Console.WriteLine("Input cannot be empty. Please enter new input.");
                input = Console.ReadLine();
            }
            return input.Trim();
        }

        private string CheckOptionalInput(string input)
        {
            return input.Trim();
        }
    }
}
