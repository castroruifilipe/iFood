using System;
using System.Collections.Generic;
using System.Linq;
using Modulo1.Dal;
using Modulo1.Models;

using Xamarin.Forms;

namespace Modulo1.Pages.TiposItensMenu {
    
    public partial class TiposItensMenuSearchPage : ContentPage {

        private TipoItemMenuDAL dalTipoItemMenu = new TipoItemMenuDAL();
        private Label displayValue;
        private Label keyValue;
        private IEnumerable<TipoItemMenu> itens;


        public TiposItensMenuSearchPage(Label keyValue, Label displayValue) {
            InitializeComponent();
            itens = dalTipoItemMenu.GetAll();
            this.keyValue = keyValue;
            this.displayValue = displayValue;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e) {
            lvTipos.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue)) {
                lvTipos.ItemsSource = itens;
            } else {
                lvTipos.ItemsSource = itens.Where(i => i.Nome.Contains(e.NewTextValue));
            }
            lvTipos.EndRefresh();
        }

        public async void OnItemTapped(object o, ItemTappedEventArgs e) {
            var item = (o as ListView).SelectedItem as TipoItemMenu;
            this.keyValue.Text = item.TipoItemMenuId.ToString();
            this.displayValue.Text = item.Nome;
            await Navigation.PopAsync();
        }
    }
}
