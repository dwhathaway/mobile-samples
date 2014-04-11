using System;

using SQLite;
using SQLite.Net;

using Phoneword.Common;
using System.Collections.Generic;

namespace Phoneword_iOS
{
	public class Database : SQLiteConnection
	{
		public Database (string path) : base(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path, false)
		{
			CreateTable<CallHistoryItem> ();
		}

		public IEnumerable<CallHistoryItem> QueryCallHistory ()
		{
			return 	from s in Table<CallHistoryItem> () select s;
		}
	}
}

