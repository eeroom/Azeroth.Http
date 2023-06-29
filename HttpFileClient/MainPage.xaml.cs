using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HttpFileClient
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("你点我了");
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
           
            SL4PopupMenu.PopupMenu menu = new SL4PopupMenu.PopupMenu(this);
            menu.AddItem("文件夹", (sd,ev) =>
            {
                MessageBox.Show("你点了文件夹");
            });
            var btn= sender as Button;
            var point= btn.TransformToVisual(null);
            var menupoint= point.Transform(new Point());
            menupoint.Y = menupoint.Y + btn.Height;
            menu.Open(menupoint, false, false);
        }
    }
}
