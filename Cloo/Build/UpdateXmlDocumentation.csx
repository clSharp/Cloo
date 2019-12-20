#! "netcoreapp3.1"
#r "nuget: NetStandard.Library, 2.0.0"
using System.Xml.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

if (Args.Count != 1)
{
    Console.Error.WriteLine("Single argument: Cloo xml file path");
    return;
}


using (var fs = new FileStream(Args [0], FileMode.Open, FileAccess.ReadWrite, FileShare.None))
using (var client = new HttpClient())
{
    var doc = XDocument.Load(fs);
    doc.Descendants("member")
        .Where(e => e.Attributes("name").Single().Value.StartsWith("M:Cloo.Bindings.CL1"))
        .ToList()
        .ForEach(e => 
        {
            string[] nodes = e.Attributes("name")
                        .Single()
                        .Value
                        .Replace("M:Cloo.Bindings.CL1", "")
                        .Split(new [] {"("}, StringSplitOptions.None)
                        .First()
                        .Split(new [] {"."}, StringSplitOptions.None);
            string url = $"https://www.khronos.org/registry/OpenCL/sdk/1.{nodes[0]}/docs/man/xhtml/cl{nodes[1]}.html";
            string url2 = $"https://www.khronos.org/registry/OpenCL/specs/opencl-1.{nodes[0]}.pdf";
            Console.WriteLine(url);

            client.GetStringAsync(url)
                .ContinueWith(t => 
                {
                    if (t.Exception == null)
                    {
                        string description = t.Result
                            .Split(new [] {$"<h1>cl{nodes[1]}</h1>"}, StringSplitOptions.None)
                            .Last()
                            .Split(new [] {"<p>", "</p>"}, StringSplitOptions.None)
                            .Skip(1)
                            .First()
                            .Trim();

                        description = Regex.Replace(description, @"\s+", " ");
                        description += $"\n{url}";
                        description += $"\n{url2}";

                        e.Element("summary").SetValue(description);

                        Console.WriteLine(description);
                    }
                    else
                    {
                        string description = e.Element("summary").Value;
                        description += $"\n{url2}";

                        Console.Error.WriteLine(t.Exception.Message);
                    }
                })
                .Wait();
        });

        fs.SetLength(0);
        doc.Save(fs);
        Console.WriteLine("UpdateXmlDocumentation.csx - DONE");
}
