using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Edifact.Test
{
  class TestData
  {
    public const string TestDataFolderName = "TestData";

    public static string Folder { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }

    public static string TestDataFolder { get { return Path.Combine(Folder, TestDataFolderName); } }

    public const string SampleFileName = "Sample.edi";
    public const string SampleFile2Name = "Sample2.edi";

    public static string GetTestFilePath(string fileName = SampleFileName)
    {
      return Path.Combine(TestDataFolder, fileName);
    }
  }
}
