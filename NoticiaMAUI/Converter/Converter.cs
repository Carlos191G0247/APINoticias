using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace NoticiaMAUI.Converter
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64String)
            {
                // Eliminar espacios en blanco adicionales si es necesario
                base64String = base64String.Trim();

                // Verificar si la cadena es válida antes de convertirla
                if (base64String.Length % 4 == 0 && Regex.IsMatch(base64String, @"^[a-zA-Z0-9+/]*={0,2}$"))
                {
                    byte[] imageBytes = System.Convert.FromBase64String(base64String);
                    ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    return imageSource;
                }
                else
                {
                    // La cadena Base64 no es válida
                    // Maneja el error o devuelve un valor predeterminado
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


