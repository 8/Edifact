using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Edifact
{
  public class EdifactStreamWriter
  {
    public EdifactParseSettings Settings { get; private set; }

    public EdifactStreamWriter()
    {
      this.Settings = new EdifactParseSettings();
    }

    public void Write(EdifactModel model, TextWriter writer)
    {
      Write(model, writer, this.Settings);
    }

    /// <summary>This function escapes the string using the releaseChar whenever it encounters a special char</summary>
    internal void WriteStringEscaped(string str,
                                     TextWriter writer,
                                     char releaseChar,
                                     params char[] specialChars)
    {
      foreach (char c in str)
      {
        if (specialChars.Contains(c)) /* if it's a special char... */
          writer.Write(releaseChar);  /* ...escape it*/

        writer.Write(c); /* write the char */
      }
    }

    public void Write(EdifactModel model, TextWriter writer, EdifactParseSettings settings)
    {
      char[] specialChars = settings.GetSpecialChars();

      /* write the segments */
      foreach (var segment in model.Segments)
      {
        /* write the segment type */
        WriteStringEscaped(segment.SegmentType, writer, settings.ReleaseCharacter, specialChars);

        /* write the data elements */
        foreach (var dataElement in segment.DataElements)
        {
          /* write the tag / data element separator */
          writer.Write(settings.SegmentTagDataElementSeparator);

          for (int i = 0; i < dataElement.Components.Count; i++)
          {
            /* write the component data element separator foreach component after the first */
            if (i != 0) writer.Write(settings.ComponentDataElementSeparator);

            /* write the component value */
            WriteStringEscaped(dataElement.Components[i], writer, settings.ReleaseCharacter, specialChars);
          }
        }

        /* write the segment termination */
        writer.Write(settings.SegmentTerminator);
      }
    }

  }
}
