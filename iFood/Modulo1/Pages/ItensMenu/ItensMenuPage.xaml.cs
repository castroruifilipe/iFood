using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Modulo1.Dal;
using Modulo1.HelperControls;
using Modulo1.Models;
using Xamarin.Forms;

namespace Modulo1.Pages.ItensMenu {
    
    public partial class ItensMenuPage : ContentPage {

		private ItemMenuDAL dalItemMenu = new ItemMenuDAL();
        private TipoItemMenuDAL dalTipoItemMenu = new TipoItemMenuDAL();


        public ItensMenuPage() {
            InitializeComponent();
            lvItensMenu.ItemsSource = GetDataByGroup();
            BtnNovoItem.Clicked += BtnNovoItemClick;
        }

        private async void BtnNovoItemClick(object sender, EventArgs e) {
            await Navigation.PushAsync(new ItensMenuNewPage());
        }

        private Collection<ListViewGrouping<TipoItemMenu, ItemMenu>> GetDataByGroup() {
            var dadosAgrupados = new Collection<ListViewGrouping<TipoItemMenu, ItemMenu>>();
            var tipos = dalTipoItemMenu.GetAllWithChildren();
            foreach (var tipo in tipos) {
                Debug.WriteLine(tipo.Nome);
                foreach (var item in tipo.Itens) {
                    Debug.WriteLine(item.Nome);
                }
                Debug.WriteLine("-------------------------------------------------------");
                dadosAgrupados.Add(new ListViewGrouping<TipoItemMenu, ItemMenu>(tipo, tipo.Itens));
            }
            return dadosAgrupados;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            lvItensMenu.BeginRefresh();
            lvItensMenu.ItemsSource = GetDataByGroup();
            lvItensMenu.EndRefresh();
        }

        public async void OnAlterarClick(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as ItemMenu;
            await Navigation.PushAsync(new ItensMenuEditPage(item));
        }

        public async void OnRemoverClick(object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as ItemMenu;
            var opcao = await DisplayAlert("Confirmação de remoção", "Comfirma remover o item " + item.Nome.ToUpper() + "?", "Sim", "Não");
            if (opcao == true) {
                dalItemMenu.DeleteById((long)item.ItemMenuId);
                this.lvItensMenu.ItemsSource = GetDataByGroup();
            }
        }
    }
}
