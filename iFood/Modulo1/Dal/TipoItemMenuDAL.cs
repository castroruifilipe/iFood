using System;
using System.Collections.Generic;
using Modulo1.Models;
using Modulo1.Infraestructure;
using SQLite.Net;
using Xamarin.Forms;
using System.Linq;
using SQLiteNetExtensions.Extensions;

namespace Modulo1.Dal {
    
    public class TipoItemMenuDAL {

        private SQLiteConnection sqlConnection;


        public TipoItemMenuDAL() {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
			this.sqlConnection.CreateTable<TipoItemMenu>();
        }

        public IEnumerable<TipoItemMenu> GetAll() {
            return (from t in sqlConnection.Table<TipoItemMenu>() select t).OrderBy(i => i.Nome).ToList();
        }
		
        public IEnumerable<TipoItemMenu> GetAllWithChildren() {
            return sqlConnection.GetAllWithChildren<TipoItemMenu>().OrderBy(i => i.Nome).ToList();
        }

        public TipoItemMenu GetItemById(long id) {
            return sqlConnection.Table<TipoItemMenu>().FirstOrDefault(t => t.TipoItemMenuId == id);
        }

        public void DeleteById(long id) {
            sqlConnection.Delete<TipoItemMenu>(id);
        }

        public void Add(TipoItemMenu tipoItemMenu) {
            sqlConnection.Insert(tipoItemMenu);
        }

        public void Update(TipoItemMenu tipoItemMenu) {
            sqlConnection.Update(tipoItemMenu);
        }
    }
}
