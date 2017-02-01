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
            _imagePath = ImagePath;
            PreviewImage.Source = _imagePath;
            ShareButton.Path = _imagePath;
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
    }
}
