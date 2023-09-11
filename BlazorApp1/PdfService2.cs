using System.Text;
using pdf_generate;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace BlazorApp1
{
    public class PdfService2
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PdfService2(IWebHostEnvironment webHostEnvironment)
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
        
        public MemoryStream CreatePDF(RootProduction root)
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
            
            pdfGrid.Rows[0].Cells[1].Value = $"{ root.ordernumber }";
            pdfGrid.Rows[0].Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            pdfGrid.Rows[0].Cells[1].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[2].Value = "Nr kompl";
            pdfGrid.Rows[0].Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            pdfGrid.Rows[0].Cells[2].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[3].Value = $"{ root.collectionnumber }";
            pdfGrid.Rows[0].Cells[3].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[4].Value = "Wystawił";
            pdfGrid.Rows[0].Cells[4].Style = SetCellStyle(9, true, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[5].Value = $"{ root.createdby }";
            pdfGrid.Rows[0].Cells[5].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[6].Value = "Data";
            pdfGrid.Rows[0].Cells[6].Style = SetCellStyle(9, false, PdfTextAlignment.Center);

            pdfGrid.Rows[0].Cells[7].Value = $"{ root.creasationdate }";
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
            headerGrid.Rows[0].Cells[2].Value = root.collectionnumber;
            headerGrid.Rows[0].Cells[2].Style = SetCellStyle(25, true,PdfTextAlignment.Center, 0, 0, 10, 0);
            #endregion

            #region SECOND PART
            PdfGrid second = new PdfGrid();
            second.Columns.Add(1);
            second.Rows.Add();
            second.Columns[0].Width = 265;
            PdfGrid secondFirstPart = new PdfGrid();
            secondFirstPart.Columns.Add(2);

            PdfGridRow secondRowFirstPart = secondFirstPart.Rows.Add();

            PdfGridCellStyle pdfGridCellStyle = new PdfGridCellStyle();
            pdfGridCellStyle.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            pdfGridCellStyle.Borders.All = new PdfPen(Color.Transparent);

            secondRowFirstPart.Height = 32;
            secondRowFirstPart.Cells[0].Value = "Numer zlecenia";
            secondRowFirstPart.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            secondRowFirstPart.Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold);

            secondRowFirstPart.Cells[1].Value = root.ordernumber;
            secondRowFirstPart.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            secondRowFirstPart.Cells[1].Style.CellPadding = new PdfPaddings(0, 0, 12, 0);

            PdfGridRow secondRowFirstPart2 = secondFirstPart.Rows.Add();
            secondRowFirstPart2.Height = 15;
            secondRowFirstPart2.Cells[0].Value = "Projekt";
            secondRowFirstPart2.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            secondRowFirstPart2.Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            secondRowFirstPart2.Cells[1].Value = root.projectwithclient;
            secondRowFirstPart2.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            secondRowFirstPart2.Cells[1].Style.CellPadding = new PdfPaddings(0, 0, 2, 0);


            second.Rows[0].Cells[0].Value = secondFirstPart;
            second.Rows[0].Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            second.Rows[0].Cells[0].Style.Borders.All = new PdfPen(Color.Transparent);
            secondFirstPart.Columns[0].Width = 55;
            #endregion

            #region BARCODE
            PdfCode39Barcode barcode = new PdfCode39Barcode();
            barcode.BarHeight = 23;
            barcode.TextColor = Color.White;
            barcode.Text = $"{root.ordernumber}$";
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
            thirdGridFirstRow.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            thirdGridFirstRow.Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);

            thirdGridFirstRow.Cells[1].Value = "Rev";
            thirdGridFirstRow.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            thirdGridFirstRow.Cells[1].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

            thirdGridFirstRow.Cells[2].Value = "Nazwa wyrobu złożonego";
            thirdGridFirstRow.Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            thirdGridFirstRow.Cells[2].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 11, PdfFontStyle.Bold);

            thirdGridFirstRow.Cells[3].Value = "Ilość";
            thirdGridFirstRow.Cells[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            thirdGridFirstRow.Cells[3].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 9, PdfFontStyle.Bold);

            thirdGrid.Rows.Add();
            thirdGrid.Rows[1].Cells[0].Value = root.goalindex;
            thirdGrid.Rows[1].Cells[1].Value = root.goalrevision;
            thirdGrid.Rows[1].Cells[2].Value = root.goalname;
            thirdGrid.Rows[1].Cells[3].Value = root.goalquantity;
            #endregion

            #region QRCODE
            PdfQRBarcode qrCode = new PdfQRBarcode();
            qrCode.XDimension = 3;
            qrCode.Text = $"{root.ordernumber}";
            qrCode.Draw(pdfPage, new Point(447, 85), new SizeF(72, 72));
            #endregion

            #region FIRTH PART
            PdfGrid firth = new PdfGrid();
            firth.Columns.Add(4);
            firth.Columns[0].Width = 140;
            firth.Columns[1].Width = 60;
            firth.Columns[2].Width = 250;
            firth.Columns[3].Width = 65;

            PdfGridRow firthRow = firth.Rows.Add();
            firthRow.Cells[0].Value = "Indeks półwyrobu";
            firthRow.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            firthRow.Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 14);

            firthRow.Cells[1].Value = "Rev";
            firthRow.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            firthRow.Cells[1].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);

            firthRow.Cells[2].Value = "Nazwa półwyrobu";
            firthRow.Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            firthRow.Cells[2].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 13);

            firthRow.Cells[3].Value = "Iłość";
            firthRow.Cells[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            firthRow.Cells[3].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
            
            PdfGridRow secondRow = firth.Rows.Add();
            secondRow.Cells[0].Value = root.productlindex;
            secondRow.Cells[1].Value = root.productrevision;
            secondRow.Cells[2].Value = root.productname;
            secondRow.Cells[3].Value = root.productquantity;
            
            PdfGrid firthSec = new PdfGrid();
            firthSec.Columns.Add(3);
            firthSec.Columns[1].Width = 200;
            PdfGridRow firthColumnSecondPart = firthSec.Rows.Add();
            firthColumnSecondPart.Cells[0].Value = "Wymiar";
            firthColumnSecondPart.Cells[0].Style.Borders.Top = new PdfPen(Color.Transparent);
            firthColumnSecondPart.Cells[0].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            firthColumnSecondPart.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            firthColumnSecondPart.Cells[1].Value = "Material";
            firthColumnSecondPart.Cells[1].Style.Borders.Top = new PdfPen(Color.Transparent);
            firthColumnSecondPart.Cells[1].Style.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Bold);
            firthColumnSecondPart.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center);

            firthColumnSecondPart.Cells[2].Value = "Uwagi";
            firthColumnSecondPart.Cells[2].Style.Borders.Top = new PdfPen(Color.Transparent);
            PdfGridRow firthColumnSecondPartValue = firthSec.Rows.Add();
            firthColumnSecondPartValue.Height = 13;
            firthColumnSecondPartValue.Cells[0].Value = root.drawingdimensions;
            firthColumnSecondPartValue.Cells[1].Value = root.drawingmaterial;
            firthColumnSecondPartValue.Cells[2].Value = root.drawingpath;
            
            var t = firth.Draw(pdfPage, new Point(0, 180));
            var x = firthSec.Draw(pdfPage, new PointF(0, t.Bounds.Bottom));
            
            #endregion

            #region MATERIAL
            PdfGraphics graphics = pdfPage.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 17, PdfFontStyle.Bold);
            graphics.DrawString("Materials", font, PdfBrushes.Black, new PointF(0, 255));

            PdfGrid materialGrid = new PdfGrid();
            List<Material> Materials = new();

            Materials.Add(new Material() { Name = "asd" });

            IEnumerable<Material> dataTable = Materials;
            materialGrid.DataSource = dataTable;

            if (materialGrid.DataSource is not null)
            {
                for (int i = 0; i < materialGrid.Rows.Count; i++)
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

            int getMaterialSize = Materials.Count * 17;

            var e = materialGrid.Draw(pdfPage, new Point(0, 280));
            pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count - 1];

            #endregion

            #region Operation
            PdfGraphics operationGraphics = pdfPage.Graphics;
            PdfFont operationFort = new PdfStandardFont(PdfFontFamily.Helvetica, 17, PdfFontStyle.Bold);
            operationGraphics.DrawString("Operations", operationFort, PdfBrushes.Black, new PointF(0, e.Bounds.Bottom + 16));

            PdfGrid operationGrid = new PdfGrid();
            
            IEnumerable<Operation> opearationsTable = root.operations;
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
            operationGrid.Columns[9].Width = 62;

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

            int getOperationSize = root.operations.Count * 17;
            operationGrid.Draw(pdfPage, new PointF(0, e.Bounds.Bottom + 38));
            pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count - 1];
            #endregion

            #region SECOND_LINE

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

            PdfGridLayoutResult otherResult = null;
            for (int i = 0; i < root.others.Count; i++)
            {
                PdfGraphics otherGraphics = pdfPage.Graphics;
                PdfFont othersOperationFort = new PdfStandardFont(PdfFontFamily.Helvetica, 17, PdfFontStyle.Bold);
                otherGraphics.DrawString(root.others[0].Title, othersOperationFort, PdfBrushes.Black, new PointF(0, t.Bounds.Bottom + 55));
                for (int u = 0; u < root.others[i].Values.Count; u++)
                {
                    PdfGrid othersGrid = new();
                    othersGrid.Rows.Add();
                    othersGrid.Rows.Add();
                    othersGrid.Columns.Add(2);

                    othersGrid.Rows[0].Cells[0].Value = "Nazwa";
                    othersGrid.Rows[0].Cells[0].Style = SetCellStyle(11, true, PdfTextAlignment.Center);
            
                    othersGrid.Rows[0].Cells[1].Value = "Wartość";
                    othersGrid.Rows[0].Cells[1].Style = SetCellStyle(11, true, PdfTextAlignment.Center);
            
                    othersGrid.Rows[1].Cells[0].Value = root.others[i].Values[u].Name;
                    othersGrid.Rows[1].Cells[0].Style = SetCellStyle(11, false, PdfTextAlignment.Center);
            
                    othersGrid.Rows[1].Cells[1].Value = root.others[i].Values[u].StringValue;
                    othersGrid.Rows[1].Cells[1].Style = SetCellStyle(11, true, PdfTextAlignment.Center);
                    
                    if(u == 0)
                        otherResult = othersGrid.Draw(pdfPage, new PointF(0, (t.Bounds.Bottom + 80)));
                    else
                        otherResult = othersGrid.Draw(pdfPage, new PointF(0, (otherResult.Bounds.Bottom + 40)));
                    pdfPage = pdfDocument.Pages[pdfDocument.Pages.Count - 1];
                }
            }
            headerGrid.Draw(pdfPage, Syncfusion.Drawing.PointF.Empty);
            second.Draw(pdfPage, new Point(0, 60));
            thirdGrid.Draw(pdfPage, new Point(0, 120));

            MemoryStream stream = new MemoryStream();
            pdfDocument.Save(stream);
            pdfDocument.Close(true);

            return stream;
        }
    }
}
