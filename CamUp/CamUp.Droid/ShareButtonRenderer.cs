using System;
using System.IO;
using Android.Content;
using CamUp;
using CamUp.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;

[assembly: ExportRenderer(typeof(ShareButton), typeof(ShareButtonRenderer))]
namespace CamUp.Droid
{
    class ShareButtonRenderer : ButtonRenderer
    {
        private string _path;
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                ShareButton share = e.NewElement as ShareButton;
                _path = share.Path;
                Control.Click += ShareClick;
            }
        }

        private void ShareClick(object sender, EventArgs e)
        {
            Control.Enabled = false;
            if (string.IsNullOrEmpty(_path)) return;

            var imageUri = Android.Net.Uri.Parse($"file://" + _path);
            var sharingIntent = new Intent();
            sharingIntent.SetAction(Intent.ActionSend);
            sharingIntent.SetType("image/*");
            sharingIntent.PutExtra(Intent.ExtraText, "CamUp Description");
            sharingIntent.PutExtra(Intent.ExtraStream, imageUri);
            sharingIntent.AddFlags(ActivityFlags.GrantReadUriPermission);
            Context.StartActivity(Intent.CreateChooser(sharingIntent, "Cam Up"));
            Control.Enabled = true;
        }
    }
}