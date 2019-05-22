//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;

//namespace Util
//{
//    public static class GerarExcel
//    {
//        public static byte[] GerarExcelArquivo(string vNomeAba, DataTable vConteudo)
//        {
//            byte[] vBytes;

//            using (MemoryStream vMemoryStream = new MemoryStream())
//            {
//                SpreadsheetDocument vArquivo = SpreadsheetDocument.Create(vMemoryStream, SpreadsheetDocumentType.Workbook);

//                WorkbookPart vWorkbook = vArquivo.AddWorkbookPart();
//                vWorkbook.Workbook = new Workbook();

//                WorksheetPart vPlanilha = vWorkbook.AddNewPart<WorksheetPart>();
//                vPlanilha.Worksheet = new Worksheet(new SheetData());

//                WorkbookStylesPart vStylesPart = vArquivo.WorkbookPart.AddNewPart<WorkbookStylesPart>();
//                vStylesPart.Stylesheet = CriarEstilos();
//                vStylesPart.Stylesheet.Save();

//                Sheets vSheets = vArquivo.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
//                SheetData vDadosPlanilha = new SheetData();

//                Row headerRow = new Row();

//                List<string> columns = new List<string>();
//                foreach (DataColumn column in vConteudo.Columns)
//                {
//                    columns.Add(column.ColumnName);

//                    Cell cell = new Cell();
//                    cell.DataType = CellValues.String;
//                    cell.CellValue = new CellValue(column.ColumnName);
//                    cell.StyleIndex = 4;
//                    headerRow.AppendChild(cell);
//                }
//                vDadosPlanilha.AppendChild(headerRow);

//                foreach (DataRow dsrow in vConteudo.Rows)
//                {
//                    Row newRow = new Row();
//                    foreach (string col in columns)
//                    {
//                        Cell cell = new Cell();
//                        cell.DataType = CellValues.String;
//                        cell.CellValue = col == "Senha" ? new CellValue(SCV.Util.Descriptografar(dsrow[col].ToString())) : new CellValue(dsrow[col].ToString());
//                        newRow.AppendChild(cell);
//                    }

//                    vDadosPlanilha.AppendChild(newRow);
//                }

//                SheetViews vSheetViews = new SheetViews();
//                SheetView vSheetView = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
//                Pane vPane = new Pane() { VerticalSplit = 1D, TopLeftCell = "A2", ActivePane = PaneValues.BottomLeft, State = PaneStateValues.Frozen };

//                vSheetView.Append(vPane);
//                vSheetViews.Append(vSheetView);

//                AutoFilter vAutoFilter = new AutoFilter() { Reference = "A1:" + (char)(vConteudo.Columns.Count + 'A' - 1) + (vConteudo.Rows.Count + 1) };

//                Worksheet vWorksheet = new Worksheet();
//                vWorksheet.Append(vSheetViews);
//                vWorksheet.Append(vDadosPlanilha);
//                vWorksheet.Append(vAutoFilter);

//                vPlanilha.Worksheet = vWorksheet;

//                Sheet vSheet = new Sheet();
//                vSheet.Id = vArquivo.WorkbookPart.GetIdOfPart(vPlanilha);
//                vSheet.SheetId = 1;
//                vSheet.Name = vNomeAba;

//                vSheets.Append(vSheet);

//                DefinedNames vDefinedNames = new DefinedNames();
//                DefinedName vDefinedName = new DefinedName
//                {
//                    Text = "'" + vSheet.Name + "'!$A$1:$" + (char)(vConteudo.Columns.Count + 'A' - 1) + "$" + (vConteudo.Rows.Count + 1),
//                    Name = "_xlnm._FilterDatabase",
//                    LocalSheetId = 0,
//                    Hidden = true,
//                };
//                vDefinedNames.Append(vDefinedName);
//                vArquivo.WorkbookPart.Workbook.Append(vDefinedNames);

//                vWorkbook.Workbook.Save();
//                vArquivo.Close();

//                vBytes = vMemoryStream.ToArray();
//                vMemoryStream.Close();
//            }
//            return vBytes;
//        }

//        private static Stylesheet CriarEstilos()
//        {
//            Stylesheet vStylesheet = new Stylesheet();
//            vStylesheet.Append(CriarFontes());
//            vStylesheet.Append(CriarCorFundo());
//            vStylesheet.Append(CriarBorda());
//            vStylesheet.Append(CriarCellEstilos());

//            return vStylesheet;
//        }

//        private static Fonts CriarFontes()
//        {
//            Fonts vFontes = new Fonts();

//            // Index 0 - Fonte default (tam = 11; cor = automatico; fonte = Calibri)
//            Font vFonteDefault = new Font();
//            vFonteDefault.Append(new FontSize() { Val = 11 });
//            vFonteDefault.Append(new Color() { Rgb = new HexBinaryValue() { Value = "000000" } });
//            vFonteDefault.Append(new FontName() { Val = "Calibri" });

//            // Index 1 - Fonte negrito (estilo = negrito; tam = 11; cor = automatico; fonte = Calibri)
//            Font vFonteNegrito = new Font();
//            vFonteNegrito.Append(new Bold());
//            vFonteNegrito.Append(new FontSize() { Val = 11 });
//            vFonteNegrito.Append(new Color() { Rgb = new HexBinaryValue() { Value = "000000" } });
//            vFonteNegrito.Append(new FontName() { Val = "Calibri" });

//            // Index 2 - Fonte negrito (estilo = italico; tam = 11; cor = automatico; fonte = Calibri)
//            Font vFonteItalico = new Font();
//            vFonteItalico.Append(new Italic());
//            vFonteItalico.Append(new FontSize() { Val = 11 });
//            vFonteItalico.Append(new Color() { Rgb = new HexBinaryValue() { Value = "000000" } });
//            vFonteItalico.Append(new FontName() { Val = "Calibri" });

//            // Index 3 - Fonte customizado (tam = 16; cor = automatico; fonte = Times New Roman)
//            Font vFonteCustom = new Font();
//            vFonteCustom.Append(new FontSize() { Val = 16 });
//            vFonteCustom.Append(new Color() { Rgb = new HexBinaryValue() { Value = "000000" } });
//            vFonteCustom.Append(new FontName() { Val = "Times New Roman" });

//            vFontes.Append(vFonteDefault);
//            vFontes.Append(vFonteNegrito);
//            vFontes.Append(vFonteItalico);
//            vFontes.Append(vFonteCustom);

//            return vFontes;
//        }

//        private static Fills CriarCorFundo()
//        {
//            Fills vFills = new Fills();

//            // Index 0 - Fill default (nenhum)
//            Fill vFillDefault = new Fill();
//            vFillDefault.Append(new PatternFill() { PatternType = PatternValues.None });

//            // Index 1 - Fill cinza
//            Fill vFillCinza = new Fill();
//            vFillCinza.Append(new PatternFill() { PatternType = PatternValues.Gray125 });

//            // Index 2 - Fill amarelo
//            Fill vFillAmarelo = new Fill();
//            vFillAmarelo.Append(new PatternFill(new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "FFFFFF00" } }) { PatternType = PatternValues.Solid });

//            // Index 3 - Fill lilas
//            Fill vFillLilas = new Fill();
//            vFillLilas.Append(new PatternFill(new ForegroundColor() { Rgb = new HexBinaryValue() { Value = "AEC7FF" } }) { PatternType = PatternValues.Solid });

//            vFills.Append(vFillDefault);
//            vFills.Append(vFillCinza);
//            vFills.Append(vFillAmarelo);
//            vFills.Append(vFillLilas);

//            return vFills;
//        }

//        private static Borders CriarBorda()
//        {
//            Borders vBordas = new Borders();

//            // Index 0 - Borda default (nenhum)
//            Border vBorderDefault = new Border();
//            vBorderDefault.Append(new LeftBorder());
//            vBorderDefault.Append(new RightBorder());
//            vBorderDefault.Append(new TopBorder());
//            vBorderDefault.Append(new BottomBorder());
//            vBorderDefault.Append(new DiagonalBorder());

//            // Index 1 - Borda fina esquerda, direita, encima, embaixo
//            Border vBorderFino = new Border();
//            vBorderFino.Append(new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin });
//            vBorderFino.Append(new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin });
//            vBorderFino.Append(new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin });
//            vBorderFino.Append(new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin });
//            vBorderFino.Append(new DiagonalBorder());

//            vBordas.Append(vBorderDefault);
//            vBordas.Append(vBorderFino);

//            return vBordas;
//        }

//        private static CellFormats CriarCellEstilos()
//        {
//            CellFormats vCellFormats = new CellFormats();

//            // Index 0 - Célula Default
//            CellFormat vCelulaDefault = new CellFormat();
//            vCelulaDefault.FontId = 0;
//            vCelulaDefault.FillId = 0;
//            vCelulaDefault.BorderId = 0;

//            // Index 1 - Célula Negrito
//            CellFormat vCelulaNegrito = new CellFormat();
//            vCelulaNegrito.FontId = 1;
//            vCelulaNegrito.FillId = 0;
//            vCelulaNegrito.BorderId = 0;
//            vCelulaNegrito.ApplyFont = true;

//            // Index 2 - Célula Italico
//            CellFormat vCelulaItalico = new CellFormat();
//            vCelulaItalico.FontId = 2;
//            vCelulaItalico.FillId = 0;
//            vCelulaItalico.BorderId = 0;
//            vCelulaItalico.ApplyFont = true;

//            // Index 3 - Célula Times New Roman
//            CellFormat vCelulaCustomizado = new CellFormat();
//            vCelulaCustomizado.FontId = 3;
//            vCelulaCustomizado.FillId = 0;
//            vCelulaCustomizado.BorderId = 0;
//            vCelulaCustomizado.ApplyFont = true;

//            // Index 4 - Célula com fundo e negrito e alinhado e com borda
//            CellFormat vCelulaFundo = new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
//            vCelulaFundo.FontId = 1;
//            vCelulaFundo.FillId = 3;
//            vCelulaFundo.BorderId = 1;
//            vCelulaFundo.ApplyFont = true;
//            vCelulaFundo.ApplyFill = true;
//            vCelulaFundo.ApplyBorder = true;

//            // Index 5 - Célula alinhado
//            CellFormat vCelulaAlinhado = new CellFormat(new Alignment() { Horizontal = HorizontalAlignmentValues.Center, Vertical = VerticalAlignmentValues.Center });
//            vCelulaAlinhado.FontId = 0;
//            vCelulaAlinhado.FillId = 0;
//            vCelulaAlinhado.BorderId = 0;
//            vCelulaAlinhado.ApplyAlignment = true;

//            // Index 6 - Célula com borda
//            CellFormat vCelulaBorda = new CellFormat();
//            vCelulaBorda.FontId = 0;
//            vCelulaBorda.FillId = 0;
//            vCelulaBorda.BorderId = 1;
//            vCelulaBorda.ApplyBorder = true;

//            vCellFormats.Append(vCelulaDefault);
//            vCellFormats.Append(vCelulaNegrito);
//            vCellFormats.Append(vCelulaItalico);
//            vCellFormats.Append(vCelulaCustomizado);
//            vCellFormats.Append(vCelulaFundo);
//            vCellFormats.Append(vCelulaAlinhado);
//            vCellFormats.Append(vCelulaBorda);

//            return vCellFormats;
//        }
//    }
//}