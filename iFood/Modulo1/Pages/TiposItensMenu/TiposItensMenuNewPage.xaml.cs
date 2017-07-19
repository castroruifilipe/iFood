using System;
using System.Collections.Generic;
using Modulo1.Dal;
using Xamarin.Forms;
using System.Linq;
using Plugin.Media;
using PCLStorage;

namespace Modulo1.Pages.TiposItensMenu {
    
    public partial class TiposItensMenuNewPage : ContentPage {

        private TipoItemMenuDAL dalTiposItensMenu = TipoItemMenuDAL.GetInstance();
        private string caminhoArquivo;


        public TiposItensMenuNewPage() {
            InitializeComponent();
            PreparaParaNovoTipoItemMenu();
            RegistaClickBotaoCamera(idtipoitemmenu.Text.Trim());
            RegistaClickBotaoAlbum();
        }

        private void PreparaParaNovoTipoItemMenu() {
            var novoId = dalTiposItensMenu.GetAll().Max(x => x.Id) + 1;
            idtipoitemmenu.Text = novoId.ToString().Trim();
            nome.Text = string.Empty;
            fototipoitemmenu.Source = null;
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
                    Name = "tipoitem_" + idparafoto + ".jpg",
                    SaveToAlbum = true
                });

                if (file == null) {
                    return;
                }

                caminhoArquivo = file.Path;

                fototipoitemmenu.Source = ImageSource.FromStream(() => {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
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

                var getArquivoPCL = FileSystem.Current.GetFileFromPathAsync(file.Path);

                var rootFolder = FileSystem.Current.LocalStorage;

                var folderFoto = await rootFolder.CreateFolderAsync("Fotos", CreationCollisionOption.OpenIfExists);

                var setArquivoPCL = folderFoto.CreateFileAsync(getArquivoPCL.Result.Name, CreationCollisionOption.ReplaceExisting);

                caminhoArquivo = setArquivoPCL.Result.Path;

                fototipoitemmenu.Source = ImageSource.FromStream(() => {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };
        }

        public void BtnGravarClick(object sender, EventArgs e) {
            if (nome.Text.Trim() == string.Empty) {
                this.DisplayAlert("Erro", "Você precisa informar o nome para o novo tipo de item do menu.", "OK");
            } else {
                dalTiposItensMenu.Add(new Models.TipoItemMenu() {
                    Id = Convert.ToUInt32(idtipoitemmenu.Text),
                    Nome = nome.Text,
                    CaminhoArquivoFoto = caminhoArquivo
                });
                PreparaParaNovoTipoItemMenu();
            }
        }
    }
}
