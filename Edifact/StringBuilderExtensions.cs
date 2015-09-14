using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edifact
{
  static class StringBuilderExtensions
  {
    internal static string Cut(this StringBuilder stringBuilder)
    {
      string ret = stringBuilder.ToString();
      stringBuilder.Clear();
      return ret;
    }
  }
}
