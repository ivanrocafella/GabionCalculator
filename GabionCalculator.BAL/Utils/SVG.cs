using GabionCalculator.DAL.Entities;
using GrapeCity.Documents.Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabionCalculator.BAL.Utils
{
    public static class SVG
    {
        public static void Get(Gabion gabion)
        {
            int X_InitCoord = 100; // X origin
            int Y_InitCoord = 200; // Y origin
            int outPartHorSize = 40; // length output part of horizontal size
            int outPartVertSize = 100; // length output part of vertical size
            int outPartVertSizeHalf = 50; // length output part of vertical height size
            int width = 1000; // width of svg
            int height = 1000; // height of svg

            var svgDoc = new GcSvgDocument();
            svgDoc.RootSvg.Width = new SvgLength(width, SvgLengthUnits.Pixels);
            svgDoc.RootSvg.Height = new SvgLength(height, SvgLengthUnits.Pixels);

            List<SvgElement> svgElements = new(); // Make list to fill with objects SvgRectElement


            // ** Draw view from above

            var pbViewAboveGabion = new SvgPathBuilder();
            var pathViewAboveGabion = new SvgPathElement();


            pbViewAboveGabion.AddMoveTo(false, X_InitCoord,
                                   Y_InitCoord);
            pbViewAboveGabion.AddHorizontalLineTo(false, X_InitCoord + gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter)); // side by length top
            pbViewAboveGabion.AddCurveTo(false, X_InitCoord + gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter), // top radius
                Y_InitCoord,
                X_InitCoord + gabion.Length,
                Y_InitCoord,
                X_InitCoord + gabion.Length,
                Y_InitCoord + gabion.BendRadius + gabion.MaterialDiameter);
            pbViewAboveGabion.AddVerticalLineTo(false, Y_InitCoord + gabion.Width - (gabion.BendRadius + gabion.MaterialDiameter)); // side by width right
            pbViewAboveGabion.AddCurveTo(false, X_InitCoord + gabion.Length, // bot radius right
                Y_InitCoord + gabion.Width - (gabion.BendRadius + gabion.MaterialDiameter),
                X_InitCoord + gabion.Length,
                Y_InitCoord + gabion.Width,
                X_InitCoord + gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter),
                Y_InitCoord + gabion.Width);
            pbViewAboveGabion.AddHorizontalLineTo(false, X_InitCoord + gabion.BendRadius + gabion.MaterialDiameter); // side by length bot
            pbViewAboveGabion.AddCurveTo(false, X_InitCoord + gabion.BendRadius + gabion.MaterialDiameter, // bot radius left
               Y_InitCoord + gabion.Width,
               X_InitCoord,
               Y_InitCoord + gabion.Width,
               X_InitCoord,
               Y_InitCoord + gabion.Width - (gabion.BendRadius + gabion.MaterialDiameter));
            pbViewAboveGabion.AddVerticalLineTo(false, Y_InitCoord + 2 * gabion.MaterialDiameter); // side by width left
            pbViewAboveGabion.AddHorizontalLineTo(false, X_InitCoord + gabion.MaterialDiameter); // diam of material at the end
            pbViewAboveGabion.AddVerticalLineTo(false, Y_InitCoord + gabion.Width - (gabion.BendRadius + gabion.MaterialDiameter)); //  side internal by width left
            pbViewAboveGabion.AddCurveTo(false, X_InitCoord + gabion.MaterialDiameter, // bot internal radius left
               Y_InitCoord + gabion.Width - (gabion.BendRadius + gabion.MaterialDiameter),
               X_InitCoord + gabion.MaterialDiameter,
               Y_InitCoord + gabion.Width - gabion.MaterialDiameter,
               X_InitCoord + gabion.BendRadius + gabion.MaterialDiameter,
               Y_InitCoord + gabion.Width - gabion.MaterialDiameter);
            pbViewAboveGabion.AddHorizontalLineTo(false, X_InitCoord + gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter)); // side internal by length bot
            pbViewAboveGabion.AddCurveTo(false, X_InitCoord + gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter), // bot internal radius right
                Y_InitCoord + gabion.Width - gabion.MaterialDiameter,
                X_InitCoord + gabion.Length - gabion.MaterialDiameter,
                Y_InitCoord + gabion.Width - gabion.MaterialDiameter,
                X_InitCoord + gabion.Length - gabion.MaterialDiameter,
                Y_InitCoord + gabion.Width - (gabion.BendRadius + gabion.MaterialDiameter));
            pbViewAboveGabion.AddVerticalLineTo(false, Y_InitCoord + (gabion.BendRadius + gabion.MaterialDiameter)); // side internal by width right
            pbViewAboveGabion.AddCurveTo(false, X_InitCoord + gabion.Length - gabion.MaterialDiameter, // top radius internal
               Y_InitCoord + (gabion.BendRadius + gabion.MaterialDiameter),
               X_InitCoord + gabion.Length - gabion.MaterialDiameter,
               Y_InitCoord + gabion.MaterialDiameter,
               X_InitCoord + gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter),
               Y_InitCoord + gabion.MaterialDiameter);
            pbViewAboveGabion.AddHorizontalLineTo(false, X_InitCoord); // side internal by length top
            pbViewAboveGabion.AddVerticalLineTo(false, Y_InitCoord); // diam of material at the start



            // top display of vertical bars with horizon orientation on the top side

            List<SvgCircleElement> BarsVertHorizOnUp = GetListSvgCircleElements(gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter)
                                                                                        , gabion.CellWidth
                                                                                        , X_InitCoord + gabion.MaterialDiameter / 2
                                                                                        , Y_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2
                                                                                        , gabion.MaterialDiameter / 2
                                                                                        , true);
            foreach (var item in BarsVertHorizOnUp)
                svgElements.Add(item);

            // top display of vertical bars with vertical orientation on the right side

            List<SvgCircleElement> BarsVertVertOnRight = GetListSvgCircleElements(gabion.Width - 2 * (gabion.BendRadius + gabion.MaterialDiameter)
                                                                                        , gabion.CellWidth
                                                                                        , X_InitCoord + gabion.Length - (gabion.MaterialDiameter / 2 + gabion.MaterialDiameter)
                                                                                        , Y_InitCoord + gabion.MaterialDiameter + gabion.BendRadius
                                                                                        , gabion.MaterialDiameter / 2
                                                                                        , false);
            foreach (var item in BarsVertVertOnRight)
                svgElements.Add(item);

            // top display of vertical bars with horizon orientation on the bot side

            List<SvgCircleElement> BarsVertHorizOnBot = GetListSvgCircleElements(gabion.Length - 2 * (gabion.BendRadius + gabion.MaterialDiameter)
                                                                                        , gabion.CellWidth
                                                                                        , X_InitCoord + (gabion.MaterialDiameter + gabion.BendRadius)
                                                                                        , Y_InitCoord + gabion.Width - (gabion.MaterialDiameter / 2 + gabion.MaterialDiameter)
                                                                                        , gabion.MaterialDiameter / 2
                                                                                        , true);
            foreach (var item in BarsVertHorizOnBot)
                svgElements.Add(item);

            // top display of vertical bars with vertical orientation on the left side

            List<SvgCircleElement> BarsVertVertOnLeft = GetListSvgCircleElements(gabion.Width - 2 * gabion.MaterialDiameter - (gabion.BendRadius + gabion.MaterialDiameter)
                                                                                        , gabion.CellWidth
                                                                                        , X_InitCoord + (gabion.MaterialDiameter / 2 + gabion.MaterialDiameter)
                                                                                        , Y_InitCoord + 2 * gabion.MaterialDiameter + gabion.MaterialDiameter / 2
                                                                                        , gabion.MaterialDiameter / 2
                                                                                        , false);
            foreach (var item in BarsVertVertOnLeft)
                svgElements.Add(item);

            // Sizes

            // Draw size of length 

            var vertLineLeftSizeLengthAbove = GetSvgLineElement(X_InitCoord
                                                                , Y_InitCoord
                                                                , X_InitCoord
                                                                , Y_InitCoord - outPartHorSize
                                                                , Color.Black
                                                                , 1f
                                                                , SvgLengthUnits.Pixels);
            svgElements.Add(vertLineLeftSizeLengthAbove);

            var vertLineRightSizeLengthAbove = GetSvgLineElement(X_InitCoord + gabion.Length
                                                             , Y_InitCoord + gabion.BendRadius + gabion.MaterialDiameter
                                                             , X_InitCoord + gabion.Length
                                                             , Y_InitCoord - outPartHorSize
                                                             , Color.Black
                                                             , 1f
                                                             , SvgLengthUnits.Pixels);
            svgElements.Add(vertLineRightSizeLengthAbove);

            var hortLineSizeLengthAbove = GetSvgLineElement(X_InitCoord
                                                             , Y_InitCoord - outPartHorSize + 10
                                                             , X_InitCoord + gabion.Length
                                                             , Y_InitCoord - outPartHorSize + 10
                                                             , Color.Black
                                                             , 1f
                                                             , SvgLengthUnits.Pixels);
            svgElements.Add(hortLineSizeLengthAbove);

            var serifLineLeftSizeLengthAbove = GetSerif(X_InitCoord
                                                        , Y_InitCoord - outPartHorSize + 10
                                                        , X_InitCoord
                                                        , Y_InitCoord - outPartHorSize + 10
                                                        , Color.Black
                                                        , 1f
                                                        , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineLeftSizeLengthAbove);

            var serifLineRightSizeLengthAbove = GetSerif(X_InitCoord + gabion.Length
                                                      , Y_InitCoord - outPartHorSize + 10
                                                      , X_InitCoord + gabion.Length
                                                      , Y_InitCoord - outPartHorSize + 10
                                                      , Color.Black
                                                      , 1f
                                                      , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineRightSizeLengthAbove);

            svgElements.Add(GetSvgTextElement($"{gabion.Length}",
                                 X_InitCoord + gabion.Length / 2 - 10,
                                 Y_InitCoord - outPartHorSize + 8,
                                 0,
                                 SvgLengthUnits.Pixels));    // Make text of size's value length of gabion

            // Draw size of width 

            var horLineUpSizeWidthAbove = GetSvgLineElement(X_InitCoord + gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter)
                                                            , Y_InitCoord
                                                            , X_InitCoord + gabion.Length + outPartVertSizeHalf
                                                            , Y_InitCoord
                                                            , Color.Black
                                                            , 1f
                                                            , SvgLengthUnits.Pixels);
            svgElements.Add(horLineUpSizeWidthAbove);

            var horLineBotSizeWidthAbove = GetSvgLineElement(X_InitCoord + gabion.Length - (gabion.BendRadius + gabion.MaterialDiameter)
                                                          , Y_InitCoord + gabion.Width
                                                          , X_InitCoord + gabion.Length + outPartVertSizeHalf
                                                          , Y_InitCoord + gabion.Width
                                                          , Color.Black
                                                          , 1f
                                                          , SvgLengthUnits.Pixels);
            svgElements.Add(horLineBotSizeWidthAbove);

            var vertLineSizeWidthAbove = GetSvgLineElement(X_InitCoord + gabion.Length + outPartVertSizeHalf - 10
                                                             , Y_InitCoord
                                                             , X_InitCoord + gabion.Length + outPartVertSizeHalf - 10
                                                             , Y_InitCoord + gabion.Width
                                                             , Color.Black
                                                             , 1f
                                                             , SvgLengthUnits.Pixels);
            svgElements.Add(vertLineSizeWidthAbove);

            var serifLineTopSizeWidthAbove = GetSerif(X_InitCoord + gabion.Length + outPartVertSizeHalf - 10
                                                  , Y_InitCoord
                                                  , X_InitCoord + gabion.Length + outPartVertSizeHalf - 10
                                                  , Y_InitCoord
                                                  , Color.Black
                                                  , 1f
                                                  , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineTopSizeWidthAbove);

            var serifLineBotSizeWidthAbove = GetSerif(X_InitCoord + gabion.Length + outPartVertSizeHalf - 10
                                                , Y_InitCoord + gabion.Width
                                                , X_InitCoord + gabion.Length + outPartVertSizeHalf - 10
                                                , Y_InitCoord + gabion.Width
                                                , Color.Black
                                                , 1f
                                                , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineBotSizeWidthAbove);

            svgElements.Add(GetSvgTextElement($"{gabion.Width}",
                                X_InitCoord + gabion.Length + outPartVertSizeHalf - 12,
                                Y_InitCoord + gabion.Width / 2 + 10,
                                -90,
                                SvgLengthUnits.Pixels));    // Make text of size's value length of gabion

            // Draw size of material diameter

            var serifDiameterMaterial = GetSvgLineElement(X_InitCoord + (gabion.MaterialDiameter / 2 + gabion.MaterialDiameter) - gabion.MaterialDiameter / 2
                                                         , Y_InitCoord + 2 * gabion.MaterialDiameter + gabion.MaterialDiameter / 2 - gabion.MaterialDiameter / 2
                                                         , X_InitCoord + (gabion.MaterialDiameter / 2 + gabion.MaterialDiameter) + 50
                                                         , Y_InitCoord + 2 * gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + 50
                                                         , Color.Black
                                                         , 1f
                                                         , SvgLengthUnits.Pixels);
            svgElements.Add(serifDiameterMaterial);

            var horLineDiameterMaterial = GetSvgLineElement(X_InitCoord + (gabion.MaterialDiameter / 2 + gabion.MaterialDiameter) + 50
                                                     , Y_InitCoord + 2 * gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + 50
                                                     , X_InitCoord + (gabion.MaterialDiameter / 2 + gabion.MaterialDiameter) + 105
                                                     , Y_InitCoord + 2 * gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + 50
                                                     , Color.Black
                                                     , 1f
                                                     , SvgLengthUnits.Pixels);
            svgElements.Add(horLineDiameterMaterial);

            svgElements.Add(GetSvgTextElement($"⌀{gabion.MaterialDiameter}",
                                X_InitCoord + (gabion.MaterialDiameter / 2 + gabion.MaterialDiameter) + 50,
                                Y_InitCoord + 2 * gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + 48,
                                0,
                                SvgLengthUnits.Pixels));    // Make text of size's value length of gabion

            // ** Draw view from front

            // front display of vertical bars by length

            List<SvgRectElement> BarsVerticalFrontViewElements = GetListSvgRectElements(gabion.Length - 3 * gabion.MaterialDiameter
                                                                                        , gabion.CellWidth
                                                                                        , X_InitCoord + gabion.MaterialDiameter
                                                                                        , 2 * Y_InitCoord + gabion.Width
                                                                                        , gabion.MaterialDiameter
                                                                                        , gabion.CardHeight
                                                                                        , false);
            foreach (var item in BarsVerticalFrontViewElements)
                svgElements.Add(item);

            var BarVerticalFrontViewLastElement = GetSvgRectElement(X_InitCoord + gabion.Length - 2 * gabion.MaterialDiameter
                                                                    , 2 * Y_InitCoord + gabion.Width
                                                                    , gabion.MaterialDiameter
                                                                    , gabion.CardHeight
                                                                    , Color.Transparent
                                                                    , Color.Black
                                                                    , 1.5f
                                                                    , SvgLengthUnits.Pixels);
            svgElements.Add(BarVerticalFrontViewLastElement);

            // front display of horizontal bars by height

            List<SvgRectElement> BarsHorizonFrontViewElements = GetListSvgRectElements(gabion.Height - gabion.CellHeight + gabion.MaterialDiameter
                                                                                       , gabion.CellHeight
                                                                                       , X_InitCoord
                                                                                       , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert - gabion.MaterialDiameter / 2
                                                                                       , gabion.MaterialDiameter
                                                                                       , gabion.Length
                                                                                       , true);
            foreach (var item in BarsHorizonFrontViewElements)
                svgElements.Add(item);

            var BarHorizonFrontViewLastElement = GetSvgRectElement(X_InitCoord
                                                                    , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert - gabion.MaterialDiameter + gabion.MaterialDiameter / 2
                                                                    , gabion.Length
                                                                    , gabion.MaterialDiameter
                                                                    , Color.Transparent
                                                                    , Color.Black
                                                                    , 1.5f
                                                                    , SvgLengthUnits.Pixels);
            svgElements.Add(BarHorizonFrontViewLastElement);

            // Sizes

            // Draw size of card height 

            var horLineUpSizeHeightCard = GetSvgLineElement(X_InitCoord + gabion.Length - gabion.MaterialDiameter
                                                            , 2 * Y_InitCoord + gabion.Width
                                                            , X_InitCoord + gabion.Length + outPartVertSize + 10
                                                            , 2 * Y_InitCoord + gabion.Width
                                                            , Color.Black
                                                            , 1f
                                                            , SvgLengthUnits.Pixels);
            svgElements.Add(horLineUpSizeHeightCard);

            var horLineBotSizeHeightCard = GetSvgLineElement(X_InitCoord + gabion.Length - gabion.MaterialDiameter
                                                            , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight
                                                            , X_InitCoord + gabion.Length + outPartVertSize + 10
                                                            , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight
                                                            , Color.Black
                                                            , 1f
                                                            , SvgLengthUnits.Pixels);
            svgElements.Add(horLineBotSizeHeightCard);

            var vertLineSizeHeightCard = GetSvgLineElement(X_InitCoord + gabion.Length + outPartVertSize
                                                             , 2 * Y_InitCoord + gabion.Width
                                                             , X_InitCoord + gabion.Length + outPartVertSize
                                                             , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight
                                                             , Color.Black
                                                             , 1f
                                                             , SvgLengthUnits.Pixels);
            svgElements.Add(vertLineSizeHeightCard);

            var serifLineTopSizeHeightCard = GetSerif(X_InitCoord + gabion.Length + outPartVertSize
                                                  , 2 * Y_InitCoord + gabion.Width
                                                  , X_InitCoord + gabion.Length + outPartVertSize
                                                  , 2 * Y_InitCoord + gabion.Width
                                                  , Color.Black
                                                  , 1f
                                                  , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineTopSizeHeightCard);

            var serifLineBotSizeHeightCard = GetSerif(X_InitCoord + gabion.Length + outPartVertSize
                                                  , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight
                                                  , X_InitCoord + gabion.Length + outPartVertSize
                                                  , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight
                                                  , Color.Black
                                                  , 1f
                                                  , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineBotSizeHeightCard);

            svgElements.Add(GetSvgTextElement($"{gabion.CardHeight}",
                                X_InitCoord + gabion.Length + outPartVertSize - 2,
                                2 * Y_InitCoord + gabion.Width + gabion.CardHeight / 2 + 20,
                                -90,
                                SvgLengthUnits.Pixels));    // Make text of size's value height card of gabion

            // Draw size of height 

            var horLineUpSizeHeight = GetSvgLineElement(X_InitCoord + gabion.Length - gabion.MaterialDiameter
                                                            , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert
                                                            , X_InitCoord + gabion.Length + outPartVertSizeHalf + 10
                                                            , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert
                                                            , Color.Black
                                                            , 1f
                                                            , SvgLengthUnits.Pixels);
            svgElements.Add(horLineUpSizeHeight);

            var horLineBotSizeHeight = GetSvgLineElement(X_InitCoord + gabion.Length - gabion.MaterialDiameter
                                                          , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert + gabion.Height
                                                          , X_InitCoord + gabion.Length + outPartVertSizeHalf + 10
                                                          , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert + gabion.Height
                                                          , Color.Black
                                                          , 1f
                                                          , SvgLengthUnits.Pixels);
            svgElements.Add(horLineBotSizeHeight);

            var vertLineSizeHeight = GetSvgLineElement(X_InitCoord + gabion.Length + outPartVertSizeHalf
                                                         , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert
                                                         , X_InitCoord + gabion.Length + outPartVertSizeHalf
                                                         , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert + gabion.Height
                                                         , Color.Black
                                                         , 1f
                                                         , SvgLengthUnits.Pixels);
            svgElements.Add(vertLineSizeHeight);

            var serifLineTopSizeHeight = GetSerif(X_InitCoord + gabion.Length + outPartVertSizeHalf
                                              , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert
                                              , X_InitCoord + gabion.Length + outPartVertSizeHalf
                                              , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert
                                              , Color.Black
                                              , 1f
                                              , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineTopSizeHeight);

            var serifLineBotSizeHeight = GetSerif(X_InitCoord + gabion.Length + outPartVertSizeHalf
                                          , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert + gabion.Height
                                          , X_InitCoord + gabion.Length + outPartVertSizeHalf
                                          , 2 * Y_InitCoord + gabion.Width + gabion.OutletVert + gabion.Height
                                          , Color.Black
                                          , 1f
                                          , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineBotSizeHeight);

            svgElements.Add(GetSvgTextElement($"{gabion.Height}",
                              X_InitCoord + gabion.Length + outPartVertSizeHalf - 2,
                              2 * Y_InitCoord + gabion.Width + gabion.CardHeight / 2 + 20,
                              -90,
                              SvgLengthUnits.Pixels));    // Make text of size's value height of gabion

            // Draw size of cell height

            var horLineUpSizeCellHeight = GetSvgLineElement(X_InitCoord
                                                           , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert - gabion.CellHeight
                                                           , X_InitCoord - outPartVertSizeHalf
                                                           , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert - gabion.CellHeight
                                                           , Color.Black
                                                           , 1f
                                                           , SvgLengthUnits.Pixels);
            svgElements.Add(horLineUpSizeCellHeight);

            var horLineBotSizeCellHeight = GetSvgLineElement(X_InitCoord
                                                       , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert
                                                       , X_InitCoord - outPartVertSizeHalf
                                                       , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert
                                                       , Color.Black
                                                       , 1f
                                                       , SvgLengthUnits.Pixels);
            svgElements.Add(horLineBotSizeCellHeight);

            var vertLineSizeCellHeight = GetSvgLineElement(X_InitCoord - outPartVertSizeHalf + 10
                                                   , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert - gabion.CellHeight
                                                   , X_InitCoord - outPartVertSizeHalf + 10
                                                   , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert
                                                   , Color.Black
                                                   , 1f
                                                   , SvgLengthUnits.Pixels);
            svgElements.Add(vertLineSizeCellHeight);

            var serifTopSizeCellHeight = GetSerif(X_InitCoord - outPartVertSizeHalf + 10
                                                    , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert - gabion.CellHeight
                                                    , X_InitCoord - outPartVertSizeHalf + 10
                                                    , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert - gabion.CellHeight
                                                    , Color.Black
                                                    , 1f
                                                    , SvgLengthUnits.Pixels);
            svgElements.Add(serifTopSizeCellHeight);

            var serifBotSizeCellHeight = GetSerif(X_InitCoord - outPartVertSizeHalf + 10
                                                      , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert
                                                      , X_InitCoord - outPartVertSizeHalf + 10
                                                      , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert
                                                      , Color.Black
                                                      , 1f
                                                      , SvgLengthUnits.Pixels);
            svgElements.Add(serifBotSizeCellHeight);

            svgElements.Add(GetSvgTextElement($"{gabion.CellHeight}",
                          X_InitCoord - outPartVertSizeHalf + 8,
                          2 * Y_InitCoord + gabion.Width + gabion.CardHeight - gabion.OutletVert - gabion.CellHeight / 2 + 20,
                          -90,
                          SvgLengthUnits.Pixels));    // Make text of size's value cell height of gabion

            // Draw size of cell width

            var vertLineRightSizeCellWidth = GetSvgLineElement(X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2
                                                           , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight
                                                           , X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2
                                                           , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize
                                                           , Color.Black
                                                           , 1f
                                                           , SvgLengthUnits.Pixels);
            svgElements.Add(vertLineRightSizeCellWidth);

            var vertLineLeftSizeCellWidth = GetSvgLineElement(X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + gabion.CellWidth
                                                       , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight
                                                       , X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + gabion.CellWidth
                                                       , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize
                                                       , Color.Black
                                                       , 1f
                                                       , SvgLengthUnits.Pixels);
            svgElements.Add(vertLineLeftSizeCellWidth);

            var horLineSizeCellWidth = GetSvgLineElement(X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2
                                                      , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize - 10
                                                      , X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + gabion.CellWidth
                                                      , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize - 10
                                                      , Color.Black
                                                      , 1f
                                                      , SvgLengthUnits.Pixels);
            svgElements.Add(horLineSizeCellWidth);

            var serifLineLeftSizeCellWidth = GetSerif(X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2
                                                   , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize - 10
                                                   , X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2
                                                   , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize - 10
                                                   , Color.Black
                                                   , 1f
                                                   , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineLeftSizeCellWidth);

            var serifLineRightSizeCellWidth = GetSerif(X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + gabion.CellWidth
                                                      , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize - 10
                                                      , X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + gabion.CellWidth
                                                      , 2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize - 10
                                                      , Color.Black
                                                      , 1f
                                                      , SvgLengthUnits.Pixels);
            svgElements.Add(serifLineRightSizeCellWidth);

            svgElements.Add(GetSvgTextElement($"{gabion.CellWidth}",
                         X_InitCoord + gabion.MaterialDiameter + gabion.MaterialDiameter / 2 + gabion.CellWidth / 2 - 10,
                         2 * Y_InitCoord + gabion.Width + gabion.CardHeight + outPartHorSize - 12,
                         0,
                         SvgLengthUnits.Pixels));    // Make text of size's value cell width of gabion





            pathViewAboveGabion.PathData = pbViewAboveGabion.ToPathData();
            pathViewAboveGabion.Fill = new SvgPaint(Color.Transparent);
            pathViewAboveGabion.Stroke = new SvgPaint(Color.Black);
            pathViewAboveGabion.StrokeWidth = new SvgLength(1.5f);

            svgElements.Add(pathViewAboveGabion);


            for (int i = 0; i < svgElements.Count; i++)
                svgDoc.RootSvg.Children.Insert(i, svgElements[i]);

            SvgViewBox view = new()
            {
                MinX = 0,
                MinY = 0,
                Width = width * 1.5f,
                Height = height * 3.5f
            };
                
            svgDoc.RootSvg.ViewBox = view;
         //   svgDoc.RootSvg.Width = new SvgLength(100, SvgLengthUnits.Percentage);
         //   svgDoc.RootSvg.Height = new SvgLength(100, SvgLengthUnits.Percentage);

            StringBuilder stringBuilder = new();
            svgDoc.Save(stringBuilder);
            string xml = stringBuilder.ToString();
            string svgElem = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            gabion.Svg = svgElem;
        }

        static SvgTextElement GetSvgTextElement(string content, float x, float y, float angle, SvgLengthUnits units)
        {
            var textSizeThreadLength = new SvgContentElement
            {
                Content = $"{content}",
                Stroke = new SvgPaint(Color.Black),
                Color = new SvgColor(Color.Black),
            };

            var coordsXSizeThreadLength = new List<SvgLength>
            {
                new SvgLength(x, units)
            };
            var coordsYSizeThreadLength = new List<SvgLength>
            {
                new SvgLength(y, units)
            };

            var transforms = new List<SvgTransform>();
            var svgRotateTrans = new SvgRotateTransform()
            {
                Angle = new SvgAngle(angle),
                CenterX = new SvgLength(x),
                CenterY = new SvgLength(y),
            };
            transforms.Add(svgRotateTrans);

            SvgTextElement svgTextElement = new()
            {
                X = coordsXSizeThreadLength,
                Y = coordsYSizeThreadLength,
                Color = new SvgColor(Color.Black),
                FontStyle = SvgFontStyle.Normal,
                FontSize = new SvgLength(45, units),
                FontWeight = SvgFontWeight.Bold,
                Transform = transforms,
                TextOrientation = SvgTextOrientation.Mixed
            };

            svgTextElement.Children.Insert(0, textSizeThreadLength);

            return svgTextElement;
        }

        static SvgLineElement GetSvgLineElement(float x1, float y1, float x2, float y2, Color color, float width, SvgLengthUnits units)
        {
            var svgLineElement = new SvgLineElement
            {
                X1 = new SvgLength(x1, units),
                Y1 = new SvgLength(y1, units),
                X2 = new SvgLength(x2, units),
                Y2 = new SvgLength(y2, units),
                Stroke = new SvgPaint(color),
                StrokeWidth = new SvgLength(width, units)
            };
            return svgLineElement;
        }

        static SvgRectElement GetSvgRectElement(float x, float y, float width, float height, Color colorOfFill, Color colorOfStroke, float strokeWidth, SvgLengthUnits units)
        {
            var svgRectElement = new SvgRectElement
            {
                X = new SvgLength(x, units),
                Y = new SvgLength(y, units),
                Width = new SvgLength(width, units),
                Height = new SvgLength(height, units),
                Fill = new SvgPaint(colorOfFill),
                Stroke = new SvgPaint(colorOfStroke),
                StrokeWidth = new SvgLength(strokeWidth, units)
            };

            return svgRectElement;
        }

        static SvgLineElement GetSerif(float x1, float y1, float x2, float y2, Color color, float width, SvgLengthUnits units)
        {
            var svgLineElement = new SvgLineElement
            {
                X1 = new SvgLength(x1 - 3, units),
                Y1 = new SvgLength(y1 + 3, units),
                X2 = new SvgLength(x2 + 3, units),
                Y2 = new SvgLength(y2 - 3, units),
                Stroke = new SvgPaint(color),
                StrokeWidth = new SvgLength(width, units)
            };
            return svgLineElement;
        }

        static SvgLineElement GetSerifRad(float x1, float y1, float x2, float y2, Color color, float width, SvgLengthUnits units)
        {
            var svgLineElement = new SvgLineElement
            {
                X1 = new SvgLength(x1 - 2, units),
                Y1 = new SvgLength(y1 + 5, units),
                X2 = new SvgLength(x2 + 2, units),
                Y2 = new SvgLength(y2 - 5, units),
                Stroke = new SvgPaint(color),
                StrokeWidth = new SvgLength(width, units)
            };
            return svgLineElement;
        }


        static SvgCircleElement GetSvgCircleElement(float x, float y, float radius, Color colorOfFill, Color colorOfStroke, float strokeWidth, SvgLengthUnits units)
        {
            var svgCircleElement = new SvgCircleElement()
            {
                CenterX = new SvgLength(x, units),
                CenterY = new SvgLength(y, units),
                Radius = new SvgLength(radius, units),
                Fill = new SvgPaint(colorOfFill),
                Stroke = new SvgPaint(colorOfStroke),
                StrokeWidth = new SvgLength(strokeWidth, units)
            };

            return svgCircleElement;
        }

        static List<SvgCircleElement> GetListSvgCircleElements(float pathLength, int step, float xStart, float yStart, float radius, bool orientationIsHorizon)
        {
            // if orientation is true then the orientation is horizon othervise vertical
            List<SvgCircleElement> svgCircleElements = new();
            _ = new SvgCircleElement();
            int qtyStep = (int)Math.Floor(pathLength / step);
            for (int i = 0; i <= qtyStep * step; i += step)
            {
                SvgCircleElement svgCircleElement;
                if (orientationIsHorizon) // horizon 
                {
                    svgCircleElement = GetSvgCircleElement(xStart + i,
                        yStart,
                        radius,
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);
                }
                else // vertical
                {
                    svgCircleElement = GetSvgCircleElement(xStart,
                       yStart + i,
                       radius,
                       Color.Transparent,
                       Color.Black,
                       1.5f,
                       SvgLengthUnits.Pixels);
                }
                svgCircleElements.Add(svgCircleElement);
            }
            return svgCircleElements;
        }

        static List<SvgRectElement> GetListSvgRectElements(float pathLength, int step, float xStart, float yStart, float width, float length, bool orientationIsHorizon)
        {
            // if orientation is true then the orientation is horizon othervise vertical
            List<SvgRectElement> svgRectElements = new List<SvgRectElement>();
            var svgRectElement = new SvgRectElement();
            int qtyStep = (int)Math.Floor(pathLength / step);
            for (int i = 0; i <= qtyStep * step; i += step)
            {
                if (orientationIsHorizon) // horizon 
                {
                    svgRectElement = GetSvgRectElement(xStart,
                        yStart + i,
                        length,
                        width,
                        Color.Transparent,
                        Color.Black,
                        1.5f,
                        SvgLengthUnits.Pixels);
                }
                else // vertical
                {
                    svgRectElement = GetSvgRectElement(xStart + i,
                       yStart,
                       width,
                       length,
                       Color.Transparent,
                       Color.Black,
                       1.5f,
                       SvgLengthUnits.Pixels);
                }
                svgRectElements.Add(svgRectElement);
            }
            return svgRectElements;
        }
    }
}
