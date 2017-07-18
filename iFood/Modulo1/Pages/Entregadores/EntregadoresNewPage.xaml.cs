using System;
using System.Collections.Generic;
using System.Linq;
using Modulo1.Dal;
using Xamarin.Forms;

namespace Modulo1.Pages.Entregadores {
    
    public partial class EntregadoresNewPage : ContentPage {

        private EntregadorDAL dalEntregadores = EntregadorDAL.GetInstance();


        public EntregadoresNewPage() {
            InitializeComponent();
            PreparaParaNovoEntregador();
        }

        public void BtnGravarClick(object sender, EventArgs e) {
            if (nome.Text.Trim() == string.Empty || telefone.Text == string.Empty) {
                this.DisplayAlert("Erro", "É necessário fornecer o nome e o telefone do novo entregador", "Ok");
            } else {
                dalEntregadores.Add(new Models.Entregador() {
                    Id = Convert.ToUInt32(identregador.Text),
                    Nome = nome.Text,
                    Telefone = telefone.Text
                });
                PreparaParaNovoEntregador();
            }
        }

        private void PreparaParaNovoEntregador() {
            var novoId = dalEntregadores.GetAll().Max(x => x.Id) + 1;
            identregador.Text = novoId.ToString().Trim();
            nome.Text = string.Empty;
            telefone.Text = string.Empty;
        }
    }
}
