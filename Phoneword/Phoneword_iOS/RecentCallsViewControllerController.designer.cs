// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Phoneword_iOS
{
	partial class RecentCallsViewControllerController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView callHistoryTable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (callHistoryTable != null) {
				callHistoryTable.Dispose ();
				callHistoryTable = null;
			}
		}
	}
}
