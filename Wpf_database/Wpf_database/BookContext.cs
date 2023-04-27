using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf_database {
    public class BookContext : INotifyPropertyChanged {
        private MySqlConnection MySqlConn;

        public List<Book> _bookList { get; set; } = new List<Book>();
        public List<Book> Books { get => _bookList; set { _bookList = value; OnPropertyChanged(); } }
        public ConsoleWindow console { get; private set; }
        public BookContext() {
            console = new ConsoleWindow();
            MySqlConn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            Update();
        }
        public void ShowConsole(MainWindow window) {
            console = new ConsoleWindow();
            console.Owner = window;
            console.Show();
        }
        public void Update()
        {
            try {
                MySqlCommand cmd = new MySqlCommand("Select * from books", MySqlConn);
                console.WriteLine(cmd.CommandText);
                MySqlConn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adapter.Fill(ds);
                Books.Clear();
                Books.AddRange(ds.AsEnumerable().Select(r => new Book() {
                    Id = r.Field<int>(0),
                    Title = r.Field<string>(1),
                    Author = r.Field<string>(2),
                    Price = r.Field<decimal>(3),
                    Reviews = r.Field<int>(4),
                }).ToList());
                MySqlConn.Close();
            }
            catch(Exception ex) {
                console.WriteLine(ex.Message);
                Books = new List<Book>() { new Book() { Id=1, Author="Error", Title="Nie dziala", Price=99.99m, Reviews=9 } };
                MessageBox.Show("Blad pobrania danych");
            }
        }
        public void AddBook(Book book) {
            try {
                string price = book.Price.ToString();
                price = price.Replace(",", ".");
                MySqlCommand cmd = new MySqlCommand($"INSERT INTO `books` (`Title`, `Author`, `Price`, `Rewievs`) VALUES ('{book.Title}', '{book.Author}', '{price}', '{book.Reviews}')", MySqlConn);
                console.WriteLine(cmd.CommandText);
                MySqlConn.Open();
                cmd.ExecuteNonQuery();
                MySqlConn.Close();
            }
            catch (Exception ex) {
                console.WriteLine(ex.Message);
                MessageBox.Show("Blad dodawania danych");
            }
        }
        public void EditBook(Book book) {
            try {
                string price = book.Price.ToString();
                price = price.Replace(",", ".");
                MySqlCommand cmd = new MySqlCommand($"UPDATE books SET Title = '{book.Title}', Author = '{book.Author}', Price = '{price}', Rewievs = '{book.Reviews}' WHERE Id LIKE {book.Id}", MySqlConn);
                console.WriteLine(cmd.CommandText);
                MySqlConn.Open();
                cmd.ExecuteNonQuery();
                MySqlConn.Close();
            }
            catch (Exception ex) {
                console.WriteLine(ex.Message);
                MessageBox.Show("Blad aktualizowania danych");
            }
        }
        public void RemoveBook(Book book) {
            try {
                MySqlCommand cmd = new MySqlCommand($"DELETE FROM books WHERE Id LIKE '{book.Id}'", MySqlConn);
                console.WriteLine(cmd.CommandText);
                MySqlConn.Open();
                cmd.ExecuteNonQuery();
                MySqlConn.Close();
            }
            catch (Exception ex) {
                console.WriteLine(ex.Message);
                MessageBox.Show("Blad usuwania danych");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
