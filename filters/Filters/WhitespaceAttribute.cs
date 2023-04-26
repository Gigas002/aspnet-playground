using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Text.RegularExpressions;

namespace MvcApp.Filters;

public class WhitespaceAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext _) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var response = context.HttpContext.Response;
        response.Body = new SpaceCleaner(response.Body);
    }

    // вспомогательный класс для удаления пробелов
    private class SpaceCleaner : Stream
    {
        private readonly Stream outputStream;
        public SpaceCleaner(Stream filterStream) => outputStream = filterStream;

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            var html = Encoding.UTF8.GetString(buffer, offset, count);
            //регулярное выражение для поиска пробелов между тегами
            var regex = new Regex(@"(?<=\s)\s+(?![^<>]*</pre>)");
            html = regex.Replace(html, string.Empty);
            buffer = Encoding.UTF8.GetBytes(html);
            await outputStream.WriteAsync(buffer, 0, buffer.Length, cancellationToken);
        }

        // нереализованные методы Stream
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
        public override bool CanRead { get { return false; } }
        public override bool CanSeek { get { return false; } }
        public override bool CanWrite { get { return true; } }
        public override long Length { get { throw new NotSupportedException(); } }
        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
        public override void Flush() => outputStream.Flush();
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
    }
}
