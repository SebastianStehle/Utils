// ==========================================================================
// ByteArrayToImageConverter.cs
// Jupiter Presenter App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

// ReSharper disable SuggestBaseTypeForParameter

namespace GP.Utils.UI
{
    /// <summary>
    /// Converts an byte array to an image.
    /// </summary>
    public sealed class ByteArrayToImageConverter : IValueConverter
    {
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapImage bitmapImage = null;

            var buffer = value as byte[];

            if (buffer != null)
            {
                bitmapImage = new BitmapImage();

                SetImageSourceAsync(bitmapImage, buffer).Forget();
            }

            return bitmapImage;
        }

        private static async Task SetImageSourceAsync(BitmapSource bitmapImage, byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                await bitmapImage.SetSourceAsync(stream.AsRandomAccessStream());
            }
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.
        /// This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="language">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
