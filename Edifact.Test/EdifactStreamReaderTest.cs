﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public const string TestDataFolderName = "TestData";

    public string Folder { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }

    public string TestDataFolder { get { return Path.Combine(this.Folder, TestDataFolderName); } }

    public const string SampleFileName = "Sample.edi";
    public const string SampleFile2Name = "Sample2.edi";

    public string GetTestFilePath(string fileName = SampleFileName)
    {
      return Path.Combine(TestDataFolder, fileName);
    }

    [TestMethod]
    public void EdifactStreamReaderTest_Ctor()
    {
      var reader = new EdifactStreamReader();
    }

    [TestMethod]
    public void EdifactStreamReaderTest_Read_UNA()
    {
      var reader = new EdifactStreamReader();
      using (var sr = File.OpenText(GetTestFilePath()))
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
      using (var sr = File.OpenText(GetTestFilePath()))
      {
        var model = reader.Read(sr);
        Assert.AreEqual<int>(15, model.Segments.Count);
        int i = 0;
        Assert.AreEqual<string>("UNB", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("UNH", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("MSG", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("IFT", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("ERC", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("IFT", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("ODI", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("TVL", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("PDI", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("APD", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("TVL", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("PDI", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("APD", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("UNT", model.Segments[i++].SegmentType);
        Assert.AreEqual<string>("UNZ", model.Segments[i++].SegmentType);
      }
    }

    [TestMethod]
    public void EdifactStreamReaderTest_Read_All2()
    {
      var reader = new EdifactStreamReader();
      using (var sr = File.OpenText(GetTestFilePath(SampleFile2Name)))
      {
        var model = reader.Read(sr);
        Assert.AreEqual<int>(15, model.Segments.Count);
      }
    }

  }
}