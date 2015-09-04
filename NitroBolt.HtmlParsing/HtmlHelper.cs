using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace NitroBolt.HtmlParsing
{
  public static class HtmlHelper
  {
    public static XElement LoadXElement(string filename)
    {
      return LoadXElement(filename, null);
    }
    public static XElement LoadXElement(string filename, Encoding encoding)
    {
      using (var reader = encoding != null ?  new StreamReader(filename, encoding) : new StreamReader(filename))
      {
        return LoadXElement(reader);
      }
    }
    public static XElement LoadXElement(TextReader reader)
    {
      using (HtmlReader htmlReader = new HtmlReader(reader))
      {
        return XElement.Load(htmlReader);
      }
    }
    public static XElement LoadXElementFromText(string text)
    {
      using (var reader = new StringReader(text))
      {
        return LoadXElement(reader);
      }
    }

    public static XmlDocument LoadDocument(string name)
    {
      using (StreamReader reader = new StreamReader(name))
      {
        return LoadDocument(reader);
      }
    }
    public static XmlDocument LoadDocument(TextReader textReader)
    {
      return LoadDocumentFromText(textReader.ReadToEnd());
    }
    public static XmlDocument LoadDocument(TextReader textReader, string dtdName)
    {
      return LoadDocumentFromText(textReader.ReadToEnd(), dtdName);
    }
    public static XmlDocument LoadDocumentFromText(string text)
    {
      text = RemoveDocType(text);
      using (StringReader stringReader = new StringReader(text))
      using (HtmlReader reader = new HtmlReader(stringReader))
      {
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);
        return doc;
      }
    }
    public static XmlDocument LoadDocumentFromText(string text, string dtdName)
    {
      text = RemoveDocType(text);
      using (StringReader stringReader = new StringReader(text))
      using (HtmlReader reader = new HtmlReader(stringReader, dtdName))
      {
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);
        return doc;
      }
    }
    static string RemoveDocType(string text)
    {
      int index = text.IndexOf("<!DOCTYPE");
      if (index >= 0)
      {
        int end = text.IndexOf(">", index);
        if (end >= 0)
          text = text.Remove(index, end - index + 1);
      }
      return text;
    }
  }
}
