using System;
using System.Collections.ObjectModel;
using Modulo1.Models;

namespace Modulo1.Dal {
    
    public class GarconDAL {

        private ObservableCollection<Garcon> Garcons = new ObservableCollection<Garcon>();
        private static GarconDAL GarconInstance = new GarconDAL();

        private GarconDAL() {
            Garcons.Add(new Garcon() {
                Id = 1, Nome = "Brauzio", Telefone = "Asdrugio"
            });
            Garcons.Add(new Garcon() {
                Id = 2, Nome = "Entencius", Telefone = "Gesfredio"
            });
            Garcons.Add(new Garcon() {
                Id = 3, Nome = "Cartucious", Telefone = "Gesfrundio"
            });
            Garcons.Add(new Garcon() {
                Id = 4, Nome = "Adoliterio", Telefone = "Kentencio"
            });
            Garcons.Add(new Garcon() {
                Id = 5, Nome = "Castrogildo", Telefone = "Gesifrelio"
            });
            Garcons.Add(new Garcon() {
                Id = 6, Nome = "Asdrugio", Telefone = "Brauzio"
            });
			Garcons.Add(new Garcon() {
				Id = 7, Nome = "Gesfredio", Telefone = "Entencius"
			});
			Garcons.Add(new Garcon() {
				Id = 8, Nome = "Gesfrundio", Telefone = "Cartucious"
            });
			Garcons.Add(new Garcon() {
				Id = 9, Nome = "Kentencio", Telefone = "Adoliterio"
            });
			Garcons.Add(new Garcon() {
                Id = 10, Nome = "Gesifrelio", Telefone = "Castrogildo"
            });
        }

        public static GarconDAL GetInstance() {
            return GarconInstance;
        }

        public ObservableCollection<Garcon> GetAll() {
            return Garcons;
        }

        public void Add(Garcon garcon) {
            this.Garcons.Add(garcon);
        }
    }
}
