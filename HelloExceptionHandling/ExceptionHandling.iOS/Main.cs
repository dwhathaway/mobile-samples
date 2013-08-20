using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Runtime.InteropServices;

namespace ExceptionHandling.iOS
{
	/// <summary>
	/// This method uses the NSSetUncauthExceptionHandler method as described in
	/// http://stackoverflow.com/questions/6542428/how-do-you-bind-to-or-invoke-foundation-functions
	/// </summary>
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			//Catch any unhandled managed exceptions
			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e) {
				
				doSomething(((Exception)e.ExceptionObject));
				
			};

			//Catch any unhandled Obj-C Runtime exceptions
			NSSetUncaughtExceptionHandler (Marshal.GetFunctionPointerForDelegate(new NSUncaughtExceptionHandler(MyUncaughtExceptionHandler)));

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}

		/// <summary>
		/// Function for handling Uncaught Obj-C runtime exceptions
		/// </summary>
		/// <param name="exception">Exception.</param>
		static void MyUncaughtExceptionHandler (IntPtr ex)
		{
			//Insert Crittercism or Bugsnag code here to track 
			Console.WriteLine (ex.ToString ());
		}

		/// <summary>
		/// Function for handling Uncaught managed exceptions
		/// </summary>
		/// <param name="ex">Ex.</param>
		private static void doSomething(Exception ex)
		{
			//Insert Crittercism or Bugsnag code here to track 
			Console.WriteLine (ex.ToString ());
		}
		
		public delegate void NSUncaughtExceptionHandler (IntPtr exception);

		[DllImport ("/System/Library/Frameworks/Foundation.framework/Foundation")]
		extern static void NSSetUncaughtExceptionHandler (IntPtr handler);
	}
}