using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MvcApp.TagHelpers;

public class StyleInfo
{
    public string? Color { get; set; }
    public int? FontSize { get; set; }
    public string? FontFamily { get; set; }
}

public class TimerTagHelper : TagHelper
{
    public bool SecondsIncluded { get; set; }
    public string? Color { get; set; }
    public StyleInfo? Style { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var now = DateTime.Now;
        var time = String.Empty;
        if (SecondsIncluded)    // если true добавляем секунды
            time = now.ToString("HH:mm:ss");
        else
            time = now.ToString("HH:mm");

        // заменяет тег <timer> тегом <div>
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        // элемент перед тегом
        output.PreElement.SetHtmlContent("<h4>Дата и время</h4>");
        // элемент после тега
        output.PostElement.SetHtmlContent($"<div>Дата: {DateTime.Now.ToString("dd/MM/yyyy")}</div>");

        // // устанавливаем цвет, если свойство Color не равно null
        // if (Color != null) output.Attributes.SetAttribute("style", $"color:{Color};");

        // формируем стиль
        string style = "";
        if (Style?.Color != null) style = $"color:{Style.Color};";
        if (Style?.FontSize != null) style = $"{style}font-size:{Style.FontSize}px;";
        if (Style?.FontFamily != null) style = $"{style}font-family:{Style.FontFamily};";

        output.Attributes.SetAttribute("style", style);

        // устанавливаем содержимое элемента
        output.Content.SetContent(time);
    }
}

// public class TimerTagHelper : TagHelper
// {
//     ITimeService timeService;
//     public TimerTagHelper(ITimeService timeServ)
//     {
//         timeService = timeServ;
//     }
//     public override void Process(TagHelperContext context, TagHelperOutput output)
//     {
//         output.TagName = "div";
//         output.TagMode = TagMode.StartTagAndEndTag;
//         output.Content.SetContent($"Текущее время: {timeService.GetTime()}");
//     }
// }

[HtmlTargetElement(Attributes = "header")]
public class HeaderTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "h2";
        output.Attributes.RemoveAll("header");
    }
}

public class DateTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.SetContent($"Текущая дата: {DateTime.Now.ToString("dd/mm/yyyy")}");
    }
}

public class SummaryTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        // получаем вложенный контекст из дочерних tag-хелперов
        var target = await output.GetChildContentAsync();
        var content = "<h3>Общая информация</h3>" + target.GetContent();
        output.Content.SetHtmlContent(content);
    }
}

public class ListTagHelper : TagHelper
{
    public List<string> Elements { get; set; } = new();
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "ul";
        string listContent = "";
        foreach (string item in Elements)
            listContent = $"{listContent}<li>{item}</li>";

        output.Content.SetHtmlContent(listContent);
    }
}
