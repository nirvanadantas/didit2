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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Didit02
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAddHabit : Page
    {
        public PageAddHabit()
        {
            InitializeComponent();
           // NavigationCacheMode = NavigationCacheMode.Enabled;

        }

        private void BacktoMain(object sender, RoutedEventArgs e)
        {
           Frame.Navigate(typeof(MainPage));
        }

        private void AddHabit(object sender, RoutedEventArgs e)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=didit1_db.db"))
            {
                db.Open();
                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;
                //Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO Habit VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", habit_string.Text);

                try
                {
                    insertCommand.ExecuteReader();
                }
                catch (SqliteException error)
                {
                    //return;
                }
                db.Close();                
                Frame.Navigate(typeof(MainPage));
            }
            

        }

       
    
    }
}
