using System;
using System.Collections.Generic;
using Modulo1.Dal;
using Xamarin.Forms;
using System.Linq;
using Plugin.Media;
using PCLStorage;
using System.IO;
using Modulo1.Models;

namespace Modulo1.Pages.TiposItensMenu {
    
    public partial class TiposItensMenuNewPage : ContentPage {

        private TipoItemMenuDAL dalTiposItensMenu = new TipoItemMenuDAL();
        private byte[] bytesFoto;


        public TiposItensMenuNewPage() {
            InitializeComponent();
            PreparaParaNovoTipoItemMenu();
            RegistaClickBotaoCamera(idtipoitemmenu.Text.Trim());
            RegistaClickBotaoAlbum();
        }

        private void PreparaParaNovoTipoItemMenu() {
            var novoId = dalTiposItensMenu.GetAll().Max(x => x.TipoItemMenuId) + 1;
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

        public void BtnGravarClick(object sender, EventArgs e) {
            if (nome.Text.Trim() == string.Empty) {
                this.DisplayAlert("Erro", "Você precisa informar o nome para o novo tipo de item do menu.", "OK");
            } else {
                dalTiposItensMenu.Add(new TipoItemMenu() {
                    Nome = nome.Text,
                    Foto = bytesFoto
                });
                PreparaParaNovoTipoItemMenu();
            }
        }
    }
}
