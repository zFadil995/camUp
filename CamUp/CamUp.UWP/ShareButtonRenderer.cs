using System;
using System.IO;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
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
            RegisterForShare();

            if (Control != null)
            {
                ShareButton share = e.NewElement as ShareButton;
                share.Clicked += ShareClick;
            }
        }

        private async void ShareClick(object sender, EventArgs e)
        {
            _path = ((ShareButton) sender).Path;
            DataTransferManager.ShowShareUI();
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
            request.Data.Properties.Title = "Camup Image";
            request.Data.Properties.Description = "Image captured in CamUp and shared.";

            DataRequestDeferral deferral = request.GetDeferral();
            try
            {
                StorageFile thumbnailFile =
                    await StorageFile.GetFileFromPathAsync(_path);
                request.Data.Properties.Thumbnail =
                    RandomAccessStreamReference.CreateFromFile(thumbnailFile);
                StorageFile imageFile =
                    await StorageFile.GetFileFromPathAsync(_path);
                request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(imageFile));
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}