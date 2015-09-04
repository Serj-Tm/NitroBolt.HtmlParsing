using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace NitroBolt.HtmlParsing
{
  public class HtmlDtd
  {
    public const string Html = "Html.dtd";
    public const string Frameset = "frameset.dtd";
    public const string Loose = "loose.dtd";
    public const string Sloppy = "Sloppy.dtd";
    public const string Strict = "strict.dtd";
  }

  public class HtmlReader:
    Sgml.SgmlReader
  {
    public HtmlReader(TextReader reader):
      this(reader, "Html.dtd")
    {
    }
    public HtmlReader(TextReader reader, string dtdName)
    {
      base.CaseFolding = Sgml.CaseFolding.ToLower;
      base.InputStream = reader;
      base.DocType = "HTML";
      base.Dtd = ParseDtd(NameTable, dtdName);
    }
    private Sgml.SgmlDtd ParseDtd(XmlNameTable nt, string name)
		{
			Stream stream = this.GetType().Assembly.GetManifestResourceStream(this.GetType(), name);
			StreamReader reader = new StreamReader(stream);
			return Sgml.SgmlDtd.Parse(null, "HTML", reader, null, null, nt);
		}

  }

}
