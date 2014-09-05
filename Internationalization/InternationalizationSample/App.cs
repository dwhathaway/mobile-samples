using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;

namespace InternationalizationSample
{
	public class PageViewModel : INotifyPropertyChanged
	{
		public static IStrings Strings { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public string Hello
		{
			/* set accessor ommitted */
			get
			{
				return Strings.GetString("hello");
			}
		}
	}

	public interface IStrings
	{
		string GetString (string stringName);
	}

	public class App
	{
		public static IStrings Strings { get; set; }

		static Label label;
		static Button button;

		public static Page GetMainPage ()
		{
			label = new Label () { 
				Text = Strings.GetString ("hello"),
				IsVisible = false
			};

			button = new Button () {
				Text = "Show label"
			};

			button.Clicked += (object sender, EventArgs e) => {
				label.IsVisible = !label.IsVisible;
				button.Text = label.IsVisible ? "Hide Label" : "Show Label";
			};

			StackLayout stackLayout = new StackLayout () {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			stackLayout.Children.Add (label);
			stackLayout.Children.Add (button);

			return new ContentPage { 
				Content = stackLayout
			};
		}
	}
}

