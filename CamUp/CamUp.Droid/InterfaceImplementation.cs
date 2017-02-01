using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using CamUp.Droid;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(ResizeImageImplementation))]
namespace CamUp.Droid
{
    public class ResizeImageImplementation : IResizeImage
    {
        public byte[] ResizeImage(byte[] imageData)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            if (originalImage != null)
            {
                Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int) originalImage.Width/8,
                    (int) originalImage.Height/8, false);
                
                
                using (MemoryStream ms = new MemoryStream())
                {
                    resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                    return ms.ToArray();
                }
            }
            else return null;
        }
    }
}