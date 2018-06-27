using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ExcelTools.Core.Writing
{
    public static class StylesProvider
    {
        public static Stylesheet ToStylesheet()
        {
            return new Stylesheet(
                            GetFonts(),
                            GetFills(),
                            GetBorders(),
                            GetCellStyleFormats(),
                            GetCellFormats());
        }

        #region Private
        private static Fonts GetFonts()
        {
            var fonts = new SortedDictionary<FontIndex, Font>
                                {
                                    { FontIndex.General, new Font() },
                                    { FontIndex.HeaderFont, new Font { Bold = new Bold() } }
                                };

            return new Fonts(fonts.Values);
        }

        private static Fills GetFills()
        {
            var fills = new SortedDictionary<FillIndex, Fill>
                                {
                                    // Excel 2010 requires two <fill>-s (before custom ones)
                                    { FillIndex.General, new Fill(new PatternFill { PatternType = PatternValues.None }) },
                                    { FillIndex.Gray125, new Fill(new PatternFill { PatternType = PatternValues.None }) },
                                    { FillIndex.Header, new Fill(new PatternFill(new ForegroundColor { Rgb = HexBinaryValue.FromString("FFEEEEEE") }) { PatternType = PatternValues.Solid }) },
                                    { FillIndex.Error, new Fill(new PatternFill(new ForegroundColor { Rgb = HexBinaryValue.FromString("FFFFFF00") }) { PatternType = PatternValues.Solid }) }
                                };

            return new Fills(fills.Values);
        }

        private static Borders GetBorders()
        {
            var borders = new SortedDictionary<BorderIndex, Border>
                                {
                                    // Excel 2010 requires a <border> (before custom ones)
                                    { BorderIndex.General, new Border() },
                                    { BorderIndex.Bordered, new Border(
                                                                new LeftBorder { Style = new EnumValue<BorderStyleValues>(BorderStyleValues.Thin) },
                                                                new RightBorder { Style = new EnumValue<BorderStyleValues>(BorderStyleValues.Thin) },
                                                                new TopBorder { Style = new EnumValue<BorderStyleValues>(BorderStyleValues.Thin) },
                                                                new BottomBorder { Style = new EnumValue<BorderStyleValues>(BorderStyleValues.Thin) },
                                                                new DiagonalBorder()) }
                                };

            return new Borders(borders.Values);
        }

        private static CellStyleFormats GetCellStyleFormats()
        {
            var cellStyleFormats = new SortedDictionary<CellStyleFormatIndex, CellFormat>
                                {
                                    { CellStyleFormatIndex.General, new CellFormat { NumberFormatId = (uint)NumberFormatId.General_0, FontId = (uint)FontIndex.General, FillId = (uint)FillIndex.General, BorderId = (uint)BorderIndex.General } }
                                };

            return new CellStyleFormats(cellStyleFormats.Values);
        }

        private static CellFormats GetCellFormats()
        {
            var cellFormats = new SortedDictionary<CellFormatIndex, CellFormat>
                                {
                                    { CellFormatIndex.General, CreateCellFormat() },
                                    { CellFormatIndex.String, CreateCellFormat(numberFormatId : NumberFormatId.String_49) },
                                    { CellFormatIndex.HeaderString, CreateCellFormat(numberFormatId : NumberFormatId.String_49, alignment : new Alignment { Horizontal = new EnumValue<HorizontalAlignmentValues>(HorizontalAlignmentValues.Center), Vertical = new EnumValue<VerticalAlignmentValues>(VerticalAlignmentValues.Center) }, fontId : FontIndex.HeaderFont, fillId : FillIndex.Header, borderId: BorderIndex.Bordered)},
                                    { CellFormatIndex.Int, CreateCellFormat(numberFormatId : NumberFormatId.Int_1, formatId : CellStyleFormatIndex.General) },
                                    { CellFormatIndex.Decimal, CreateCellFormat(numberFormatId : NumberFormatId.Decimal_2) },
                                    { CellFormatIndex.Date, CreateCellFormat(numberFormatId : NumberFormatId.Date_14) },
                                    { CellFormatIndex.Time, CreateCellFormat(numberFormatId : NumberFormatId.Time_21) },
                                    { CellFormatIndex.ErrorString, CreateCellFormat(numberFormatId : NumberFormatId.String_49, fillId: FillIndex.Error) }
                                };

            return new CellFormats(cellFormats.Values);
        }

        private static CellFormat CreateCellFormat(
                                    NumberFormatId numberFormatId = NumberFormatId.General_0,
                                    Alignment alignment = null,
                                    FontIndex fontId = FontIndex.General,
                                    FillIndex fillId = FillIndex.General,
                                    BorderIndex borderId = BorderIndex.General,
                                    bool applyFont = true,
                                    bool applyBorder = true,
                                    bool applyFill = true,
                                    bool applyNumberFormat = true,
                                    bool applyAlignment = true,
                                    CellStyleFormatIndex formatId = CellStyleFormatIndex.General)
        {
            return new CellFormat
            {
                Alignment = alignment,
                NumberFormatId = (uint)numberFormatId,
                FontId = (uint)fontId,
                FillId = (uint)fillId,
                FormatId = (uint)formatId,
                BorderId = (uint)borderId,
                ApplyFont = applyFont,
                ApplyFill = applyFill,
                ApplyBorder = applyBorder,
                ApplyNumberFormat = applyNumberFormat,
                ApplyAlignment = applyAlignment
            };
        }
        #endregion

        #region Private enums
        private enum FontIndex : uint
        {
            General = 0,
            HeaderFont,
        }

        private enum FillIndex : uint
        {
            General = 0,
            Gray125,
            Header,
            Error
        }

        private enum BorderIndex : uint
        {
            General = 0,
            Bordered
        }

        private enum CellStyleFormatIndex : uint
        {
            General = 0
        }

        private enum NumberFormatId : uint
        {
            General_0 = 0,
            Int_1 = 1,
            Decimal_2 = 2,
            Date_14 = 14,
            Time_21 = 21,
            String_49 = 49,
        }
        #endregion
    }
}