using IronPdf;
namespace Recipe.Helpers
{
    public class PdfGenerator
    {
        public byte[] Generate()
        {
            var renderer = new ChromePdfRenderer();
            var pdf = renderer.RenderHtmlAsPdf("<h1> Hello IronPdf </h1>");
            return pdf.Stream.ToArray();
        }
    }
}
