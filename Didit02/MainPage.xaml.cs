using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Didit02
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void AddHabit(object sender, RoutedEventArgs e)
        {
            using (SqliteConnection db = new SqliteConnection("Filename= didit_db.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                //Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Habit VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", Input_Box.Text);

                try
                {
                    insertCommand.ExecuteReader();
                }
                catch (SqliteException error)
                {
                    //Handle error
                    return;
                }
                db.Close();
            }
            Output.ItemsSource = Grab_Entries();
        }

        private List<String> Grab_Entries()
        {
            List<string> habits = new List<string>();
            using (SqliteConnection db = new SqliteConnection("Filename=didit_db.db"))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand("SELECT text_habit from Habit", db);
                SqliteDataReader query;
                try
                {
                    query = selectCommand.ExecuteReader();
                }
                catch (SqliteException error)
                {
                    //Handle error
                    return habits;
                }
                while (query.Read())
                {
                    habits.Add(query.GetString(0));
                }
                db.Close();
            }
            return habits;
        }
    
    }
}
