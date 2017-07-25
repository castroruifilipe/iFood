using System;
using System.Collections.Generic;
using Modulo1.Models;
using Modulo1.Infraestructure;
using SQLite.Net;
using Xamarin.Forms;
using System.Linq;
using SQLiteNetExtensions.Extensions;

namespace Modulo1.Dal {
    
    public class ItemMenuDAL {

        private SQLiteConnection sqlConnection;


        public ItemMenuDAL() {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
			this.sqlConnection.CreateTable<ItemMenu>();
        }

        public void Add(ItemMenu itemMenu) {
            sqlConnection.Insert(itemMenu);
        }

        public void DeleteById(long id) {
            sqlConnection.Delete<ItemMenu>(id);
        }

        public void Update(ItemMenu itemMenu) {
            sqlConnection.Update(itemMenu);
        }
    }
}
