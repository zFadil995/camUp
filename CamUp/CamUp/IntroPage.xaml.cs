using System;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace CamUp
{
    public partial class IntroPage : ContentPage
    {
        private MediaFile PhotoFile;
        public IntroPage()
        {
            InitializeComponent();
            TapGestureRecognizer galleryTapped = new TapGestureRecognizer() { Command = new Command(new Action(OnGalleryTapped)) };
            GalleryButton.GestureRecognizers.Add(galleryTapped);
            TapGestureRecognizer cameraTapped = new TapGestureRecognizer() { Command = new Command(new Action(OnCameraTapped))};
            CameraButton.GestureRecognizers.Add(cameraTapped);
        }
        
        private async void OnGalleryTapped()
        {
            GalleryButton.IsEnabled = false;
            await Navigation.PushModalAsync(new PhotoPickerPage() { Title = "Photo Picker Page" });
            GalleryButton.IsEnabled = true;
        }

        private async void OnCameraTapped()
        {
            PhotoFile?.Dispose();
            CameraButton.IsEnabled = false;
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            PhotoFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                DefaultCamera = CameraDevice.Rear,
                PhotoSize = PhotoSize.Medium,
                SaveToAlbum = true,
                Directory = "CamUp",
                Name = "CamUp.jpg",
            });

            if (PhotoFile == null)
                return;

            if (PhotoFile.AlbumPath != null && Device.OS != TargetPlatform.iOS)
            {
                if (PhotoFile.AlbumPath.Contains("/"))
                    Settings.FolderPath = PhotoFile.AlbumPath.Substring(0, PhotoFile.AlbumPath.LastIndexOf('/'));
            }
            else if (PhotoFile.Path.Contains("\\"))
                Settings.FolderPath = PhotoFile.Path.Substring(0, PhotoFile.Path.LastIndexOf('\\'));
            else if (PhotoFile.AlbumPath.Contains("/"))
                Settings.FolderPath = PhotoFile.Path.Substring(0, PhotoFile.Path.LastIndexOf('/'));

            CameraButton.IsEnabled = true;
        }
    }
}
