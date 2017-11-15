﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace EatInTimeClient.View
{
    /// <summary>
    /// Logique d'interaction pour UserControlOrdering.xaml
    /// </summary>
    public partial class UserControlOrdering : UserControl
    {
        public UserControlOrdering()
        {
            InitializeComponent();
        }

        private void SearchDishTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchDishTextBox.Text = String.Empty;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}