using System;
using Modulo1.Infraestructure;
using Modulo1.Droid;
using SQLite.Net.Platform.XamarinAndroid;
using System.IO;
using SQLite.Net;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]

namespace Modulo1.Droid {
    
    public class DatabaseConnection_Android {

        public SQLiteConnection DbConnection() {
            var dbName = "iFoodDb.db3";
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsFolder, dbName);
            var platform = new SQLitePlatformAndroid();
            return new SQLiteConnection(platform, path);
        }

    }
}
