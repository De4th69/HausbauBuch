using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HausbauBuch.Controls;
using HausbauBuch.Views.Home;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace HausbauBuch.Views.Documents
{
    public class DocumentsView : DefaultContentPage
    {
        public DocumentsView()
        {
            Title = "Dokumente";
            var testData = new List<Classes.Documents>
            {
                new Classes.Documents {Name = "Test 1", Size = 12},
                new Classes.Documents {Name = "Test 3", Size = 12387383},
                new Classes.Documents {Name = "Blub", Size = 199},
                new Classes.Documents {Name = "Test 4", Size = 0}
            };

            var documentsListView = new ListView
            {
                HasUnevenRows = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ItemTemplate = new DataTemplate(typeof (DocumentsCell)),
                ItemsSource = testData
            };

            var photoToolbarItem = new ToolbarItem
            {
                Icon = "camera.png",
                Command = new Command(ShowActionSheet)
            };

            ToolbarItems.Add(photoToolbarItem);

            Content = documentsListView;
        }

        private async void ShowActionSheet()
        {
            await CrossMedia.Current.Initialize();

            var answer = await DisplayActionSheet("Bild/Video", "Abbrechen", null, "Bild auswählen", "Bild aufnehmen", "Video auswählen", "Video aufnehmen");

            switch (answer)
            {
                case "Bild auswählen":
                    PickPhoto();
                    break;
                case "Bild aufnehmen":
                    TakePhoto();
                    break;
                case "Video auswählen":
                    PickVideo();
                    break;
                case "Video aufnehmen":
                    TakeVideo();
                    break;
            }
        }

        private async void PickPhoto()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                if (photo != null)
                {

                }
            }
            else
            {
                await DisplayAlert("Fehler", "Kann keine Bilder auswählen", "Ok");
            }
        }

        private async void TakePhoto()
        {
            if (CrossMedia.Current.IsTakePhotoSupported)
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    DefaultCamera = CameraDevice.Rear,
                    Directory = "Photos",
                    Name = "hausbaubuch" + DateTime.Now,
                    PhotoSize = PhotoSize.Full
                });

                if (photo != null)
                {

                }
            }
            else
            {
                await DisplayAlert("Fehler", "Kann keine Bilder aufnehmen", "Ok");
            }
        }

        private async void PickVideo()
        {
            if (CrossMedia.Current.IsPickVideoSupported)
            {
                var video = await CrossMedia.Current.PickVideoAsync();
                if (video != null)
                {
                    
                }
            }
            else
            {
                await DisplayAlert("Fehler", "Kann keine Videos auswählen", "Ok");
            }
        }

        private async void TakeVideo()
        {
            if (CrossMedia.Current.IsTakeVideoSupported)
            {
                var video = await CrossMedia.Current.TakeVideoAsync(new StoreVideoOptions
                {
                    DefaultCamera = CameraDevice.Rear,
                    Directory = "Videos",
                    Name = "hausbaubuch" + DateTime.Now,
                    Quality = VideoQuality.Medium
                });
                if (video != null)
                {
                    
                }
            }
            else
            {
                await DisplayAlert("Fehler", "Kann keine Videos aufnehmen", "Ok");
            }
        }
    }
}
