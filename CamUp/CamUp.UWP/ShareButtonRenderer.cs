using System;
using Windows.ApplicationModel;
using CamUp;
using CamUp.UWP;
using Xamarin.Forms.Platform.UWP;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

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
        private void RegisterForShare()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>(this.ShareImageHandler);
        }

        private async void ShareImageHandler(DataTransferManager sender,
            DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Share Image Example";
            request.Data.Properties.Description = "Demonstrates how to share an image.";

            // Because we are making async calls in the DataRequested event handler,
            //  we need to get the deferral first.
            DataRequestDeferral deferral = request.GetDeferral();

            // Make sure we always call Complete on the deferral.
            try
            {
                StorageFile thumbnailFile =
                    await Package.Current.InstalledLocation.GetFileAsync("Assets\\SmallLogo.png");
                request.Data.Properties.Thumbnail =
                    RandomAccessStreamReference.CreateFromFile(thumbnailFile);
                StorageFile imageFile =
                    await Package.Current.InstalledLocation.GetFileAsync("Assets\\Logo.png");
                request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(imageFile));
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}