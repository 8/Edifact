using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edifact.Test
{
  [TestClass]
  public class EdifactStreamWriterTest
  {
    [TestMethod]
    public void EdifactStreamWriterTest_Ctor()
    {
      new EdifactStreamWriter();
    }

    [TestMethod]
    public void EdifactStreamWriterTest_Write()
    {
      var writer = new EdifactStreamWriter();
      var sb = new StringBuilder();

      EdifactModel model;
      using (var sr = File.OpenText(TestData.GetTestFilePath(TestData.SampleFile2Name)))
        model = new EdifactStreamReader().Read(sr);

      using (var sw = new StringWriter(sb))
        writer.Write(model, sw);

      string written = sb.ToString();
      string sampleFile2 = File.ReadAllText(TestData.GetTestFilePath(TestData.SampleFile2Name));

      for (int i = 0; i < sampleFile2.Length; i++)
        Assert.AreEqual<char>(sampleFile2[i], written[i], 
          string.Format("error at char {0}, should be {1} but is {2}", i, sampleFile2[i], written[i]));
    }
  }
}
