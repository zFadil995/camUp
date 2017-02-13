using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;

namespace CamUp
{
    public partial class PhotoPreviewPage : ContentPage
    {
        private string _imagePath;
        public PhotoPreviewPage(IFile Image)
        {
            InitializeComponent();
            ShareButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            TapGestureRecognizer galleryTapped = new TapGestureRecognizer() { Command = new Command(new Action(BackClicked)) };
            BackImage.GestureRecognizers.Add(galleryTapped);
            SetImage(Image);
            if(Device.OS != TargetPlatform.Android) { DeleteButton.BackgroundColor = Color.White; ShareButton.BackgroundColor = Color.White;}
        }

        private async void SetImage(IFile Image)
        {
            Stream stream = await Image.OpenAsync(FileAccess.Read);
            _imagePath = Image.Path;
            PreviewImage.Source = ImageSource.FromStream(() => stream);
            ShareButton.Path = _imagePath;
            ShareButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            IFile imageFile = await PCLStorage.FileSystem.Current.GetFileFromPathAsync(_imagePath);
            if (imageFile != null)
            {
                await imageFile.DeleteAsync();
                ((PhotoPickerPage) Navigation.ModalStack[0]).Updated = false;
            }
            await Navigation.PopModalAsync();
        }
        private void BackClicked()
        {
            Navigation.PopModalAsync();
        }
    }
}
