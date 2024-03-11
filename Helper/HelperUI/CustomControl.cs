using MaterialDesignThemes.Wpf;
using MosqueMate.Pages;
using MosqueMateServices.Enums;
using MosqueMateServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Application = System.Windows.Application;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Control = System.Windows.Controls.Control;
using FlowDirection = System.Windows.FlowDirection;
using FontFamily = System.Windows.Media.FontFamily;
namespace MosqueMate.Helper.HelperUI
{
    public class CustomControl
    {
        private static IHadith hadith;
        public static T ChangeBackForeColors<T>(T originalControl, string backColorCode, string foreColorCode) where T : Control
        {
            Color customBackColor = (Color)ColorConverter.ConvertFromString(backColorCode);
            Color customForeColorCode = (Color)ColorConverter.ConvertFromString(foreColorCode);
            originalControl.Background = new SolidColorBrush(customBackColor);
            originalControl.Foreground = new SolidColorBrush(customForeColorCode);
            return originalControl;
        }
        public static Point GetOffsetChildOfElement<T>(Panel  panel, string childName) where T : ContentControl    
        {
            var point  = new Point();   
            var result = panel.Children.OfType<T>().Where(x => x.Content.ToString().Contains(childName)).FirstOrDefault();
            if(result != null)
            {
                point = result.TranslatePoint(new Point(0, 0), panel);
            }
            else
            {
                point = new Point(10,10);
            }
            return point;

        }
        #region RightToLeftControl
        public static void RightToLeft(Page page)
        {
            page.FlowDirection = FlowDirection.RightToLeft;
        }
        public static void RightToLeft(Window page)
        {
            page.FlowDirection = FlowDirection.RightToLeft;
        }
        public static void LeftToRight(Page page)
        {
            page.FlowDirection = FlowDirection.LeftToRight;
        }
        public static void LeftToRight(Window page)
        {
            page.FlowDirection = FlowDirection.LeftToRight;
        }
        public static void RightToLeft(Control control)
        {
            control.FlowDirection = FlowDirection.RightToLeft;
        }
        public static void LeftToRight(Control control)
        {
            control.FlowDirection = FlowDirection.LeftToRight;
        }
        #endregion
        public static void SetAppFont(Window window)
        {
            var cairoFont = (FontFamily)Application.Current.Resources["CairoBalck"];
            window.FontFamily = cairoFont;
        }
        public static void SetAppFont(UserControl userControl)
        {
            var cairoFont = (FontFamily)Application.Current.Resources["CairoBalck"];
            userControl.FontFamily = cairoFont;
        }
        public static void GenerateMaterialCard(Panel panel, List<string> allQuran, RepositoriesTypes types)
        {
            int marginCount = 120;
            #region Draw_Card
            List<Card> cards = new List<Card>();
            for (int i = 0; i < allQuran.Count; i++)
            {
                Color customBackColor = (Color)ColorConverter.ConvertFromString("#FF4774A5");
                #region Card_Properties

                var card = new Card
                {
                    Name = $"card_soura_{i + 1}",
                    Background = new SolidColorBrush(customBackColor),
                    Foreground = Brushes.White,
                    Content = allQuran[i],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = 700,
                    Height = 120,
                    FontSize = 20,
                    Padding = new Thickness(10, 10, 10, 10),
                    Margin = new Thickness(0, marginCount, 0, 0),
                    Cursor = Cursors.Hand,
                };
                RectangleGeometry clipGeometry = new RectangleGeometry(new Rect(0, 0, card.Width, card.Height));
                clipGeometry.RadiusX = 20;
                clipGeometry.RadiusY = 20;
                // Add shadow effect to the card
                //card.Effect = new DropShadowEffect
                //{
                //    ShadowDepth = 5,
                //    BlurRadius = 5,
                //    Opacity = 0.5,
                //    Color = Colors.AliceBlue
                //};
                card.Clip = clipGeometry;   
                marginCount += 150;
                cards.Add(card);
                panel.Children.Add(card);
                #endregion
                card.MouseLeftButtonDown += (sender, e) => Card_MouseLeftButtonDown(sender, e, types);

            }

            #endregion

        }

        private static void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, RepositoriesTypes types)
        {
            var selectedCard = sender as Card;
            switch (types)
            {
                case RepositoriesTypes.Azkar:
                    if (selectedCard != null)
                    {
                        new AdhkarsTemplate(selectedCard.Content.ToString()).ShowDialog();
                    }
                    break;
                case RepositoriesTypes.Quran:
                    if (selectedCard != null)
                    {
                        var souraID = selectedCard.Name.Split('_')[2];
                        new QuranTemplate(Convert.ToInt32(souraID)).ShowDialog();
                    }
                    break;
                case RepositoriesTypes.Hadith:
                    if (selectedCard != null)
                    {
                        var souraID = selectedCard.Name.Split('_')[2];
                        new HdithTemplate(Convert.ToInt32(souraID)).ShowDialog();
                    }
                    break;

            }


        }
    }
}
