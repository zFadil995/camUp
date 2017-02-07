using System;
using Windows.UI.Xaml.Controls;
using CamUp;
using CamUp.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ShareButton), typeof(ShareButtonRenderer))]
namespace CamUp.UWP
{
    public class ShareButtonRenderer : ButtonRenderer
    {
        private string _path;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                ShareButton share = e.NewElement as ShareButton;
                _path = share.Path;
                share.Clicked += ShareClick;
            }
        }

        private void ShareClick(object sender, EventArgs e)
        {
            ((ShareButton) sender).Text = _path;
        }
    }
}