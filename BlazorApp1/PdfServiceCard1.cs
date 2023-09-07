using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using Syncfusion.Drawing;
using System.Drawing.Text;
using System.Text;
using Syncfusion.Blazor;

namespace BlazorApp1
{
    public class PdfServiceCard1
    {
        public MemoryStream CreatePdf()
        {
            PdfDocument document = new PdfDocument();
            document.PageSettings.Size = new SizeF(226, 155);
            document.PageSettings.Margins.Top = 10;
            document.PageSettings.Margins.Left = 20;
            document.PageSettings.Margins.Right = 0;

            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            PdfFont mainFont = new PdfStandardFont(PdfFontFamily.Courier, 40, PdfFontStyle.Bold);
            graphics.DrawString("S", mainFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Courier, 9);
            font.SetTextEncoding(Encoding.GetEncoding("Windows-1250"));
            graphics.DrawString("Nr kompl: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(25, 5));
            graphics.DrawString("Projekt: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(25, 19));
            graphics.DrawString("Ilość: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(130, 5));
            graphics.DrawString("Nr zlecenia: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 45));
            graphics.DrawString("Nazwa: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 60));
            graphics.DrawString("Nr rysunku: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 75));
            graphics.DrawString("Kolor: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 90));


            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            document.Close(true);

            return stream;
        }
    }
}
