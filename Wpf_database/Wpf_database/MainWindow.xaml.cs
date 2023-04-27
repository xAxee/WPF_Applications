using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_database {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        BookContext bookContext;
        public MainWindow() {
            bookContext = new BookContext();
            InitializeComponent();
            GridList.ItemsSource = bookContext.Books;
            UpdateInfo();
        }

        private void GridList_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e) {
            if (e.EditAction == DataGridEditAction.Commit) {
                Book book = e.Row.DataContext as Book;
                if (book.Id == 0) {
                    bookContext.AddBook(book);
                    UpdateInfo();
                    return;
                }
                bookContext.EditBook(book);
                UpdateInfo();
            }
        }

        private void GridList_PreviewKeyDown(object sender, KeyEventArgs e) {
            var grid = (DataGrid)sender;
            if (e.Key != Key.Delete) return;
            if (grid.SelectedItems.Count == 0) return;

            foreach (var item in grid.SelectedItems) {
                Book book = item as Book;
                bookContext.RemoveBook(book);
                UpdateInfo();
            }
        }

        private void Refresh_btn_Click(object sender, RoutedEventArgs e) {
            bookContext.Update();
            GridList.Items.Refresh();
            UpdateInfo();
        }

        public void UpdateInfo() {
            Info_Count.Text = $"Ilość: {bookContext.Books.Count}";
            Info_pSum.Text = $"Suma ceny: {bookContext.Books.Sum(e => e.Price)}";
            Info_pAvg.Text = $"Średnia cena: {String.Format("{0:0.00}", bookContext.Books.Average(e => e.Price))}";
            Info_pMax.Text = $"Maksymalna cena: {bookContext.Books.Max(e => e.Price)}";
            Info_pMin.Text = $"Minimalna cena: {bookContext.Books.Min(e => e.Price)}";

            Info_rAvg.Text = $"Średnia opinia: {String.Format("{0:0.00}", bookContext.Books.Average(e => e.Reviews))}";
            Info_rMax.Text = $"Maksymalna opinia: {bookContext.Books.Max(e => e.Reviews)}";
            Info_rMin.Text = $"Minimalna opinia: {bookContext.Books.Min(e => e.Reviews)}";
        }
        private void Console_btn_Click(object sender, RoutedEventArgs e) {
            bookContext.ShowConsole(this);
        }
    }
}
