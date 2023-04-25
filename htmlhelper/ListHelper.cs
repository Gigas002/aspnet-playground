using Microsoft.AspNetCore.Html;        // для HtmlString
using Microsoft.AspNetCore.Mvc.Rendering;   // для IHtmlHelper
using System.Text.Encodings.Web;    // для HtmlEncoder

namespace MvcApp;

public static class ListHelper
{
    public static HtmlString CreateList(this IHtmlHelper html, string[] items)
    {
        TagBuilder ul = new TagBuilder("ul");
        foreach (string item in items)
        {
            TagBuilder li = new TagBuilder("li");
            // добавляем текст в li
            li.InnerHtml.Append(item);
            // добавляем li в ul
            ul.InnerHtml.AppendHtml(li);
        }
        ul.Attributes.Add("class", "itemsList");
        using var writer = new StringWriter();
        ul.WriteTo(writer, HtmlEncoder.Default);
        return new HtmlString(writer.ToString());
    }
}
