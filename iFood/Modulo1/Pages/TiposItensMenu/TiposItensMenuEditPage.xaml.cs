using System;
using System.Collections.Generic;
using Modulo1.Dal;
using Modulo1.Models;
using PCLStorage;
using Plugin.Media;
using Xamarin.Forms;

namespace Modulo1.Pages.TiposItensMenu {
    
    public partial class TiposItensMenuEditPage : ContentPage {


        private TipoItemMenu tipoItemMenu;
        private string caminhoArquivo;
		private TipoItemMenuDAL dalTiposItensMenu = TipoItemMenuDAL.GetInstance();


        public TiposItensMenuEditPage(TipoItemMenu tipoItemMenu) {
			InitializeComponent();
            PopularFormulario(tipoItemMenu);
			RegistaClickBotaoCamera(idtipoitemmenu.Text.Trim());
			RegistaClickBotaoAlbum();
		}

        private void PopularFormulario(TipoItemMenu tipoItemMenu) {
            this.tipoItemMenu = tipoItemMenu;
            idtipoitemmenu.Text = tipoItemMenu.Id.ToString();
            nome.Text = tipoItemMenu.Nome;
            caminhoArquivo = tipoItemMenu.CaminhoArquivoFoto;
            fototipoitemmenu.Source = ImageSource.FromFile(tipoItemMenu.CaminhoArquivoFoto);
		}

		private void RegistaClickBotaoCamera(string idparafoto) {
			BtnCamera.Clicked += async (sender, e) => {
				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
					DisplayAlert("Não existe câmera", "A câmera não está disponível", "OK");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions {
					Directory = FileSystem.Current.LocalStorage.Name,
					Name = "tipoitem_" + idparafoto + ".jpg",
					SaveToAlbum = true
				});

				if (file == null) {
					return;
				}

                fototipoitemmenu.Source = ImageSource.FromFile(file.Path);
				
                caminhoArquivo = file.Path;
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

				var getArquivoPCL = FileSystem.Current.GetFileFromPathAsync(file.Path);

				var rootFolder = FileSystem.Current.LocalStorage;

				var folderFoto = await rootFolder.CreateFolderAsync("Fotos", CreationCollisionOption.OpenIfExists);

				var setArquivoPCL = folderFoto.CreateFileAsync(getArquivoPCL.Result.Name, CreationCollisionOption.ReplaceExisting);

				caminhoArquivo = setArquivoPCL.Result.Path;

                if (file == null) {
                    return;
                }

				fototipoitemmenu.Source = ImageSource.FromStream(() => {
					var stream = file.GetStream();
					file.Dispose();
					return stream;
				});
			};
		}

        public async void BtnGravarClick(object sender, EventArgs e) {
			if (nome.Text.Trim() == string.Empty) {
				this.DisplayAlert("Erro", "Você precisa informar o nome para o novo tipo de item do menu.", "OK");
			} else {
                this.tipoItemMenu.Nome = nome.Text;
                this.tipoItemMenu.CaminhoArquivoFoto = caminhoArquivo;
                dalTiposItensMenu.Update(this.tipoItemMenu);
                await Navigation.PopModalAsync();
			}
		}
	}
}
