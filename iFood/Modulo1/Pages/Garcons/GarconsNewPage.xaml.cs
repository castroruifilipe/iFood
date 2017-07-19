using System;
using System.Collections.Generic;
using System.Linq;
using Modulo1.Dal;
using Xamarin.Forms;

namespace Modulo1.Pages.Garcons {

	public partial class GarconsNewPage : ContentPage {

		private GarconDAL dalGarcons = GarconDAL.GetInstance();


		public GarconsNewPage() {
			InitializeComponent();
			PreparaParaNovoGarcon();
		}

		public void BtnGravarClick(object sender, EventArgs e) {
			if (nome.Text.Trim() == string.Empty || telefone.Text == string.Empty) {
				this.DisplayAlert("Erro", "É necessário fornecer o nome e o telefone do novo garcon", "Ok");
			} else {
				dalGarcons.Add(new Models.Garcon() {
					Id = Convert.ToUInt32(idgarcon.Text),
					Nome = nome.Text,
					Telefone = telefone.Text
				});
				PreparaParaNovoGarcon();
			}
		}

		private void PreparaParaNovoGarcon() {
			var novoId = dalGarcons.GetAll().Max(x => x.Id) + 1;
			idgarcon.Text = novoId.ToString().Trim();
			nome.Text = string.Empty;
			telefone.Text = string.Empty;
		}
	}
}
