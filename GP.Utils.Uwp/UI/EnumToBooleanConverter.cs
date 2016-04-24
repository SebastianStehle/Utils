// ==========================================================================
// EnumToBooleanConverter.cs
// Jupiter Presenter App
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using Windows.UI.Xaml.Data;

namespace GP.Utils.UI
{
    /// <summary>
    /// Converts an enum boolean to true, when it is equal to the specified parameter.
    /// </summary>
    /// <typeparam name="T">The type of the enum.</typeparam>
    public class EnumToBooleanConverter<T> : IValueConverter where T : struct
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
            bool result = false;

            if (value is T)
            {
                T parameterValue;

                if (Enum.TryParse(parameter.ToString(), out parameterValue))
                {
                    result = Equals((T)value, parameterValue);
                }
            }

            return result;
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
