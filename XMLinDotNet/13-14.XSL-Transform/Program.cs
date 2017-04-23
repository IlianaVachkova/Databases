using System;
using System.Xml.Xsl;

class Program
{
    static void Main()
    {
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load("../../catalog.xsl");
        xslt.Transform("../../catalog.xml", "../../catalog.html");
    }
}
