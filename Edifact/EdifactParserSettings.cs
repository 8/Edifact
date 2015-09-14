using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edifact
{
  public class EdifactParseSettings
  {
    public char SegmentTerminator { get; set; }
    public char SegmentTagDataElementSeparator { get; set; }
    public char ComponentDataElementSeparator { get; set; }
    public char ReleaseCharacter { get; set; }
    public char DecimalNotification { get; set; }

    public EdifactParseSettings()
    {
      this.SegmentTagDataElementSeparator = '+';
      this.ComponentDataElementSeparator = ':';
      this.ReleaseCharacter = '?';
      this.SegmentTerminator = '\'';
      this.DecimalNotification = '.';
    }

    public char[] GetSpecialChars()
    {
      return new[] {
        this.SegmentTerminator,
        this.SegmentTagDataElementSeparator,
        this.ComponentDataElementSeparator,
        this.ReleaseCharacter
      };
    }
  }
}
