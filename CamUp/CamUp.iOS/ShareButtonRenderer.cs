using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using CamUp;
using CamUp.Droid;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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
                Control.TouchUpInside += ShareClick;
            }
        }

        private void ShareClick(object sender, EventArgs e)
        {
            Control.Enabled = false;
            if (string.IsNullOrEmpty(_path)) return;
            ImageSource image = _path;
            var item = NSObject.FromObject(image);
            var activityItems = new NSObject[] { item };

            var activityController = new UIActivityViewController(activityItems, null);

            ViewController.PresentViewController(activityController, true, null);

            Control.Enabled = true;
        }
    }
}