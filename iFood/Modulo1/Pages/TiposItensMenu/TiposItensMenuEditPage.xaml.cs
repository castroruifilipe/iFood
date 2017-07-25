using System;
using System.Collections.Generic;
using System.IO;
using Modulo1.Dal;
using Modulo1.Models;
using PCLStorage;
using Plugin.Media;
using Xamarin.Forms;

namespace Modulo1.Pages.TiposItensMenu {
    
    public partial class TiposItensMenuEditPage : ContentPage {


        private TipoItemMenu tipoItemMenu;
        private byte[] bytesFoto;
        private TipoItemMenuDAL dalTiposItensMenu = new TipoItemMenuDAL();


        public TiposItensMenuEditPage(TipoItemMenu tipoItemMenu) {
			InitializeComponent();
            PopularFormulario(tipoItemMenu);
			RegistaClickBotaoCamera(idtipoitemmenu.Text.Trim());
			RegistaClickBotaoAlbum();
		}

        private void PopularFormulario(TipoItemMenu tipoItemMenu) {
            this.tipoItemMenu = tipoItemMenu;
            idtipoitemmenu.Text = tipoItemMenu.TipoItemMenuId.ToString();
            nome.Text = tipoItemMenu.Nome;
            bytesFoto = tipoItemMenu.Foto;
		}

		private void RegistaClickBotaoCamera(string idparafoto) {
            BtnCamera.Clicked += async (sender, e) => {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                    DisplayAlert("Não existe câmera", "A câmera não está disponível", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() {
                    Directory = FileSystem.Current.LocalStorage.Name,
                    Name = "tipoitem_" + idparafoto + ".jpg"
                });
                if (file == null) {
                    return;
                }

                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                fototipoitemmenu.Source = ImageSource.FromStream(() => {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });
                bytesFoto = memoryStream.ToArray();
            };
        }

		private void RegistaClickBotaoAlbum() {
            BtnAlbum.Clicked += async (sender, e) => {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported) {
                    DisplayAlert("Álbum não suportado", "Não existe permissão para aceder o albúm de fotos", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null) {
                    return;
                }

                var stream = file.GetStream();
                var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);

                fototipoitemmenu.Source = ImageSource.FromStream(() => {
                    var s = file.GetStream();
                    file.Dispose();
                    return s;
                });
                bytesFoto = memoryStream.ToArray();
            };
        }

        public async void BtnGravarClick(object sender, EventArgs e) {
			if (nome.Text.Trim() == string.Empty) {
				this.DisplayAlert("Erro", "Você precisa informar o nome para o novo tipo de item do menu.", "OK");
			} else {
                this.tipoItemMenu.Nome = nome.Text;
                this.tipoItemMenu.Foto = bytesFoto;
                dalTiposItensMenu.Update(this.tipoItemMenu);
                await Navigation.PopModalAsync();
			}
		}
	}
}
