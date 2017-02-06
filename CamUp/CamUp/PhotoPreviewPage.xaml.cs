using System;
using System.Collections.Generic;
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
        public PhotoPreviewPage(string ImagePath)
        {
            InitializeComponent();
            TapGestureRecognizer galleryTapped = new TapGestureRecognizer() { Command = new Command(new Action(BackClicked)) };
            BackImage.GestureRecognizers.Add(galleryTapped);
            _imagePath = ImagePath;
            PreviewImage.Source = _imagePath;
            ShareButton.Path = _imagePath;
            if(Device.OS == TargetPlatform.iOS) { DeleteButton.BackgroundColor = Color.White; ShareButton.BackgroundColor = Color.White;}
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
