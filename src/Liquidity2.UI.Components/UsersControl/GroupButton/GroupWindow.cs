﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Liquidity2.UI.Components.UsersControl.GroupButton
{
    public class GroupWindow : Window
    {
        public IReadOnlyCollection<string> DataSource => listEntity;
        public ICommand BtnClickCmd { get; }
        public ICommand InputCmd { get; set; }

        private readonly string[] listEntity ={ "1","2","3","4","5","6","7","8","9",
                    "0","A","B","C","D","E","F","G","H",
                    "I","J","K","L","M","N","O","P","Q",
                    "R","S","T","U","V","W","X","Y","Z",
                    "a","b","c","d","e","f","g","h","i",
                    "j","k","l","m","n","o","p","q","r",
                    "s","t","u","v","w","x","y","z","/"};
        public GroupWindow()
        {
            InitializeTemplates();
            this.ShowInTaskbar = false;
            DataContext = this;

            //创建window后获得焦点
            this.Deactivated += GroupWindow_Deactivated;

            BtnClickCmd = new CustomRoutedCommand(nameof(BtnClickCmd), typeof(GroupWindow), this);
            this.AddCommandBinding(BtnClickCmd, BtnClickcb_CanExecute, BtnClickcb_Executed);
        }

        public void Show(WindowPosition baseWindow)
        {
            if (baseWindow != null)
            {
                this.Left = baseWindow.Left + baseWindow.Width - this.Width - 10;
                this.Top = baseWindow.Top + 30;
            }
            this.Show();
        }

        public void CloseWindow()
        {
            this.Deactivated -= GroupWindow_Deactivated;
            this.Close();
        }

        private void InitializeTemplates()
        {
            Style = (Style)Application.Current.Resources["GroupWindowStyle"];
        }

        private void BtnClickcb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var group = e.Parameter;
            this.InputCmd.Execute(group);
            e.Handled = true;
        }

        private void BtnClickcb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        private void GroupWindow_Deactivated(object sender, EventArgs e)
        {
            this.CloseWindow();
        }
    }
}
