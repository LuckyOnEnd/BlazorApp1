using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf;
using Syncfusion.Drawing;
using System.Drawing.Text;
using System.Text;
using Syncfusion.Blazor;
using Syncfusion.Pdf.Grid;

namespace BlazorApp1
{
    public class PdfServiceCard1
    {
        public MemoryStream CreatePdf(SRoot root)
        {
            PdfDocument document = new PdfDocument();
            document.PageSettings.Size = new SizeF(226, 155);
            document.PageSettings.Margins.Top = 10;
            document.PageSettings.Margins.Left = 20;
            document.PageSettings.Margins.Right = 0;
            document.PageSettings.Margins.Bottom = 0;

            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            PdfFont mainFont = new PdfStandardFont(PdfFontFamily.Courier, 40, PdfFontStyle.Bold);
            graphics.DrawString("M", mainFont, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Courier, 7);
            font.SetTextEncoding(Encoding.GetEncoding("Windows-1250"));
            
            graphics.DrawString("Nr kompl: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(25, 7));
            graphics.DrawString(root.collectionnumber, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(63, 7));
            
            graphics.DrawString("Projekt: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(25, 21));
            graphics.DrawString(root.projectwithclient, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(59, 21));
            
            graphics.DrawString("Ilość: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(130, 7));
            graphics.DrawString(root.quantity.ToString(), font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(155, 7));
            
            graphics.DrawString("Nr zlecenia: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 45));
            graphics.DrawString(root.ordernumber, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(52, 45));
            
            graphics.DrawString("Nazwa: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 58));
            graphics.DrawString(root.productname, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(27, 58));
            
            graphics.DrawString("Nr. rys: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 70));
            graphics.DrawString(root.productname, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(35, 70));
            
            graphics.DrawString("Nazwa zł: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 85));
            graphics.DrawString(root.productname, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(45, 85));
            
            graphics.DrawString("Nr.zł: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 100));
            graphics.DrawString(root.ordernumber, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(30, 100));
            
            graphics.DrawString("Cel: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 115));
            graphics.DrawString(root.colour, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(19, 115));
            
            graphics.DrawString("Kolor: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(120, 135));
            graphics.DrawString(root.colour, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(150, 135));

            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            document.Close(true);

            return stream;
        }
    }
    
    public class PdfServiceCard2
    {
        public MemoryStream CreatePdf(NRoot root)
        {
            PdfDocument document = new PdfDocument();
            document.PageSettings.Size = new SizeF(226, 155);
            document.PageSettings.Margins.Top = 10;
            document.PageSettings.Margins.Left = 20;
            document.PageSettings.Margins.Right = 0;
            document.PageSettings.Margins.Bottom = 0;

            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.Courier, 7);
            font.SetTextEncoding(Encoding.GetEncoding("Windows-1250"));
            
            graphics.DrawString("Nr kompl: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 5));
            graphics.DrawString(root.collectionnumber, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(40, 5));
            
            graphics.DrawString("Ilość: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(140, 5));
            graphics.DrawString(root.quantity.ToString(), font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(175, 5));
            
            graphics.DrawString("Projekt: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 20));
            graphics.DrawString(root.projectwithclient, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(37, 20));
            
            graphics.DrawString("Nr zlecenia: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 35));
            graphics.DrawString(root.quantity.ToString(), font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(50, 35));
            
            graphics.DrawString("Nazwa: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 50));
            graphics.DrawString(root.productname, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(27, 50));
            
            graphics.DrawString("Nr.rys: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 65));
            graphics.DrawString(root.productindexwithrevision, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(30, 65));
            
            graphics.DrawString("Nazwa zl.: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 80));
            graphics.DrawString(root.goalname, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(42, 80));
            
            graphics.DrawString("Nr.zl: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 95));
            graphics.DrawString(root.goalindexwithrevision, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(28, 95));
            
            graphics.DrawString("Cel: ", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 105));
            graphics.DrawString(root.goalindexwithrevision, font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(23, 105));

            PdfGrid grid = new();
            grid.Columns.Add();
            grid.Rows.Add();
            PdfStandardFont gridFont = new PdfStandardFont(PdfFontFamily.Courier, 7);
            grid.Rows[0].Cells[0].Style.Font = gridFont;
            grid.Rows[0].Cells[0].Style.CellPadding = new PdfPaddings(0, 10, 0, 0);
            grid.Rows[0].Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            grid.Rows[0].Cells[0].Value = root.productindexwithrevision;
            grid.Rows[0].Cells[0].Style.StringFormat = new PdfStringFormat(PdfTextAlignment.Right);

            grid.Draw(page, new PointF(85, 115));
            
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            document.Close(true);

            return stream;
        }
    }
}
