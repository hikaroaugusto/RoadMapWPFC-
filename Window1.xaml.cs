/*
 * Created by SharpDevelop.
 * User: hikar
 * Date: 26/05/2018
 * Time: 01:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using RoadMapWPF.ViewModels;

namespace RoadMapWPF
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}
		
		void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
		
		void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonOpenMenu.Visibility = Visibility.Collapsed;
			ButtonCloseMenu.Visibility = Visibility.Visible;
		}
		
		void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonOpenMenu.Visibility = Visibility.Visible;
			ButtonCloseMenu.Visibility = Visibility.Collapsed;
		}
		
		void RMHikaro_Click(object sender, RoutedEventArgs e)
		{
			DataContext = new RMHikaroViewModel();
		}
		
		void RMGeral_Click(object sender, RoutedEventArgs e)
		{
			DataContext = new RMGeralViewModel();
		}
	}
}