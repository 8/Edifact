using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Edifact.Test
{
  [TestClass]
  public class EdifactStreamReaderTest
  {
  

    [TestMethod]
    public void EdifactStreamReaderTest_Ctor()
    {
      var reader = new EdifactStreamReader();
    }

    [TestMethod]
    public void EdifactStreamReaderTest_Read_UNA()
    {
      var reader = new EdifactStreamReader();
      using (var sr = File.OpenText(TestData.GetTestFilePath()))
      {
        var model = reader.Read(sr);

        Assert.AreEqual<char>(':', model.UNA.ComponentDataElementSeparator);
        Assert.AreEqual<char>('?', model.UNA.ReleaseCharacter);
        Assert.AreEqual<char>('+', model.UNA.SegmentTagDataElementSeparator);
        Assert.AreEqual<char>('\'', model.UNA.SegmentTerminator);
        Assert.AreEqual<char>('.', model.UNA.DecimalNotification);
      }
    }

    [TestMethod]
    public void EdifactStreamReaderTest_Read_All()
    {
      var reader = new EdifactStreamReader();
      using (var sr = File.OpenText(TestData.GetTestFilePath()))
      {
        var model = reader.Read(sr);
        Assert.AreEqual<int>(15, model.Segments.Count);
        int i = 0;
        Assert.AreEqual<string>("UNB", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(5, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("UNH", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(2, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("MSG", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(1, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("IFT", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(2, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("ERC", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(1, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("IFT", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(2, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("ODI", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(0, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("TVL", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(6, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("PDI", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(4, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("APD", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(7, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("TVL", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(6, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("PDI", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(2, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("APD", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(8, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("UNT", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(2, model.Segments[i++].DataElements.Count);

        Assert.AreEqual<string>("UNZ", model.Segments[i].SegmentType);
        Assert.AreEqual<int>(2, model.Segments[i++].DataElements.Count);
      }
    }

    [TestMethod]
    public void EdifactStreamReaderTest_Read_All2()
    {
      var reader = new EdifactStreamReader();
      using (var sr = File.OpenText(TestData.GetTestFilePath(TestData.SampleFile2Name)))
      {
        var model = reader.Read(sr);
        Assert.AreEqual<int>(15, model.Segments.Count);
      }
    }

  }
}
