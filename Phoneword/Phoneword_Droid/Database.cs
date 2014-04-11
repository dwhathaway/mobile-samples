using System;

using SQLite;
using SQLite.Net;

using Phoneword.Common;
using System.Collections.Generic;

namespace Phoneword_Droid
{
	public class Database : SQLiteConnection
	{
		public Database (string path) : base(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), path)
		{
			CreateTable<CallHistoryItem> ();
		}

		public IEnumerable<CallHistoryItem> QueryCallHistory ()
		{
			return 	from s in Table<CallHistoryItem> () select s;
		}

		/*public void UpdateStock (string stockSymbol)
		{
			//
			// Ensure that there is a valid Stock in the DB
			//
			var callHistoryItem = QueryCallHistory (stockSymbol);
			if (callHistoryItem == null) {
				callHistoryItem = new CallHistoryItem { Number = "1-855-XAMARIN" };
				Insert (callHistoryItem);
			}
		}*/
	}
}

