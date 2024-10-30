using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.ReadLine
{
    public interface ILineReader
    {
        string ReadLine();
        string ReadOptionalLine();
    }

}
