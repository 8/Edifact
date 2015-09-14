using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edifact
{
  [DebuggerDisplay("Components.Count: {Components.Count}")]
  public class EdifactDataElement
  {
    /// <summary>Gets the list of its components</summary>
    public List<string> Components { get; private set; }

    public EdifactDataElement()
    {
      this.Components = new List<string>();
    }

    public EdifactDataElement(string firstComponent)
      : this()
    {
      this.Components.Add(firstComponent);
    }

    public override string ToString()
    {
      return string.Format("Components.Count={0}", this.Components.Count);
    }

  }
}
