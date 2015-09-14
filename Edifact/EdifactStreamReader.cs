using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Edifact
{
  public class EdifactStreamReader
  {
    public EdifactParseSettings Settings { get; private set; }
    
    public EdifactStreamReader()
    {
      this.Settings = new EdifactParseSettings();
    }

    enum ParserState
    {
      Tag,
      DataElement
    };

    /// <summary>Tries to read the UNA segment that contains the ParseSettings</summary>
    private EdifactParseSettings ReadUNA(StreamReader sr, out Queue<char> charsNeedProcessing)
    {
      /* create the default settings */
      EdifactParseSettings settings = null;
      charsNeedProcessing = new Queue<char>();

      /* UNA:+.? '               012345678 */
      char[] buffer = new char[ "UNA:+.? '".Length ];
      int count = sr.ReadBlock(buffer, 0, buffer.Length);

      /* validate the input */
      if (count == buffer.Length &&
          buffer[0] == 'U' &&
          buffer[1] == 'N' &&
          buffer[2] == 'A')
      {
        settings = new EdifactParseSettings()
        {
          ComponentDataElementSeparator  = buffer[3],
          SegmentTagDataElementSeparator = buffer[4],
          DecimalNotification            = buffer[5],
          ReleaseCharacter               = buffer[6],
          /* reserverd                   = buffer[7] */
          SegmentTerminator              = buffer[8]
        };
      }
      else
      { /* if there is no match for UNA, return the read characters so that they can be processed further and are not lost */
        for (int i = 0; i < count; i++)
          charsNeedProcessing.Enqueue(buffer[i]);
      }

      return settings;
    }

    public EdifactModel Read(StreamReader sr)
    {
      return Read(sr, this.Settings);
    }

    public EdifactModel Read(StreamReader sr,
                             EdifactParseSettings defaultSettings)
    {
      var model       = new EdifactModel();        /* the current model, start with a new edifact model */
      var segment     = new EdifactSegmentModel(); /* the current segment, start with a new segment */
      var dataElement = new EdifactDataElement();  /* the current dataElement, start with a new data element */

      bool isEscaped = false;              /* true, if the last character was a ReleaseCharacter */
      ParserState state = ParserState.Tag; /* start in SegmentTag mode */

      StringBuilder textbuffer = new StringBuilder();    /* text buffer */

      /* try to read the parse settings by looking for the UNA element at the start of the stream */
      Queue<char> unprocessedChars;
      EdifactParseSettings settings = ReadUNA(sr, out unprocessedChars);
      model.UNA = settings; /* store the UNA parse settings of the model */
      if (settings == null) /* if the stream does not contain settings (it is missing the UNA segment) */
        settings = defaultSettings; /* then use the provided defaultSettings for parsing the model */

      int r;
      /* start with the unprocessedChars from the ReadUNA read attempt, then switch to the rest of the stream */
      while ( (r = (unprocessedChars.Count > 0 ? unprocessedChars.Dequeue() : sr.Read())) != -1)
      {
        char c = (char)r;

        bool add = false;

        if (isEscaped) /* if the previous character was an escape */
        {
          add = true;        /* then add it to the text buffer */
          isEscaped = false; /* and stop the escaping */
        }
        else
        {
          if (c == settings.ReleaseCharacter) /* if it's an escape character, escape the next char */
            isEscaped = true;
          else if (c == settings.SegmentTagDataElementSeparator) /* if segment tag / data element change occurs */
          {
            switch (state)
            {
              case ParserState.Tag:
                segment.SegmentType = textbuffer.Cut(); /* set the collected text as the segments type and reset the buffer */
                state = ParserState.DataElement;        /* the next separators will separate data elements */
                break;

              case ParserState.DataElement:
                dataElement.Components.Add(textbuffer.Cut()); /* complete the component */
                segment.DataElements.Add(dataElement);        /* add the completed dataElement to segment */
                dataElement = new EdifactDataElement();       /* start a new dataElement */
                break;
            }
          }
          else if (c == settings.ComponentDataElementSeparator)
          {
            dataElement.Components.Add(textbuffer.Cut()); /* complete the component */
          }
          else if (c == settings.SegmentTerminator) /* if it's the end of the segment */
          {
            if (segment.SegmentType == null) /* complete the segment if it's not completed yet */
              segment.SegmentType = textbuffer.Cut();
            else
            {
              dataElement.Components.Add(textbuffer.Cut()); /* complete the component */
              segment.DataElements.Add(dataElement);  /* add the completed dataElement to the segment */
              dataElement = new EdifactDataElement(); /* and start a new dataElement */
            }

            model.Segments.Add(segment);           /* add the completed segment to the model */
            segment = new EdifactSegmentModel();   /* and start a new segment */
            state = ParserState.Tag;      /* switch back to the SegmentTag state */
          }
          else if (c == '\r' || c == '\n') { } /* ignore line breaks */
          else add = true;                     /* if this was no special character, add it to the buffer */
        }

        if (add)
          textbuffer.Append(c); /* collect it in the text buffer */

      }

      

      return model;
    }
  }
}
