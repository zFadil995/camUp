using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using CamUp.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(ResizeImageImplementation))]
namespace CamUp.UWP
{
    public class ResizeImageImplementation : IResizeImage
    {
        public byte[] ResizeImage(byte[] imageData)
        {
            /*byte[] resizedData;

            using (var streamIn = new MemoryStream(imageData))
            {
                using (var imageStream = streamIn.AsRandomAccessStream())
                {
                    var decoder = BitmapDecoder.CreateAsync(imageStream);
                    var resizedStream = new InMemoryRandomAccessStream();
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);
                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;
                    encoder.BitmapTransform.ScaledHeight = (uint)height;
                    encoder.BitmapTransform.ScaledWidth = (uint)width;
                    encoder.FlushAsync();
                    resizedStream.Seek(0);
                    resizedData = new byte[resizedStream.Size];
                    resizedStream.ReadAsync(resizedData.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                }
            }*/

            return imageData;
        }
        
    }
}