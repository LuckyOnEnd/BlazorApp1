using Microsoft.AspNetCore.Mvc.RazorPages;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System.Reflection.Metadata;
using System.Text;
using BlazorApp1;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace pdf_generate
{
    public enum Positions
    {
        Left = 0,
        Center = 1,
        Right = 2,
    }
    
    public class PdfSerivce
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PdfSerivce(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        private PdfGridCellStyle SetCellStyle(int size, bool bold = false,
            PdfTextAlignment positions = PdfTextAlignment.Left, int paddingLeft = 0, 
            int paddingRight = 0, int paddingTop = 0, int paddingBottom = 0)
        {
            var style = new PdfGridCellStyle()
            {
                Font = FontSize(size, bold),
                StringFormat = new PdfStringFormat(positions),
                CellPadding = new PdfPaddings()
                {
                    Left = paddingLeft,
                    Right = paddingRight,
                    Bottom = paddingBottom,
                    Top = paddingTop,
                }
            };

            return style;
        }
        private PdfFont FontSize(int size, bool bold = false)
        {
            var font = new PdfStandardFont(PdfFontFamily.Helvetica, size);
            if (bold)
                font = new PdfStandardFont(PdfFontFamily.Helvetica, size, PdfFontStyle.Bold);
            
            font.SetTextEncoding(Encoding.GetEncoding("Windows-1250"));
            return font;
        }

        public MemoryStream CreatePDF(Root root)
        {
            PdfDocument pdfDocument = new PdfDocument();
            PdfPage pdfPage = pdfDocument.Pages.Add();
            
            #region FOOTER
            RectangleF bounds = new RectangleF(0, 0, pdfDocument.Pages[0].GetClientSize().Width, 50);
            PdfPageTemplateElement footer = new PdfPageTemplateElement(bounds);
            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Rows.Add();
            pdfGrid.Columns.Add(9);
          
            pdfGrid.Columns[8].Width = 90;
            pdfGrid.Rows[0].Height = 14;

            PdfPageNumberField pageNumber = new PdfPageNumberField(FontSize(7));
            PdfPageCountField count = new PdfPageCountField(FontSize(7));
            PdfCompositeField compositeField = new PdfCompositeField(FontSize(9, true), "Strona {0} z {1}", pageNumber, count);
            compositeField.Bounds = footer.Bounds;
            compositeField.Draw(footer.Graphics, new PointF(445, 21));
            
            pdfGrid.Rows[0].Cells[0].Value = "Zlecenie";
            pdfGrid.Rows[0].Cells[0].Style = SetCellStyle(9, false, PdfTextAlignment.Center);
            
            pdfGrid.Rows[0].Cells[1].Value = $"{ root.Ordernumber }";
            pdfGrid.Rows[0].Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            pdfGrid.Rows[0].Cells[1].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[2].Value = "Nr kompl";
            pdfGrid.Rows[0].Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            pdfGrid.Rows[0].Cells[2].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[3].Value = $"{ root.Collectionnumber }";
            pdfGrid.Rows[0].Cells[3].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[4].Value = "Wystawił";
            pdfGrid.Rows[0].Cells[4].Style = SetCellStyle(9, true, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[5].Value = $"{ root.Createdby }";
            pdfGrid.Rows[0].Cells[5].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[6].Value = "Data";
            pdfGrid.Rows[0].Cells[6].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[7].Value = $"{ root.Creasationdate }";
            pdfGrid.Rows[0].Cells[7].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[8].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Draw(footer.Graphics, new PointF(0, 20));
            pdfDocument.Template.Bottom = footer;

            #endregion
            #region HEADER
            PdfGrid headerGrid = new PdfGrid();
            headerGrid.Rows.Add();
            headerGrid.Columns.Add(3);
            
            headerGrid.Rows[0].Height = 50;
           

            headerGrid.Columns[0].Width = 93f;
            headerGrid.Columns[1].Width = 275f;

            PdfGrid headerMainPartGrid = new();
            headerMainPartGrid.Rows.Add();
            headerMainPartGrid.Columns.Add(4);
            headerMainPartGrid.Rows[0].Height = 50;

            headerMainPartGrid.Columns[0].Width = 38;
            headerMainPartGrid.Columns[1].Width = 180;
            headerMainPartGrid.Columns[3].Width = 60;
            
            headerMainPartGrid.Rows[0].Cells[0].Value = "WKS";
            headerMainPartGrid.Rows[0].Cells[0].Style = SetCellStyle(11, false, PdfTextAlignment.Left, 2, 0, 12, 0);
            
            headerMainPartGrid.Rows[0].Cells[1].Value = "ZLECENIE PRODUKCYJNE WYRÓB WŁASNY";
            headerMainPartGrid.Rows[0].Cells[1].Style = SetCellStyle(11, true, PdfTextAlignment.Left, 2, 0, 12, 0);
            
            PdfGrid nrPickingGrid = new();
            nrPickingGrid.Columns.Add();
            nrPickingGrid.Rows.Add();
            nrPickingGrid.Rows.Add();
            nrPickingGrid.Rows.Add();

            nrPickingGrid.Rows[0].Height = 8;
            nrPickingGrid.Rows[1].Height = 30;
            nrPickingGrid.Rows[2].Height = 20;
            
            nrPickingGrid.Rows[1].Cells[0].Value = "NR. KOMPL";
            nrPickingGrid.Rows[1].Cells[0].Style = SetCellStyle(9, true, PdfTextAlignment.Center, -5, 0, 8, 0);
            
            headerMainPartGrid.Rows[0].Cells[3].Value = nrPickingGrid;
            
            headerGrid.Rows[0].Cells[0].Style.Borders.Right = new PdfPen(PdfBrushes.Transparent);
            headerGrid.Rows[0].Cells[1].Style.Borders.Left = new PdfPen(PdfBrushes.Transparent);
            headerMainPartGrid.Rows[0].Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            headerMainPartGrid.Rows[0].Cells[1].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            headerMainPartGrid.Rows[0].Cells[2].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            headerMainPartGrid.Rows[0].Cells[3].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            nrPickingGrid.Rows[0].Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            nrPickingGrid.Rows[1].Cells[0].Style.Borders.Right = new PdfPen(PdfBrushes.Transparent);
            nrPickingGrid.Rows[2].Cells[0].Style.Borders.All = new PdfPen(PdfBrushes.Transparent);
            
            PdfGraphics logoImage = pdfPage.Graphics;
            FileStream image = new FileStream(_webHostEnvironment.WebRootPath + "//logo-with-bg.png", FileMode.Open, FileAccess.Read);
            PdfBitmap bitmapImage = new PdfBitmap(image);
            SizeF imageSize = new SizeF(87, 22);
            PointF imagelocaion = new PointF(5, 15); 
            logoImage.DrawImage(bitmapImage, imagelocaion, imageSize);

            headerGrid.Rows[0].Cells[1].Value = headerMainPartGrid;
            headerGrid.Rows[0].Cells[2].Value = root.Collectionnumber;
            headerGrid.Rows[0].Cells[2].Style = SetCellStyle(25, true,PdfTextAlignment.Center, 0, 0, 10, 0);
            #endregion

            #region SECOND PART
            
            PdfGrid secondPartGrid = new PdfGrid();
            secondPartGrid.Columns.Add(1);
            secondPartGrid.Rows.Add();
            secondPartGrid.Columns[0].Width = 265;
            
            PdfGrid orderNumberGrid = new PdfGrid();
            orderNumberGrid.Columns.Add(2);

            PdfGridRow orderNumberRow = orderNumberGrid.Rows.Add();
            PdfGridCellStyle pdfGridCellStyle = new()
            {
                Font = FontSize(10, true),
                Borders = new PdfBorders()
                {
                    All = new PdfPen(Color.Transparent),
                }
            };

            orderNumberRow.Height = 32;
            orderNumberRow.Cells[0].Value = "Numer zlecenia";
            orderNumberRow.Cells[0].Style = SetCellStyle(11, true, PdfTextAlignment.Center);

            orderNumberRow.Cells[1].Value = root.Ordernumber;
            orderNumberRow.Cells[1].Style = SetCellStyle(10, true, PdfTextAlignment.Center, 0, 0, 10, 0);

            PdfGridRow projectRow = orderNumberGrid.Rows.Add();
            projectRow.Height = 15;
            projectRow.Cells[0].Value = "Projekt";
            projectRow.Cells[0].Style = SetCellStyle(12, false, PdfTextAlignment.Center);

            projectRow.Cells[1].Value = root.Projectwithclient;
            projectRow.Cells[1].Style = SetCellStyle(10, false, PdfTextAlignment.Center);

            secondPartGrid.Rows[0].Cells[0].Value = orderNumberGrid;
            secondPartGrid.Rows[0].Cells[0].Style.Font = FontSize(10, true);
            secondPartGrid.Rows[0].Cells[0].Style.Borders.All = new PdfPen(Color.Transparent);
            orderNumberGrid.Columns[0].Width = 55;
            #endregion

            #region BARCODE
            PdfCode39Barcode barcode = new PdfCode39Barcode();
            barcode.BarHeight = 23;
            barcode.TextColor = Color.White;
            barcode.Text = "29668/ZD/2023/1/S1$";
            barcode.Draw(pdfPage, new PointF(270, 60));
            #endregion

            #region THIRD PART
            PdfGrid thirdGrid = new PdfGrid();
            thirdGrid.Columns.Add(4);
            thirdGrid.Columns[0].Width = 175;
            thirdGrid.Columns[1].Width = 32;
            thirdGrid.Columns[2].Width = 195;
            thirdGrid.Columns[3].Width = 38;
            PdfGridRow thirdGridFirstRow = thirdGrid.Rows.Add();

            thirdGridFirstRow.Height = 18;

            thirdGridFirstRow.Cells[0].Value = "Indeks wyrobu wlasnego";
            thirdGridFirstRow.Cells[0].Style = SetCellStyle(14, true, PdfTextAlignment.Center);

            thirdGridFirstRow.Cells[1].Value = "Rev";
            thirdGridFirstRow.Cells[1].Style = SetCellStyle(12, false, PdfTextAlignment.Center);

            thirdGridFirstRow.Cells[2].Value = "Nazwa wyrobu złożonego";
            thirdGridFirstRow.Cells[2].Style = SetCellStyle(12, false, PdfTextAlignment.Center);

            thirdGridFirstRow.Cells[3].Value = "Ilość";
            thirdGridFirstRow.Cells[3].Style = SetCellStyle(9, false);

            thirdGrid.Rows.Add();
            thirdGrid.Rows[1].Cells[0].Value = root.Goalindex;
            thirdGrid.Rows[1].Cells[1].Value = root.Goalrevision;
            thirdGrid.Rows[1].Cells[2].Value = root.Goalname;
            thirdGrid.Rows[1].Cells[3].Value = root.Goalquantity.ToString();
            #endregion

            #region QRCODE
            PdfQRBarcode qrCode = new PdfQRBarcode();
            qrCode.XDimension = 3;
            qrCode.Text = "29668/ZD/2023/1/S1";
            qrCode.Draw(pdfPage, new Point(447, 83), new SizeF(72, 72));
            #endregion

            #region FOURTH PART
            PdfGrid fourth = new PdfGrid();
            fourth.Columns.Add(3);
            PdfGridRow pdfGridRow = fourth.Rows.Add();
            pdfGridRow.Cells[0].Value = "Kolor";
            pdfGridRow.Cells[0].Style = SetCellStyle(11, false, PdfTextAlignment.Center);

            pdfGridRow.Cells[1].Value = "Material";
            pdfGridRow.Cells[1].Style = SetCellStyle(11, false, PdfTextAlignment.Center);

            pdfGridRow.Cells[2].Value = "Uwagi";
            pdfGridRow.Cells[2].Style = SetCellStyle(11, false, PdfTextAlignment.Center);

            fourth.Columns[1].Width = 200;
            fourth.Rows.Add();

            fourth.Rows[1].Cells[0].Value = root.Colour;
            fourth.Rows[1].Cells[1].Value = root.Drawingmaterial;
            fourth.Rows[1].Cells[2].Value = root.Goalcomments;
            fourth.Rows[1].Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            fourth.Rows[1].Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            fourth.Rows[1].Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            #endregion
            
            headerGrid.Draw(pdfPage, Syncfusion.Drawing.PointF.Empty);
            secondPartGrid.Draw(pdfPage, new Point(0, 60));
            thirdGrid.Draw(pdfPage, new Point(0, 120));
            fourth.Draw(pdfPage, new Point(0, 163));

            #region MATERIAL
            PdfGraphics graphics = pdfPage.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 17, PdfFontStyle.Bold);
            graphics.DrawString("Materials", font, PdfBrushes.Black, new PointF(0, 210));

            PdfGrid materialGrid = new PdfGrid();

            for (int i = 0; i < root.Materials.Count; i++)
            {
                root.Materials[i].Nr = i + 1;
            }

            IEnumerable<Material> dataTable = root.Materials;
            materialGrid.DataSource = dataTable;

            if(materialGrid.DataSource is not null)
            {
                for(int i = 0; i < materialGrid.Rows.Count; i++)
                {
                    materialGrid.Rows[i].Height = 17;
                }
            }

            materialGrid.Columns[0].Width = 35;
            materialGrid.Columns[1].Width = 120;
            materialGrid.Columns[2].Width = 200;
            materialGrid.Columns[3].Width = 45;
            materialGrid.Columns[4].Width = 25;
            materialGrid.Columns[5].Width = 90;

            PdfGridRow pdfGridHeader = materialGrid.Headers[0];
            pdfGridHeader.Cells[0].Value = "Nr";
            pdfGridHeader.Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold);
            pdfGridHeader.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            pdfGridHeader.Cells[1].Value = "Index materialu";
            pdfGridHeader.Cells[1].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 11);
            pdfGridHeader.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            pdfGridHeader.Cells[2].Value = "Nazwa materialu";
            pdfGridHeader.Cells[2].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 11);
            pdfGridHeader.Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            pdfGridHeader.Cells[3].Value = "Ilość";
            pdfGridHeader.Cells[3].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 11);
            pdfGridHeader.Cells[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            pdfGridHeader.Cells[4].Value = "JM";
            pdfGridHeader.Cells[4].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 11);
            pdfGridHeader.Cells[4].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            pdfGridHeader.Cells[5].Value = "Uwagi";
            pdfGridHeader.Cells[5].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 11);
            pdfGridHeader.Cells[5].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            var e = materialGrid.Draw(pdfPage, new Point(0, 235));
            pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count - 1];

            #endregion

            #region Operation
            PdfGraphics operationGraphics = pdfPage.Graphics;
            PdfFont operationFort = new PdfStandardFont(PdfFontFamily.Helvetica, 17, PdfFontStyle.Bold);
            operationGraphics.DrawString("Operations", operationFort, PdfBrushes.Black, new PointF(0, e.Bounds.Bottom + 16));

            PdfGrid operationGrid = new PdfGrid();
            List<Operation> Operations = new();

            for (int i = 0; i < root.Operations.Count; i++)
            {
                root.Operations[i].Nr = i + 1;
            }

            IEnumerable<Operation> opearationsTable = root.Operations;
            operationGrid.DataSource = opearationsTable;

            if (operationGrid.DataSource is not null)
            {
                for (int i = 0; i < operationGrid.Rows.Count; i++)
                {
                    operationGrid.Rows[i].Height = 17;
                }
            }

            operationGrid.Columns[0].Width = 22;
            operationGrid.Columns[1].Width = 62;
            operationGrid.Columns[2].Width = 62;
            operationGrid.Columns[3].Width = 58;
            operationGrid.Columns[4].Width = 62;
            operationGrid.Columns[5].Width = 52;
            operationGrid.Columns[6].Width = 52;
            operationGrid.Columns[7].Width = 22;
            operationGrid.Columns[8].Width = 58;
            operationGrid.Columns[9].Width = 65;

            PdfGridRow operationGridHeader = operationGrid.Headers[0];

            operationGridHeader.Cells[0].Value = "Nr";
            operationGridHeader.Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            operationGridHeader.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[1].Value = "Operacja";
            operationGridHeader.Cells[1].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            operationGridHeader.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[2].Value = "Stanowisko";
            operationGridHeader.Cells[2].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            operationGridHeader.Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[3].Value = "Czas pl.";
            operationGridHeader.Cells[3].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            operationGridHeader.Cells[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[4].Value = "Term zak.";
            operationGridHeader.Cells[4].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            operationGridHeader.Cells[4].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[5].Value = "Czas rz.";
            operationGridHeader.Cells[5].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            operationGridHeader.Cells[5].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[6].Value = "Inicjały";
            operationGridHeader.Cells[6].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            operationGridHeader.Cells[6].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[7].Value = "QC";
            operationGridHeader.Cells[7].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            operationGridHeader.Cells[7].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[8].Value = "Sprawdził";
            operationGridHeader.Cells[8].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            operationGridHeader.Cells[8].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            operationGridHeader.Cells[9].Value = "Uwagi";
            operationGridHeader.Cells[9].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            operationGridHeader.Cells[9].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            int getOperationSize = Operations.Count * 17;
            var t = operationGrid.Draw(pdfPage, new PointF(0, e.Bounds.Bottom + 38));
            pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count - 1];
            #endregion


            #region LINE
            for (int i = 0; i < 60; i++)
            {
                if(i % 2 != 0)
                {
                    PdfPen pen = new PdfPen(PdfBrushes.Black, 5f);
                    PointF point1 = new PointF(i * 10, t.Bounds.Bottom + 40);
                    PointF point2 = new PointF(i * 10 + 10, t.Bounds.Bottom + 40);
                    pdfPage.Graphics.DrawLine(pen, point1, point2);
                }
            }
            pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count - 1];
            #endregion

            #region semi-finished title
            PdfGraphics semiOperationsGraphics = pdfPage.Graphics;
            PdfFont semiOperationFort = new PdfStandardFont(PdfFontFamily.Helvetica, 17, PdfFontStyle.Bold);
            semiOperationsGraphics.DrawString("Półwyroby: ", semiOperationFort, PdfBrushes.Black, new PointF(0, t.Bounds.Bottom + 60)); //getMaterialSize + getOperationSize +  getProductSize  + 370
            #endregion

            #region semi_finised products

            for (int i = 0; i < root.Semiproducts.Count; i++)
            {
                PdfGraphics semiOperationsProductGraphics = pdfPage.Graphics;
                PdfFont semiOperationProductFort = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);
                
                semiOperationsProductGraphics.DrawString(root.Semiproducts[i].Title, operationFort, PdfBrushes.Black, new PointF(25, (i == 0 ? t.Bounds.Bottom + 100 : t.Bounds.Bottom + 10)));
                PdfPen pen = new PdfPen(PdfBrushes.Black, 1f);
                PointF point1 = new PointF(0 , (i == 0 ? t.Bounds.Bottom + 111 : t.Bounds.Bottom + 21));
                PointF point2 = new PointF(20, (i == 0 ? t.Bounds.Bottom + 111 : t.Bounds.Bottom + 21));
                pdfPage.Graphics.DrawLine(pen, point1, point2);
                
                PdfGrid productsGrid = new PdfGrid();

                for (int u = 0; u < root.Semiproducts[i].Lines.Count; u++)
                    root.Semiproducts[i].Lines[u].Id = u + 1;

                IEnumerable<Line> productTable = root.Semiproducts[i].Lines;
                productsGrid.DataSource = productTable;

                if (productsGrid.DataSource is not null)
                {
                    for (int u = 0; u < productsGrid.Rows.Count-1; u++)
                    {
                        productsGrid.Rows[u].Height = 17;
                    }
                }
                if (productsGrid.Headers.Count > 0)
                {

                    PdfGridRow productGridHeader = productsGrid.Headers[0];
                    productGridHeader.Cells[0].Value = "Nr";
                    productGridHeader.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                    productGridHeader.Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);

                    productGridHeader.Cells[1].Value = "Nr zlecenia";
                    productGridHeader.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                    productGridHeader.Cells[1].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);

                    productGridHeader.Cells[2].Value = "Numer rys";
                    productGridHeader.Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                    productGridHeader.Cells[2].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);

                    productGridHeader.Cells[3].Value = "Rev";
                    productGridHeader.Cells[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                    productGridHeader.Cells[3].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                    productGridHeader.Cells[4].Value = "Nazwa";
                    productGridHeader.Cells[4].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                    productGridHeader.Cells[4].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);

                    productGridHeader.Cells[5].Value = "Mat.";
                    productGridHeader.Cells[5].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                    productGridHeader.Cells[5].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);

                    productGridHeader.Cells[6].Value = "Ilość";
                    productGridHeader.Cells[6].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                    productGridHeader.Cells[6].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

                    productGridHeader.Cells[7].Value = "Ścieżka";
                    productGridHeader.Cells[7].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                    productGridHeader.Cells[7].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
                }

                if (i == 0)
                    t = productsGrid.Draw(pdfPage, new PointF(0, (t.Bounds.Bottom + 125)));
                else
                    t = productsGrid.Draw(pdfPage, new PointF(0, (t.Bounds.Bottom + 35)));
                pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count - 1];
            }
            pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count - 1];
            #endregion

            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream);
            pdfDocument.Close(true);

            return stream;
        }
    }
}
