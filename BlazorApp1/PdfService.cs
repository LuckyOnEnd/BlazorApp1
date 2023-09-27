using System.Net;
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
        private PdfPage Page;

        private PdfGridCellStyle CellStyle(int size, bool bold = false,
            PdfTextAlignment positions = PdfTextAlignment.Center, int paddingLeft = 0,
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

        private PdfBorders HideBorder(bool left = false, bool top = false, bool right = false, bool bottom = false)
        {
            return new()
            {
                Left = left ? new(Color.Black) : new(Color.Transparent),
                Top = top ? new(Color.Black) : new(Color.Transparent),
                Right = right ? new(Color.Black) : new(Color.Transparent),
                Bottom = bottom ? new(Color.Black) : new(Color.Transparent),
            };
        }

        private PdfFont FontSize(int size, bool bold = false)
        {
            var font = new PdfStandardFont(PdfFontFamily.TimesRoman, size);
            if (bold)
                font = new PdfStandardFont(PdfFontFamily.TimesRoman, size, PdfFontStyle.Bold);

            font.SetTextEncoding(Encoding.GetEncoding("Windows-1250"));
            return font;
        }

        private PdfGridLayoutResult GenerateInput(int width, PointF position, int height = 10)
        {
            PdfGrid grid = new();
            grid.Rows.Add();
            grid.Columns.Add(1);

            grid.Rows[0].Height = height;
            grid.Columns[0].Width = width;

            return grid.Draw(Page, position);
        }

        public async Task<MemoryStream> CreatePDF()
        {
            PdfDocument documents = new PdfDocument();
            Page = documents.Pages.Add();
            PdfGraphics Graphics = Page.Graphics;

            #region TITLE

            Graphics.DrawString("Protokół sprawdzenia technicznej sprawności", FontSize(13, true), PdfBrushes.Black,
                new PointF(130, 45));

            #endregion

            #region SmallTitle

            Graphics.DrawString("wykonany na zlecenie", FontSize(8, true), PdfBrushes.Black, new PointF(190, 80));

            #endregion

            #region FirstChapter

            var firstAndLastNameInput = GenerateInput(70, new PointF(20, 130), 14);
            Graphics.DrawString("(Nazwisko i imię użytkownika)", FontSize(6), PdfBrushes.Black,
                new PointF(firstAndLastNameInput.Bounds.Left - 5, firstAndLastNameInput.Bounds.Bottom + 2));

            var identificate = GenerateInput(75, new PointF(110, 130), 14);
            Graphics.DrawString("(Identyfikator)", FontSize(6), PdfBrushes.Black,
                new PointF(identificate.Bounds.Left + 17, identificate.Bounds.Bottom + 2));

            var address = GenerateInput(160, new PointF(225, 130), 14);
            Graphics.DrawString("(Ulica, nr domu, nr mieszkania)", FontSize(6), PdfBrushes.Black,
                new PointF(address.Bounds.Left + 38, address.Bounds.Bottom + 2));

            var QCDate = GenerateInput(70, new PointF(400, 130), 14);
            Graphics.DrawString("Data sprawdzania", FontSize(6), PdfBrushes.Black,
                new PointF(QCDate.Bounds.Left + 17, QCDate.Bounds.Bottom + 2));

            #endregion

            #region SecondChapter

            GenerateInput(63, new PointF(112, 169));
            Graphics.DrawString("Temperatura zewnętrzna", FontSize(8), PdfBrushes.Black, new PointF(16, 170));

            GenerateInput(70, new PointF(285, 169));
            Graphics.DrawString("Temperatura wewnętrzna", FontSize(8), PdfBrushes.Black, new PointF(190, 170));

            GenerateInput(40, new PointF(430, 169));
            Graphics.DrawString("Ilość nawiewników", FontSize(8), PdfBrushes.Black, new PointF(365, 170));

            #endregion

            #region GridChapter

            PdfGrid mainGrid = new();
            mainGrid.Columns.Add(4);

            {
                #region GridHeader

                mainGrid.Rows.Add();
                mainGrid.Rows[0].Height = 55;

                {
                    #region GridHeaderFirstChapter

                    PdfGrid grid = new PdfGrid();
                    grid.Columns.Add(3);
                    grid.Columns[0].Width = 48;
                    grid.Columns[1].Width = 47;
                    grid.Columns[2].Width = 33;
                    
                    grid.Rows.Add();
                    grid.Rows[0].Height = 55;
                    grid.Rows[0].Cells[0].Style.Borders = HideBorder(false, false, true);
                    grid.Rows[0].Cells[1].Style.Borders = HideBorder(false, false, true);
                    grid.Rows[0].Cells[2].Style.Borders = HideBorder();

                    {
                        #region FirstColumn

                        PdfGrid ads = new();

                        #endregion
                    }

                    {
                        #region SecondColumn

                        PdfGrid headerSecondColumnRowAdd = new();
                        headerSecondColumnRowAdd.Columns.Add(1);
                        headerSecondColumnRowAdd.Rows.Add();
                        headerSecondColumnRowAdd.Rows.Add();

                        headerSecondColumnRowAdd.Rows[0].Height = 44;
                        headerSecondColumnRowAdd.Rows[1].Height = 9;
                        headerSecondColumnRowAdd.Rows[0].Cells[0].Style.Borders = HideBorder();
                        headerSecondColumnRowAdd.Rows[1].Cells[0].Style = CellStyle(8);
                        headerSecondColumnRowAdd.Rows[1].Cells[0].Style.Borders = HideBorder(false, true, false, false);
                        headerSecondColumnRowAdd.Rows[1].Cells[0].Value = "m3/h";

                        grid.Rows[0].Cells[1].Value = headerSecondColumnRowAdd;

                        #endregion
                    }

                    {
                        #region ThirdColumn

                        PdfGrid headerSecondColumnRowAdd = new();
                        headerSecondColumnRowAdd.Columns.Add(1);
                        headerSecondColumnRowAdd.Rows.Add();
                        headerSecondColumnRowAdd.Rows.Add();
                        headerSecondColumnRowAdd.Rows.Add();

                        headerSecondColumnRowAdd.Rows[0].Height = 35;
                        headerSecondColumnRowAdd.Rows[1].Height = 9;
                        headerSecondColumnRowAdd.Rows[2].Height = 9;

                        headerSecondColumnRowAdd.Rows[0].Cells[0].Style.Borders = HideBorder();

                        headerSecondColumnRowAdd.Rows[1].Cells[0].Style = CellStyle(8);
                        headerSecondColumnRowAdd.Rows[1].Cells[0].Style.Borders = HideBorder(false, true);
                        headerSecondColumnRowAdd.Rows[1].Cells[0].Value = "ϕ cm";

                        headerSecondColumnRowAdd.Rows[2].Cells[0].Style = CellStyle(8);
                        headerSecondColumnRowAdd.Rows[2].Cells[0].Style.Borders = HideBorder(false, true);
                        headerSecondColumnRowAdd.Rows[2].Cells[0].Value = "cm";

                        grid.Rows[0].Cells[2].Value = headerSecondColumnRowAdd;

                        #endregion
                    }

                    #endregion

                    mainGrid.Rows[0].Cells[0].Value = grid;
                }
                {
                    #region GridHeaderSecondChapter

                    PdfGrid grid = new PdfGrid();
                    grid.Columns.Add(1);
                    grid.Rows.Add();
                    grid.Rows.Add();

                    grid.Rows[0].Cells[0].Style.Borders = HideBorder();
                    grid.Rows[1].Cells[0].Style.Borders = HideBorder(false, true);
                    grid.Rows[0].Height = 26;
                    grid.Rows[1].Height = 35;
                    
                    {
                        #region TopSideGrid

                        grid.Rows[0].Cells[0].Value = "Strumienie powietrza w kratkach\nkanałów wentylacyjnych";
                        grid.Rows[0].Cells[0].Style = CellStyle(9);
                        grid.Rows[0].Cells[0].Style.Borders = HideBorder();

                        #endregion
                    }
                    {
                        #region BottomSideGrid

                        PdfGrid bottomGrid = new();
                        bottomGrid.Rows.Add();
                        bottomGrid.Columns.Add(2);
                        bottomGrid.Rows[0].Height = 32;
                        bottomGrid.Rows[0].Cells[0].Style.Borders = HideBorder();
                        bottomGrid.Rows[0].Cells[1].Style.Borders = HideBorder();
                        {
                            #region BottomLeftSide

                            PdfGrid leftSideGrid = new();
                            leftSideGrid.Columns.Add(1);
                            leftSideGrid.Rows.Add();
                            leftSideGrid.Rows.Add();
                            
                            leftSideGrid.Rows[0].Cells[0].Style = CellStyle(9);
                            leftSideGrid.Rows[0].Cells[0].Value = "Okna zamknięte";
                            leftSideGrid.Rows[0].Cells[0].Style.Borders = HideBorder(false, false,true);
                            leftSideGrid.Rows[1].Cells[0].Style.Borders = HideBorder();
                            leftSideGrid.Rows[0].Height = 16;
                            leftSideGrid.Rows[1].Height = 16;

                            PdfGrid bottom = new();
                            bottom.Columns.Add(2);
                            bottom.Rows.Add();
                            bottom.Rows[0].Height = 15;
                            bottom.Rows[0].Cells[0].Value = "m/s";
                            bottom.Rows[0].Cells[1].Value = "m3/h";
                            bottom.Rows[0].Cells[0].Style = CellStyle(8);
                            bottom.Rows[0].Cells[1].Style = CellStyle(8);
                            bottom.Rows[0].Cells[0].Style.Borders = HideBorder(true, true, true);
                            bottom.Rows[0].Cells[1].Style.Borders = HideBorder(true, true);
                            
                            leftSideGrid.Rows[1].Cells[0].Value = bottom;
                            #endregion

                            #region BottomRightSide

                            PdfGrid rightSideGrid = new();
                            rightSideGrid.Columns.Add(1);
                            rightSideGrid.Rows.Add();
                            rightSideGrid.Rows.Add();
                            
                            rightSideGrid.Rows[0].Cells[0].Style = CellStyle(9);
                            rightSideGrid.Rows[0].Cells[0].Value = "Okna (mikrouchył\ndo 10 mm)";
                            rightSideGrid.Rows[0].Cells[0].Style.Borders = HideBorder(true, false,true);
                            rightSideGrid.Rows[1].Cells[0].Style.Borders = HideBorder(true, false, true);
                            rightSideGrid.Rows[0].Height = 16;
                            rightSideGrid.Rows[1].Height = 16;

                            PdfGrid bottomRight = new();
                            bottomRight.Columns.Add(2);
                            bottomRight.Rows.Add();
                            bottomRight.Rows[0].Height = 15;
                            bottomRight.Rows[0].Cells[0].Value = "m/s";
                            bottomRight.Rows[0].Cells[1].Value = "m3/h";
                            bottomRight.Rows[0].Cells[0].Style = CellStyle(8);
                            bottomRight.Rows[0].Cells[1].Style = CellStyle(8);
                            bottomRight.Rows[0].Cells[0].Style.Borders = HideBorder(false, true);
                            bottomRight.Rows[0].Cells[1].Style.Borders = HideBorder(true, true);

                            leftSideGrid.Rows[1].Cells[0].Value = bottomRight;

                            #endregion

                            bottomGrid.Rows[0].Cells[0].Value = leftSideGrid;
                            bottomGrid.Rows[0].Cells[1].Value = leftSideGrid;
                        }
                        grid.Rows[1].Cells[0].Value = bottomGrid;

                        #endregion
                    }
                    

                    #endregion
                    
                    mainGrid.Rows[0].Cells[1].Value = grid;
                }
                {
                    #region GridHeaderThirdChapter

                    PdfGrid grid = new();
                    grid.Rows.Add();
                    grid.Rows.Add();
                    grid.Columns.Add(2);

                    grid.Rows[0].Cells[0].Value = "Przepływ przy\nOKNACH\nZAMKNIĘTYCH";
                    grid.Rows[0].Cells[1].Value = "Przepływ przy\nmikrouchyle do 10\nmm";
                    PdfGrid bottomPart = new();
                    bottomPart.Rows.Add();
                    bottomPart.Columns.Add(2);
                    bottomPart.Rows[0].Cells[0].Value = "Nadmiar\nm3/h";
                    bottomPart.Rows[0].Cells[1].Value = "Niedobór\nm3/h";
                    bottomPart.Rows[0].Cells[0].Style = CellStyle(6, false, PdfTextAlignment.Center, 2, 2, 3);
                    bottomPart.Rows[0].Cells[1].Style = CellStyle(6, false, PdfTextAlignment.Center, 2, 2, 3);
                    
                    grid.Rows[1].Cells[0].Value = bottomPart;
                    grid.Rows[1].Cells[1].Value = bottomPart;
                    
                    grid.Rows[0].Cells[0].Style = CellStyle(7);
                    grid.Rows[0].Cells[1].Style = CellStyle(7);
                    grid.Rows[0].Height = 26;
                    
                    bottomPart.Rows[0].Height = 30;

                    #endregion
                    
                    mainGrid.Rows[0].Cells[2].Value = grid;
                }
                {
                    #region GridHeaderFourthChapter
                    mainGrid.Rows[0].Cells[3].Value = "Uwagi";
                    mainGrid.Rows[0].Cells[3].Style = CellStyle(7, false, PdfTextAlignment.Center, 10, 10, 22);

                    #endregion


                }

                #endregion
            }
            {
                #region GridContent

                {
                    #region LeftSide

                    mainGrid.Rows.Add();
                    for (int i = 1; i < 5; i++)
                    {
                        mainGrid.Rows.Add();
                        mainGrid.Rows[i].Height = 57;
                        PdfGrid leftSideContent = new();
                        leftSideContent.Rows.Add();
                        leftSideContent.Columns.Add(3);
                        leftSideContent.Columns[0].Width = 48;
                        leftSideContent.Columns[1].Width = 47;
                        leftSideContent.Columns[2].Width = 33;
                        leftSideContent.Rows[0].Height = 57;

                        mainGrid.Rows[i].Cells[0].Value = leftSideContent;
                    }

                    #endregion
                }
                {
                    #region Center

                    PdfGrid gridContent = new();
                    for (int i = 1; i < 5; i++)
                    {
                        PdfGrid gridColumns = new();
                        gridColumns.Columns.Add(4);
                        gridColumns.Rows.Add();
                        gridColumns.Rows[0].Height = 57;
                        mainGrid.Rows[i].Cells[1].Value = gridColumns;
                        mainGrid.Rows[i].Cells[2].Value = gridColumns;
                    }
        
                    #endregion
                }
                #endregion
            }

            #endregion

            mainGrid.Draw(Page, new PointF(0, 300));

            MemoryStream stream = new MemoryStream();
            documents.Save(stream);
            documents.Close(true);

            return stream;
        }
    }
}