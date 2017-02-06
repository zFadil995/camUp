using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PCLStorage;
using Xamarin.Forms;

namespace CamUp
{
    public partial class PhotoPickerPage : ContentPage
    {
        public bool Updated = false;
        public PhotoPickerPage()
        {
            InitializeComponent();
            TapGestureRecognizer galleryTapped = new TapGestureRecognizer() { Command = new Command(new Action(BackClicked)) };
            BackImage.GestureRecognizers.Add(galleryTapped);
            if (Device.OS == TargetPlatform.iOS)
            {
                pickerPageLayout.Margin = new Thickness(0, 20, 0, 0);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!Updated)
            {
                ImageGrid.Children.Clear();
                ImageGrid.ColumnDefinitions.Clear();
                ImageGrid.RowDefinitions.Clear();
                ImageGrid.ColumnDefinitions.Add(new ColumnDefinition());
                ImageGrid.ColumnDefinitions.Add(new ColumnDefinition());
                ImageGrid.ColumnDefinitions.Add(new ColumnDefinition());
                ImageGrid.ColumnDefinitions.Add(new ColumnDefinition());
                setFolder();
                Updated = true;
            }
        }

        private async void setFolder()
        {
            if (Settings.FolderPath != "")
            {
                IFolder photoFolder = await FileSystem.Current.GetFolderFromPathAsync(Settings.FolderPath);
                if (photoFolder != null)
                {
                    IList<IFile> photoFiles = await photoFolder.GetFilesAsync();
                    int column = 4, row = -1;
                    foreach (IFile photoFile in photoFiles)
                    {
                        byte[] imageData;

                        Stream stream = await photoFile.OpenAsync(FileAccess.Read);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            imageData = ms.ToArray();
                        }

                        byte[] resizedImage = DependencyService.Get<IResizeImage>().ResizeImage(imageData);

                        if (resizedImage != null)
                        {
                            if (column == 4)
                            {
                                ImageGrid.RowDefinitions.Add(new RowDefinition() { Height = ImageGrid.Width / 4 });
                                column = 0;
                                row++;
                            }
                            StackLayout imageLayout = new StackLayout()
                            {
                                BackgroundColor = Color.FromHex("#EE0000"),
                                Children =
                                {
                                    new Image
                                    {
                                        Source = ImageSource.FromStream(() => new MemoryStream(resizedImage)),
                                        Margin = 5,
                                        VerticalOptions = LayoutOptions.CenterAndExpand,
                                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                                        GestureRecognizers = { new TapGestureRecognizer() {Command = new Command(new Action(() => Navigation.PushModalAsync(new PhotoPreviewPage(photoFile.Path))))} }
                                    }
                                }
                            };
                            ImageGrid.Children.Add(imageLayout, column, row);
                            column++;
                        }
                    }
                }
            }
        }

        private void BackClicked()
        {
            Navigation.PopModalAsync();
        }
    }
}
