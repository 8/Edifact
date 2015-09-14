using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edifact
{
  public class EdifactModel
  {
    /// <summary>Gets or sets the UNA parse settings of the model</summary>
    public EdifactParseSettings UNA { get; set; }

    /// <summary>Gets the list of its segments</summary>
    public List<EdifactSegmentModel> Segments { get; private set; }

    public EdifactModel()
    {
      this.Segments = new List<EdifactSegmentModel>();
    }

    public EdifactModel(EdifactSegmentModel segment)
      : this()
    {
      this.Segments.Add(segment);
    }

    public override string ToString()
    {
      return string.Format("Segments.Count={0}", this.Segments.Count);
    }
  }
}
