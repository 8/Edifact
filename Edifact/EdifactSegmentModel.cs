using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edifact
{
  [DebuggerDisplay("SegmentType: {SegmentType}, DataElements.Count: {DataElements.Count}")]
  public class EdifactSegmentModel
  {
    /// <summary>Gets or sets the SegmentType / Tag</summary>
    public string SegmentType { get; set; }

    /// <summary>Gets the list of its data elements</summary>
    public List<EdifactDataElement> DataElements { get; private set; }

    public EdifactSegmentModel()
    {
      this.DataElements = new List<EdifactDataElement>();
    }

    public EdifactSegmentModel(string segmentType)
      : this()
    {
      this.SegmentType = segmentType;
    }

    public override string ToString()
    {
      return string.Format("SegmentType={0}, DataElements.Count={1}",
                           this.SegmentType, this.DataElements.Count);
    }
  }
}
