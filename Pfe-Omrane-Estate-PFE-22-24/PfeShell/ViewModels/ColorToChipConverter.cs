using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PfeShell.ViewModels
{
    public class ColorToChipConverter : IValueConverter
    {
        #region Member

        SfChip selectedChip = null;

        #endregion

        #region Convert
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<SfChip> colorChips = new ObservableCollection<SfChip>();
            foreach (var item in value as ObservableCollection<Color>)
            {

                var colorChip = new SfChip() { BackgroundColor = (Color)item, ShowSelectionIndicator = true, SelectionIndicatorColor = Color.Transparent, CornerRadius = 20, WidthRequest = 40, HeightRequest = 40, Margin = 10, BorderWidth = 1 };
                colorChip.BorderColor = Color.FromRgb(-(colorChip.BackgroundColor.R - 1), -(colorChip.BackgroundColor.G - 1), -(colorChip.BackgroundColor.B - 1));
                var mean = (colorChip.BackgroundColor.R + colorChip.BackgroundColor.G + colorChip.BackgroundColor.B) / 3;
                colorChip.BorderColor = mean < 0.5 ? Color.White : Color.White;

                colorChip.Clicked += ColorChip_Clicked;
                colorChips.Add(colorChip);

            }
            return colorChips;
        }
        #endregion

        #region Event

        private void ColorChip_Clicked(object sender, EventArgs e)
        {
            if (selectedChip != null)
            {
                selectedChip.ShowSelectionIndicator = false;
                selectedChip.BorderWidth = 1;
            }

            selectedChip = (sender as SfChip);
            (selectedChip.Parent as FlexLayout).BackgroundColor = selectedChip.BackgroundColor;

            selectedChip.ShowSelectionIndicator = true;
            selectedChip.SelectionIndicatorColor = selectedChip.BorderColor;
            selectedChip.BorderWidth = 3;
        }
        #endregion
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }


}
