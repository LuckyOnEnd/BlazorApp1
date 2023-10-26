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

        private PdfGridLayoutResult GenerateInput(int width, PointF position, int height = 10, string text = "")
        {
            PdfGrid grid = new();
            grid.Rows.Add();
            grid.Columns.Add(1);

            grid.Rows[0].Height = height;
            grid.Columns[0].Width = width;
            grid.Rows[0].Cells[0].Value = text;

            return grid.Draw(Page, position);
        }

        public async Task<MemoryStream> CreatePDF()
        {
            PdfDocument documents = new PdfDocument();
            Page = documents.Pages.Add();
            PdfGraphics Graphics = Page.Graphics;

            #region TITLE

            Graphics.DrawString("Protokół sprawdzenia technicznej sprawności", FontSize(13, true), PdfBrushes.Black,
                new PointF(130, 37));

            #endregion

            #region SmallTitle

            Graphics.DrawString("przewodów kominowych i instalacji gazowej", FontSize(8), PdfBrushes.Black, new PointF(185, 60));
            Graphics.DrawString("wykonany na zlecenie", FontSize(8, true), PdfBrushes.Black, new PointF(210, 72));
            
            Graphics.DrawString("Spółdzielni Mieszkaniowej \"Twoje Osiedle\"", FontSize(7), PdfBrushes.Black, new PointF(185, 93));

            #endregion

            #region FirstChapter

            var firstAndLastNameInput = GenerateInput(70, new PointF(50, 120));
            Graphics.DrawString("(Nazwisko i imię użytkownika)", FontSize(6), PdfBrushes.Black,
                new PointF(firstAndLastNameInput.Bounds.Left - 5, firstAndLastNameInput.Bounds.Bottom + 2));

            var identificate = GenerateInput(75, new PointF(150, 120));
            Graphics.DrawString("(Identyfikator)", FontSize(6), PdfBrushes.Black,
                new PointF(identificate.Bounds.Left + 19, identificate.Bounds.Bottom + 2));
            
            var address = GenerateInput(75, new PointF(240, 120));
            Graphics.DrawString("(Ulica, nr domu, nr mieszkania)", FontSize(6), PdfBrushes.Black,
                new PointF(address.Bounds.Left, address.Bounds.Bottom + 2));
            
            var phoneNumber = GenerateInput(75, new PointF(330, 120));
            Graphics.DrawString("Nr telefonu", FontSize(6), PdfBrushes.Black,
                new PointF(phoneNumber.Bounds.Left + 22, phoneNumber.Bounds.Bottom + 2));

            var QCDate = GenerateInput(70, new PointF(420, 120));
            Graphics.DrawString("Data sprawdzania", FontSize(6), PdfBrushes.Black,
                new PointF(QCDate.Bounds.Left + 17, QCDate.Bounds.Bottom + 2));

            #endregion

            #region SecondChapter

            GenerateInput(63, new PointF(112, 145));
            Graphics.DrawString("Temperatura zewnętrzna", FontSize(8), PdfBrushes.Black, new PointF(16, 147));

            GenerateInput(70, new PointF(285, 145));
            Graphics.DrawString("Temperatura wewnętrzna", FontSize(8), PdfBrushes.Black, new PointF(190, 147));

            GenerateInput(40, new PointF(430, 145));
            Graphics.DrawString("Ilość nawiewników", FontSize(8), PdfBrushes.Black, new PointF(365, 147));

            #endregion

            #region GridChapter

            PdfGrid mainGrid = new();
            mainGrid.Columns.Add(5);
            mainGrid.Columns[3].Width = 17;

            {
                #region GridHeader

                mainGrid.Rows.Add();
                mainGrid.Rows[0].Height = 55;

                {
                    #region GridHeaderFirstChapter

                    PdfGrid grid = new PdfGrid();
                    grid.Columns.Add(3);
                    grid.Columns[0].Width = 50;
                    grid.Columns[1].Width = 45;
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


                    #region TextRotation

                    PdfGraphicsState state = Page.Graphics.Save();
                    Page.Graphics.TranslateTransform(100,100);
                    Page.Graphics.RotateTransform(-90);
                    Page.Graphics.DrawString("  Przepływ w\npomieszczeniu", FontSize(6), PdfBrushes.Black, new PointF(-109, -83));
                    Page.Graphics.DrawString( "  Wymagania\nPN-83/B-03430", FontSize(6), PdfBrushes.Black, new PointF(-107, -35));
                    
                    Page.Graphics.DrawString( " Wymiary\n   kratek", FontSize(6), PdfBrushes.Black, new PointF(-96, 5));
                    
                    Page.Graphics.Restore(state);

                    #endregion
                    
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
                        bottomGrid.Columns[0].Width = 63;
                        
                        bottomGrid.Rows[0].Cells[0].Style.Borders = HideBorder();
                        bottomGrid.Rows[0].Cells[1].Style.Borders = HideBorder(true);
                        
                        {
                            #region BottomLeftSide

                            PdfGrid leftSideGrid = new();
                            leftSideGrid.Columns.Add(1);
                            leftSideGrid.Rows.Add();
                            leftSideGrid.Rows.Add();

                            leftSideGrid.Rows[0].Cells[0].Style = CellStyle(7, false, PdfTextAlignment.Center, 0, 0, 2);
                            leftSideGrid.Rows[0].Cells[0].Value = "Okna zamknięte";
                            leftSideGrid.Rows[0].Cells[0].Style.Borders = HideBorder();
                            leftSideGrid.Rows[1].Cells[0].Style.Borders = HideBorder();
                            leftSideGrid.Rows[0].Height = 17;
                            leftSideGrid.Rows[1].Height = 16;

                            PdfGrid bottom = new();
                            bottom.Columns.Add(2);
                            bottom.Rows.Add();
                            bottom.Rows[0].Height = 15;
                            bottom.Rows[0].Cells[0].Value = "m/s";
                            bottom.Rows[0].Cells[1].Value = "m3/h";
                            bottom.Rows[0].Cells[0].Style = CellStyle(8);
                            bottom.Rows[0].Cells[1].Style = CellStyle(8);
                            bottom.Columns[0].Width = 29;
                            bottom.Rows[0].Cells[0].Style.Borders = HideBorder(false, true);
                            bottom.Rows[0].Cells[1].Style.Borders = HideBorder(true, true);

                            leftSideGrid.Rows[1].Cells[0].Value = bottom;

                            #endregion

                            #region BottomRightSide

                            PdfGrid rightSideGrid = new();
                            rightSideGrid.Columns.Add(1);
                            rightSideGrid.Rows.Add();
                            rightSideGrid.Rows.Add();

                            rightSideGrid.Rows[0].Cells[0].Style = CellStyle(7);
                            rightSideGrid.Rows[0].Cells[0].Value = "Okna (mikrouchył\ndo 10 mm)";
                            rightSideGrid.Rows[0].Cells[0].Style.Borders = HideBorder();
                            rightSideGrid.Rows[1].Cells[0].Style.Borders = HideBorder();
                            rightSideGrid.Rows[0].Height = 17;
                            rightSideGrid.Rows[1].Height = 16;

                            PdfGrid bottomRight = new();
                            bottomRight.Columns.Add(2);
                            bottomRight.Columns[0].Width = 30.5f;
                            bottomRight.Rows.Add();
                            bottomRight.Rows[0].Height = 15;
                            bottomRight.Rows[0].Cells[0].Value = "m/s";
                            bottomRight.Rows[0].Cells[1].Value = "m3/h";
                            bottomRight.Rows[0].Cells[0].Style = CellStyle(8);
                            bottomRight.Rows[0].Cells[1].Style = CellStyle(8);
                            bottomRight.Rows[0].Cells[0].Style.Borders = HideBorder(false,true);
                            bottomRight.Rows[0].Cells[1].Style.Borders = HideBorder(true, true);

                            rightSideGrid.Rows[1].Cells[0].Value = bottomRight;

                            #endregion
                            
                            bottomGrid.Rows[0].Cells[0].Value = leftSideGrid;
                            bottomGrid.Rows[0].Cells[1].Value = rightSideGrid;
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
                    bottomPart.Rows[0].Cells[0].Style = CellStyle(6, false, PdfTextAlignment.Center, 2, 2, 4);
                    bottomPart.Rows[0].Cells[1].Style = CellStyle(6, false, PdfTextAlignment.Center, 2, 2, 4);
                    bottomPart.Rows[0].Cells[0].Style.Borders = HideBorder();
                    bottomPart.Rows[0].Cells[1].Style.Borders = HideBorder(true);
                    bottomPart.Columns[0].Width = 31;
                    
                    grid.Rows[1].Cells[0].Value = bottomPart;
                    grid.Rows[1].Cells[1].Value = bottomPart;
                    grid.Rows[0].Cells[0].Style = CellStyle(7);
                    grid.Rows[0].Cells[1].Style = CellStyle(7);
                    grid.Rows[0].Height = 26;
                    bottomPart.Rows[0].Height = 30;
                    
                    grid.Rows[0].Cells[0].Style.Borders = HideBorder();
                    grid.Rows[0].Cells[1].Style.Borders = HideBorder(true);
                    grid.Rows[1].Cells[0].Style.Borders = HideBorder(false,true);
                    grid.Rows[1].Cells[1].Style.Borders = HideBorder(true, true);
                    #endregion

                    mainGrid.Rows[0].Cells[2].Value = grid;
                    mainGrid.Rows[0].Cells[2].Style.Borders = HideBorder(false, true, false, true);
                }
                {
                    PdfGraphicsState state = Page.Graphics.Save();
                    Page.Graphics.TranslateTransform(100,100);
                    Page.Graphics.RotateTransform(-90);
                    Page.Graphics.DrawString("czyszczenie", FontSize(7), PdfBrushes.Black, new PointF(-109, 277));
                    Page.Graphics.Restore(state);
                    #region GridHeaderFourthChapter

                    mainGrid.Rows[0].Cells[4].Value = "Uwagi";
                    mainGrid.Rows[0].Cells[4].Style = CellStyle(7, false, PdfTextAlignment.Center, 10, 10, 22);

                    #endregion
                }

                #endregion
            }
            {
                #region GridContent

                var totalRows = 5;
                for (int i = 1; i <= totalRows; i++)
                {
                    mainGrid.Rows.Add();

                    PdfGrid grid = new();
                    grid.Rows.Add();
                    grid.Columns.Add(3);

                    #region HeightAndWidthSettings

                    grid.Columns[0].Width = 50;
                    grid.Columns[1].Width = 45;
                    grid.Columns[2].Width = 33;

                    grid.Rows[0].Height = 40;

                    #endregion

                    grid.Rows[0].Cells[0].Value = "Kuchnia";
                    grid.Rows[0].Cells[1].Value = "70";
                    grid.Rows[0].Cells[2].Value = " ";

                    grid.Rows[0].Cells[0].Style = CellStyle(9, true, PdfTextAlignment.Center, 0, 0, 15);
                    grid.Rows[0].Cells[1].Style = CellStyle(9, true, PdfTextAlignment.Center, 0, 0, 15);
                    grid.Rows[0].Cells[2].Style = CellStyle(9, true, PdfTextAlignment.Center, 0, 0, 15);

                    mainGrid.Rows[1].Cells[0].Value = grid;

                    grid.Rows[0].Cells[0].Style.Borders = HideBorder();
                    grid.Rows[0].Cells[1].Style.Borders = HideBorder(true);
                    grid.Rows[0].Cells[2].Style.Borders = HideBorder(true);

                    mainGrid.Rows[i].Cells[0].Value = grid;
                }
                {
                    #region Bottom

                    mainGrid.Rows.Add();
                    {
                        #region FirstColumn

                        PdfGrid grid = new();
                        grid.Rows.Add();
                        grid.Columns.Add(2);

                        grid.Rows[0].Height = 40;
                        grid.Rows[0].Cells[0].Value = "Suma\nobjętości\nstrumienia\npowietrza";
                        grid.Columns[0].Width = 50;
                        grid.Rows[0].Cells[0].Style = CellStyle(9, true);
                        grid.Rows[0].Cells[0].Style.Borders = HideBorder(false, false, true);

                        PdfGrid rightSideGrid = new();
                        rightSideGrid.Rows.Add();
                        rightSideGrid.Rows.Add();
                        rightSideGrid.Columns.Add(1);

                        rightSideGrid.Rows[0].Height = 19;
                        rightSideGrid.Rows[1].Height = 21;
                        rightSideGrid.Rows[0].Cells[0].Value = "ZMIERZONA";
                        rightSideGrid.Rows[1].Cells[0].Value = "WYMAGANA\nPN/83-B/03430";
                        rightSideGrid.Rows[0].Cells[0].Style = CellStyle(8, true, PdfTextAlignment.Center, 0, 0, 2);
                        rightSideGrid.Rows[1].Cells[0].Style = CellStyle(8, true, PdfTextAlignment.Center, 0, 0, 2);
                        
                        rightSideGrid.Rows[0].Cells[0].Style.Borders = HideBorder();
                        rightSideGrid.Rows[1].Cells[0].Style.Borders = HideBorder(false, true);

                        grid.Rows[0].Cells[1].Value = rightSideGrid;
                        grid.Rows[0].Cells[1].Style.Borders = HideBorder();

                        #endregion

                        mainGrid.Rows[mainGrid.Rows.Count - 1].Cells[0].Value = grid;
                    }
                    {
                        #region SecondColumn

                        PdfGrid grid = new();

                        grid.Rows.Add();
                        grid.Rows.Add();

                        grid.Rows[0].Height = 20;
                        grid.Rows[1].Height = 20;
                        
                        grid.Columns.Add(1);

                        PdfGrid topRowGrid = new();
                        topRowGrid.Columns.Add(2);
                        topRowGrid.Rows.Add();
                        topRowGrid.Rows[0].Height = 17;
                      //  topRowGrid.Columns[0].Width = 63;
                        topRowGrid.Rows[0].Cells[0].Style.Borders = HideBorder();
                        topRowGrid.Rows[0].Cells[1].Style.Borders = HideBorder();

                        grid.Rows[0].Cells[0].Value = topRowGrid;
                        grid.Rows[0].Cells[0].Style.Borders = HideBorder();
                        grid.Rows[1].Cells[0].Style.Borders = HideBorder(false, true);
                        #endregion

                        mainGrid.Rows[mainGrid.Rows.Count - 1].Cells[1].Value = grid;
                    }
                    {
                        #region ThirdColumn

                        PdfGrid grid = new();
                        grid.Rows.Add();
                        grid.Columns.Add(2);
                        grid.Rows[0].Height = 40;
                        
                        grid.Columns[0].Width = 64;
                        grid.Columns[1].Width = 63;

                        grid.Rows[0].Cells[0].Style.Borders = HideBorder();
                        grid.Rows[0].Cells[1].Style.Borders = HideBorder(true);
                        
                        #endregion

                        mainGrid.Rows[mainGrid.Rows.Count - 1].Cells[2].Value = grid;
                        mainGrid.Rows[mainGrid.Rows.Count - 1].Cells[3].Style.Borders = HideBorder(false, false, false, true);
                        mainGrid.Rows[mainGrid.Rows.Count - 1].Cells[4].Style.Borders = HideBorder(false, true, true, true);
                    }

                    #endregion
                }
                #endregion
            }

            #endregion
            
            var gridDrawResult = mainGrid.Draw(Page, new PointF(0, 165));


            #region ThirdChapter

            Graphics.DrawString("Uwagi - wentylacja", FontSize(8), PdfBrushes.Black, new PointF(gridDrawResult.Bounds.Left + 50, gridDrawResult.Bounds.Bottom + 10));
            GenerateInput(400, new PointF(gridDrawResult.Bounds.Left + 50, gridDrawResult.Bounds.Bottom + 20), 20, "Zobowiązuje się użytkownika mieszkania do zapewnienia właściwego napływu powietrza do\nmieszkania");
            
            Graphics.DrawString("Instalacja gazowa", FontSize(8), PdfBrushes.Black, new PointF(gridDrawResult.Bounds.Left + 50, gridDrawResult.Bounds.Bottom + 50));
            GenerateInput(100, new PointF(gridDrawResult.Bounds.Left + 120, gridDrawResult.Bounds.Bottom + 50));

            Graphics.DrawString("Urządzenie gazowe:", FontSize(8), PdfBrushes.Black, new PointF(gridDrawResult.Bounds.Left + 50, gridDrawResult.Bounds.Bottom + 70));

            PdfGridLayoutResult thirdChapterGenerateInput = null;
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    Graphics.DrawString((i+1) + ". Kuchnia gazowa", FontSize(8), PdfBrushes.Black, new PointF(gridDrawResult.Bounds.Left + 125, gridDrawResult.Bounds.Bottom + 70));
                    thirdChapterGenerateInput = GenerateInput(100, new PointF(gridDrawResult.Bounds.Left + 225, gridDrawResult.Bounds.Bottom + 70));
                }
                else
                {
                    Graphics.DrawString((i+1) + ". Kuchnia gazowa", FontSize(8), PdfBrushes.Black, new PointF(gridDrawResult.Bounds.Left + 125, thirdChapterGenerateInput.Bounds.Bottom + 6));
                    thirdChapterGenerateInput = GenerateInput(100, new PointF(thirdChapterGenerateInput.Bounds.Left, thirdChapterGenerateInput.Bounds.Bottom + 6));
                }
            }
            
            Graphics.DrawString("Zawartość tlenku węgla w spalinach", FontSize(8), PdfBrushes.Black,  new PointF(gridDrawResult.Bounds.Left + 50, thirdChapterGenerateInput.Bounds.Bottom + 6));
            thirdChapterGenerateInput = GenerateInput(100, new PointF(thirdChapterGenerateInput.Bounds.Left, thirdChapterGenerateInput.Bounds.Bottom + 6));
            Graphics.DrawString("[max 500ppm]", FontSize(8), PdfBrushes.Black,  new PointF(gridDrawResult.Bounds.Left + 50, thirdChapterGenerateInput.Bounds.Bottom));
            
            
            Graphics.DrawString("Uwagi", FontSize(8), PdfBrushes.Black, new PointF(gridDrawResult.Bounds.Left + 50, thirdChapterGenerateInput.Bounds.Bottom + 12)); //TODO check when null
            GenerateInput(400, new PointF(gridDrawResult.Bounds.Left + 50, thirdChapterGenerateInput.Bounds.Bottom + 22), 20);

            Graphics.DrawString("Zalecenia lub uwagi dla użytkownika lokalu", FontSize(8), PdfBrushes.Black, new PointF(gridDrawResult.Bounds.Left + 50, thirdChapterGenerateInput.Bounds.Bottom + 45));
            GenerateInput(400, new PointF(gridDrawResult.Bounds.Left + 50, thirdChapterGenerateInput.Bounds.Bottom + 55), 40);

            Graphics.DrawString("Zalecenia lub uwagi dla zarządcy budynku", FontSize(8), PdfBrushes.Black, new PointF(gridDrawResult.Bounds.Left + 50, thirdChapterGenerateInput.Bounds.Bottom + 100));
            var fourthChapterLastInputPostion = GenerateInput(400, new PointF(gridDrawResult.Bounds.Left + 50, thirdChapterGenerateInput.Bounds.Bottom + 110), 20);

            #endregion

            #region FifthChapter

            PdfGrid fifthChapterGrid = new();
            fifthChapterGrid.Columns.Add(3);
            fifthChapterGrid.Columns[0].Width = 140;
            fifthChapterGrid.Columns[1].Width = 140;
            fifthChapterGrid.Rows.Add();
            fifthChapterGrid.Rows.Add();
            fifthChapterGrid.Rows.Add();
            fifthChapterGrid.Rows[0].Height = 10;
            fifthChapterGrid.Rows[1].Height = 35;
            fifthChapterGrid.Rows[2].Height = 10;

            fifthChapterGrid.Rows[1].Cells[0].Value = "MISTRZ KOMINIARSKI\nSzymon Mączyński\nNr upr. 5598";
            fifthChapterGrid.Rows[1].Cells[0].Style = CellStyle(7, false, PdfTextAlignment.Center, 0, 0);
            
            fifthChapterGrid.Rows[2].Cells[0].Value = "Pomiar wentylacji wykonał\n(podpis, pieczątka i nr uprawnień)";
            fifthChapterGrid.Rows[2].Cells[0].Style = CellStyle(7, false, PdfTextAlignment.Center, 0, 0);
            
            fifthChapterGrid.Rows[1].Cells[1].Value = "KONTROLER INSTALACJI GAZOWEJ\nRafał Niegot\nG3-E/103/015/21\nG3-D/104/015/21";
            fifthChapterGrid.Rows[1].Cells[1].Style = CellStyle(7, false, PdfTextAlignment.Center, 0, 0);
            
            fifthChapterGrid.Rows[2].Cells[1].Value = "Pomiar szczelności i sprawności wykonał\n(podpis, pieczątka i nr uprawnień)";
            fifthChapterGrid.Rows[2].Cells[1].Style = CellStyle(7, false, PdfTextAlignment.Center, 0, 0);

            fifthChapterGrid.Rows[0].Cells[2].Value = "wyniki pomiarów odebrano";
            fifthChapterGrid.Rows[0].Cells[2].Style = CellStyle(6, false, PdfTextAlignment.Center, 0, 0, 2);
            
            fifthChapterGrid.Rows[2].Cells[2].Value = "Podpis mieszkańca lub\nużytkownika mieszkania";
            fifthChapterGrid.Rows[2].Cells[2].Style = CellStyle(6);
            
            fifthChapterGrid.Rows[0].Cells[0].Style.Borders = HideBorder();
            fifthChapterGrid.Rows[1].Cells[0].Style.Borders = HideBorder();
            fifthChapterGrid.Rows[0].Cells[1].Style.Borders = HideBorder();
            fifthChapterGrid.Rows[1].Cells[1].Style.Borders = HideBorder();
            fifthChapterGrid.Rows[0].Cells[2].Style.Borders = HideBorder();
            fifthChapterGrid.Rows[1].Cells[2].Style.Borders = HideBorder();
            fifthChapterGrid.Rows[2].Cells[2].Style.Borders = HideBorder();
            fifthChapterGrid.Rows[2].Cells[1].Style.Borders = HideBorder();
            fifthChapterGrid.Rows[2].Cells[0].Style.Borders = HideBorder();
            
            fifthChapterGrid.Draw(Page, new PointF(50, gridDrawResult.Bounds.Bottom + 245));
            #endregion
            
            
            # region GRID_BORDERS_PROBLEM
            Graphics.DrawLine(new PdfPen(PdfBrushes.Black, 1),new PointF(124.5f, 211), new PointF(248.5f, 211));
            Graphics.DrawLine(new PdfPen(PdfBrushes.Black, 1), new PointF(187.1f, gridDrawResult.Bounds.Bottom - 42), new PointF(187.1f, gridDrawResult.Bounds.Bottom - 22));
            #endregion

            
            MemoryStream stream = new MemoryStream();
            documents.Save(stream);
            documents.Close(true);

            return stream;
        }
    }
}
