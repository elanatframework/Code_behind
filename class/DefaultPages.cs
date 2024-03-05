namespace SetCodeBehind
{
    public class DefaultPages
    {
        public void Set()
        {
            Directory.CreateDirectory("wwwroot");

            string FilePath = "wwwroot/layout.aspx";
            var file1 = File.CreateText(FilePath);

            file1.WriteLine("@page");
            file1.WriteLine("@islayout");
            file1.WriteLine("@{");
            file1.WriteLine("  string WelcomeText = \"Welcome to the CodeBehind Framework!\";");
            file1.WriteLine("}");
            file1.WriteLine("<!DOCTYPE html>");
            file1.WriteLine("<html>");
            file1.WriteLine("<head>");
            file1.WriteLine("  <title>CodeBehind Framework - @ViewData.GetValue(\"title\")</title>");
            file1.WriteLine("  <meta charset=\"utf-8\" />");
            file1.WriteLine("  <style>");
            file1.WriteLine("  body");
            file1.WriteLine("  {");
            file1.WriteLine("      font-family: Arial, sans-serif;");
            file1.WriteLine("      margin: 0;");
            file1.WriteLine("      padding: 0;");
            file1.WriteLine("      line-height: 32px;");
            file1.WriteLine("  }");
            file1.WriteLine();
            file1.WriteLine("  header");
            file1.WriteLine("  {");
            file1.WriteLine("      background-color: #f2f2f2;");
            file1.WriteLine("      text-align: center;");
            file1.WriteLine("      padding: 20px 0;");
            file1.WriteLine("  }");
            file1.WriteLine();
            file1.WriteLine("  nav");
            file1.WriteLine("  {");
            file1.WriteLine("      background-color: #90dbff;");
            file1.WriteLine("      color: #fff;");
            file1.WriteLine("      text-align: center;");
            file1.WriteLine("      padding: 10px 0;");
            file1.WriteLine("  }");
            file1.WriteLine();
            file1.WriteLine("  nav ul");
            file1.WriteLine("  {");
            file1.WriteLine("      list-style-type: none;");
            file1.WriteLine("      padding: 0;");
            file1.WriteLine("  }");
            file1.WriteLine();
            file1.WriteLine("  nav ul li");
            file1.WriteLine("  {");
            file1.WriteLine("      display: inline;");
            file1.WriteLine("      margin: 0 10px;");
            file1.WriteLine("  }");
            file1.WriteLine();
            file1.WriteLine("  footer");
            file1.WriteLine("  {");
            file1.WriteLine("      background-color: #333;");
            file1.WriteLine("      color: #fff;");
            file1.WriteLine("      text-align: center;");
            file1.WriteLine("      padding: 10px 0;");
            file1.WriteLine("  }");
            file1.WriteLine("  </style>");
            file1.WriteLine("</head>");
            file1.WriteLine("<body>");
            file1.WriteLine();
            file1.WriteLine("  @LoadPage(\"/header.aspx\")");
            file1.WriteLine();
            file1.WriteLine("  <nav>");
            file1.WriteLine("      <ul>");
            file1.WriteLine("          <li><a href=\"/\">Home</a></li>");
            file1.WriteLine("          <li><a href=\"#\">About</a></li>");
            file1.WriteLine("          <li><a href=\"#\">Contact</a></li>");
            file1.WriteLine("      </ul>");
            file1.WriteLine("  </nav>");
            file1.WriteLine();
            file1.WriteLine("  <h2>CodeBehind Framework - @ViewData.GetValue(\"title\")</h2>");
            file1.WriteLine("  <p>Text value is: @WelcomeText</p>");
            file1.WriteLine();
            file1.WriteLine("  @PageReturnValue");
            file1.WriteLine();
            file1.WriteLine("  @LoadPage(\"/footer.aspx\")");
            file1.WriteLine();
            file1.WriteLine("</body>");
            file1.WriteLine("</html>");

            file1.Dispose();
            file1.Close();


            FilePath = "wwwroot/Default.aspx";
            var file2 = File.CreateText(FilePath);

            file2.WriteLine("@page");
            file2.WriteLine("@layout \"/layout.aspx\"");
            file2.WriteLine("@{");
            file2.WriteLine("  ViewData.Add(\"title\",\"Main page\");");
            file2.WriteLine("}");
            file2.WriteLine("  <main>");
            file2.WriteLine("      <p>CodeBehind library is a modern back-end framework and is an alternative to ASP.NET Core. This library is a programming model based on the MVC structure, which provides the possibility of creating dynamic aspx files in .NET Core and has high serverside independence. CodeBehind framework supports standard syntax and Razor syntax. This framework guarantees the separation of server-side codes from the design part (html) and there is no need to write server-side codes in the view.</p>");
            file2.WriteLine("      <p>Code Behind framework inherits every advantage of ASP.NET Core and gives it more simplicity, power and flexibility.</p>");
            file2.WriteLine("      <p><b>CodeBehind framework is an alternative to ASP.NET Core.</b></p>");
            file2.WriteLine("      <h3>Why use CodeBehind?</h3>");
            file2.WriteLine("      <ul>");
            file2.WriteLine("          <li><b>Fast:</b> The CodeBehind framework is faster than the default structure of cshtml pages in ASP.NET Core.</li>");
            file2.WriteLine("          <li><b>Simple:</b> Developing with CodeBehind is very simple. You can use mvc pattern or model-view or controller-view or only view.</li>");
            file2.WriteLine("          <li><b>Modular:</b> It is modular. Just copy the new project files, including dll and aspx, into the current active project.</li>");
            file2.WriteLine("          <li><b>Get output:</b> You can call the output of the aspx page in another aspx page and modify its output.</li>");
            file2.WriteLine("          <li><b>Under .NET Core:</b> Your project will still be under ASP.NET Core and you will benefit from all the benefits of .NET Core.</li>");
            file2.WriteLine("          <li><b>Code-Behind:</b> Code-Behind pattern will be fully respected.</li>");
            file2.WriteLine("          <li><b>Modern:</b> CodeBehind is a modern framework with revolutionary ideas.</li>");
            file2.WriteLine("          <li><b>Understandable:</b> View is preferable to controller and there is no need to set controllers in route.</li>");
            file2.WriteLine("      </ul>");
            file2.WriteLine("      <p><b>CodeBehind is .NET Diamond!</b></p>");
            file2.WriteLine("      <p>In every scenario, CodeBehind performs better than the default structure in ASP.NET Core.</p>");
            file2.WriteLine("  </main>");

            file2.Dispose();
            file2.Close();


            FilePath = "wwwroot/header.aspx";
            var file3 = File.CreateText(FilePath);

            file3.WriteLine("@page");
            file3.WriteLine("@break");
            file3.WriteLine("  <header>");
            file3.WriteLine("      <h1>Company name</h1>");
            file3.WriteLine("  </header>");

            file3.Dispose();
            file3.Close();


            FilePath = "wwwroot/footer.aspx";
            var file4 = File.CreateText(FilePath);

            file4.WriteLine("@page");
            file4.WriteLine("@break");
            file4.WriteLine("  <footer>");
            file4.WriteLine("      <p>&copy; @DateTime.Now.ToString(\"yyyy\") Company name - Built with <a href=\"https://elanat.net/page_content/code_behind\" title=\"CodeBehind Framework\">CodeBehind Framework</a></p>");
            file4.WriteLine("  </footer>");

            file4.Dispose();
            file4.Close();


            FilePath = "wwwroot/error.aspx";
            var file5 = File.CreateText(FilePath);

            file5.WriteLine("@page");
            file5.WriteLine("@layout \"/layout.aspx\"");
            file5.WriteLine("@section");
            file5.WriteLine("@{");
            file5.WriteLine("  ViewData.Add(\"title\",\"Error page\");");
            file5.WriteLine();
            file5.WriteLine("  int ErrorValue = 0;");
            file5.WriteLine("  if (Section.GetValue(0).IsNumber())");
            file5.WriteLine("    ErrorValue = Section.GetValue(0).ToNumber();");
            file5.WriteLine("}");
            file5.WriteLine("  <main>");
            file5.WriteLine("      <div>");
            file5.WriteLine("      @if (ErrorValue == 400)");
            file5.WriteLine("      {");
            file5.WriteLine("        <h1>Error 400 Bad request</h1>");
            file5.WriteLine("        <h3>The path you requested is incorrect or the server cannot respond to this request.</h3>");
            file5.WriteLine("      }");
            file5.WriteLine("      else if (ErrorValue == 401)");
            file5.WriteLine("      {");
            file5.WriteLine("        <h1>Error 401 Authorization required</h1>");
            file5.WriteLine("        <h3>The path you requested requires validation. Either you don't have access to the path or you need to log in.</h3>");
            file5.WriteLine("      }");
            file5.WriteLine("      else if (ErrorValue == 403)");
            file5.WriteLine("      {");
            file5.WriteLine("        <h1>Error 403 Forbidden</h1>");
            file5.WriteLine("        <h3>The path you requested cannot be accessed.</h3>");
            file5.WriteLine("      }");
            file5.WriteLine("      else if (ErrorValue == 404)");
            file5.WriteLine("      {");
            file5.WriteLine("        <h1>Error 404 Page not found</h1>");
            file5.WriteLine("        <h3>No page was found in the path you requested.</h3>");
            file5.WriteLine("      }");
            file5.WriteLine("      else if (ErrorValue == 500)");
            file5.WriteLine("      {");
            file5.WriteLine("        <h1>Error 500 Internal server error</h1>");
            file5.WriteLine("        <h3>The server encountered an unexpected problem, so the problem prevented us from responding to your request.</h3>");
            file5.WriteLine("      }");
            file5.WriteLine("      else");
            file5.WriteLine("      {");
            file5.WriteLine("        <h1>Error! Status Code: @ErrorValue</h1>");
            file5.WriteLine("        <h3>A problem has occurred.</h3>");
            file5.WriteLine("      }");
            file5.WriteLine("      </div>");
            file5.WriteLine("  </main>");

            file5.Dispose();
            file5.Close();
        }
    }
}
