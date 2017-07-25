using System;
using System.Collections.Generic;
using System.IO;
using Modulo1.Dal;
using Modulo1.Models;
using Modulo1.Pages.TiposItensMenu;
using Plugin.Media;
using Xamarin.Forms;

namespace Modulo1.Pages.ItensMenu.Controls {
    
    public partial class GridCustomControl : Grid {

        private byte[] bytesFoto;
        private ItemMenu itemMenu = null;


        public GridCustomControl() {
            InitializeComponent();
        }

        private async void OnTapLookForTipos(object sender, EventArgs args) {
            await Navigation.PushAsync(new TiposItensMenuSearchPage(idTipo, nomeTipo));
        }

        public async void OnAlbumClicked(object sender, EventArgs e) {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported) {
                await App.Current.MainPage.DisplayAlert("Álbum não suportado", "Não existe permissão para aceder o albúm de fotos", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null) {
                return;
            }

            var stream = file.GetStream();
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            fotoAlbum.Source = ImageSource.FromStream(() => {
                var s = file.GetStream();
                file.Dispose();
                return s;
            });
            bytesFoto = memoryStream.ToArray();
        }

        private async void OnTappedSaveItem(object sender, EventArgs args) {
            var dal = new ItemMenuDAL();
            if (this.itemMenu == null) {
                this.itemMenu = new ItemMenu();
            }
            this.itemMenu.Nome = nome.Text;
            this.itemMenu.Descricao = descricao.Text;
            this.itemMenu.Preco = Convert.ToDouble(preco.Text);
            this.itemMenu.TipoItemMenuId = Convert.ToInt32(idTipo.Text);
            this.itemMenu.Foto = bytesFoto;

            if (this.itemMenu.ItemMenuId == null) {
                dal.Add(this.itemMenu);
				await App.Current.MainPage.DisplayAlert("Inserção de item", "Item inserido com sucesso", "OK");
            } else {
                dal.Update(this.itemMenu);
				await App.Current.MainPage.DisplayAlert("Alteração de item", "Item alterado com sucesso", "OK");
                await Navigation.PopAsync();
            }

            ClearControls();
        }

        private void ClearControls() {
            nome.Text = string.Empty;
			descricao.Text = string.Empty;
			preco.Text = string.Empty;
            bytesFoto = null;
			idTipo.Text = string.Empty;
			nomeTipo.Text = string.Empty;
            fotoAlbum.Source = null;
		}

        public void PopularControls(ItemMenu itemMenu) {
            this.itemMenu = itemMenu;
            nome.Text = this.itemMenu.Nome;
            descricao.Text = this.itemMenu.Descricao;
            preco.Text = this.itemMenu.Preco.ToString("N");
            if (this.itemMenu.Foto != null) {
                fotoAlbum.Source = ImageSource.FromStream(() => new MemoryStream(this.itemMenu.Foto));
                bytesFoto = this.itemMenu.Foto;
            }
            nomeTipo.Text = this.itemMenu.TipoItemMenu.Nome;
            idTipo.Text = this.itemMenu.TipoItemMenuId.ToString();
        }


    }
}
