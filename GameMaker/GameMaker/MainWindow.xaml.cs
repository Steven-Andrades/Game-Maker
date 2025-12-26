using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace GameMaker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // List to store all data stored into the system and displayed in the app.
    List<TeamDetails> teamList = new List<TeamDetails>();
        
    FileManager file = new FileManager();

public MainWindow()
    {
        // Sets up the window and triggers its component building.
        InitializeComponent();
            
        // Reads data for any previously saved entries.
        teamList = file.ReadDataFromFile();
        
        // Update the table view.
        UpdateTableView();
    }
    
// Button Save event.
private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        SaveNewEntry();
        // Ensures table is updated.
        UpdateTableView();
    }

// Method that checks if all input fields are filled (no nulls) and saves entry into the teamList.
    private void SaveNewEntry()
    {
        // Checks if the form is filled before saving.
        // If not, it pops up a message to the user and then exits the method.
        if (IsFormFilledCorrectly() == false)
        {
            MessageBox.Show(this, "Form is not filled correctly.\nPlease check and try again.", "Attention!",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;         
        }
        // Writes all the provided text into a new Expense object.
        TeamDetails newEntry = new TeamDetails();
        // Read each input field and gets its value and puts it into the object property.
        newEntry.TeamName = txtTeamName.Text;
        newEntry.ContactName = txtContactName.Text;
        newEntry.ContactPhone = txtContactPhone.Text;
        newEntry.ContactEmail = txtContactEmail.Text;
        newEntry.CompetitionPoints = int.Parse(txtCompetitionPoints.Text);
            
        // Adds the new object to the list.
        teamList.Add(newEntry);
        
        // Writes new entry into file.
        file.WriteDataToFile(teamList.ToArray());

        // Refresh table.
        UpdateTableView();

        // Inform users that new entry was saved.
        MessageBox.Show(this, "New entry has been saved.", "New Entry Added", 
            MessageBoxButton.OK, MessageBoxImage.Information);

        // Clear input fields.
        ClearFields();
    }

// Button delete event.
private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        DeleteEntry();
        // Ensures table is updated.
        UpdateTableView(); 
    }

// Method that deletes an entry selected on the data grid view table. 
private void DeleteEntry()
    {
        if (dgvteamTable.SelectedItem is TeamDetails selectedItem)
        {
            // Prompt user if they really want to delete item.
            MessageBoxResult answer = MessageBox.Show(this, $"Are you sure you want to " +
                $"delete '{selectedItem.TeamName}?'\nThis action cannot be undone!'", 
                "Confirm Deletion?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (answer == MessageBoxResult.Yes)
            {
                // Delete selected item row from table.
                teamList.Remove(selectedItem);

                // Write deletion into file.
                file.WriteDataToFile(teamList.ToArray());

                // Refresh table.
                UpdateTableView();

                // Clears input fields.
                ClearFields();

                // Inform user that deletion worked.
                MessageBox.Show(this, $"Entry deleted successfully.", 
                    "Entry Deleted.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        else
        {
            // Prompt user to select which row. 
            MessageBox.Show(this, "Please select something in the table to delete.");
            return;
        }
    }

// Button Update.
private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
        UpdateEntry();
        // Ensures table is updated.
        UpdateTableView();
    }

// Method that finds selected entry in teamList that enables editing entry, and saves the changes.
private void UpdateEntry()
    {
        if (dgvteamTable.SelectedItem is TeamDetails selectedItem)
        {      
            // Edit and Update existing entry using input fields.
            selectedItem.TeamName = txtTeamName.Text;
            selectedItem.ContactName = txtContactName.Text;
            selectedItem.ContactPhone = txtContactPhone.Text;
            selectedItem.ContactEmail = txtContactEmail.Text;
            // Try parse checks the points safely.
            if (int.TryParse(txtCompetitionPoints.Text, out int points))
            {
                selectedItem.CompetitionPoints = points;
            }
            else
            {
                // Display message to prompt user to use numbers in the mentioned field. 
                MessageBox.Show(this, "Please enter a score 'number' in Competition Points.", "Invalid Format!", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Save edit to file.
            file.WriteDataToFile(teamList.ToArray());

            // Refresh table.
            UpdateTableView();

            // Clear input fields.
            ClearFields();

            // Display entry success message.
            MessageBox.Show(this, "Entry updated successfully.", "Updated Successfull.", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            
        }
        else
        {
            // Display message to select a row.
            MessageBox.Show(this, "Please select a row in the table to update.", "Attention!", 
                MessageBoxButton.OK ,MessageBoxImage.Error);
        }
    }

    // Method update with parameters (overloads).
    private void UpdateEntry(TeamDetails selectedItems, string teamName, string contactName, 
        string contactPhone, string contactEmail, int competitionPoints)
    {
        selectedItems.TeamName = teamName;
        selectedItems.ContactName = contactName;
        selectedItems.ContactPhone = contactPhone;
        selectedItems.ContactEmail = contactEmail;
        selectedItems.CompetitionPoints = competitionPoints;

        // Write to file.
        file.WriteDataToFile(teamList.ToArray());
        // Update data grid view table.
        UpdateTableView();
    }

// Checks each entry field to see if it is blank or invalid data.
// If any of them are, it will return a false value, otherwise it returns a true (all filled) value.
private bool IsFormFilledCorrectly()
    {
        if (string.IsNullOrWhiteSpace(txtTeamName.Text))
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtContactName.Text))
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtContactPhone.Text))
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtContactEmail.Text))
        {
            return false;
        }
        // Try parse checks the entered value is a valid number. Will also fail if blank.
        if (int.TryParse(txtCompetitionPoints.Text, out int value) == false)
        {
            return false;
        }
        return true;
    }

// Button Exit event.
private void btnExit_Click(object sender, RoutedEventArgs e)
    {
        // Prompt user if they want to exit.
        MessageBoxResult confirm = MessageBox.Show(this, $"Are you sure you want to " +
                $"exit the application?'", "Confirm Deletion?", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (confirm == MessageBoxResult.Yes)
        {
            // Closes the app completely.
            Application.Current.Shutdown();
        } 
    }

// Detects when row is selected.
private void dgvteamTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (dgvteamTable.SelectedItem is TeamDetails selectedItem)
        {
            txtTeamName.Text = selectedItem.TeamName;
            txtContactName.Text = selectedItem.ContactName;
            txtContactPhone.Text = selectedItem.ContactPhone;
            txtContactEmail.Text = selectedItem.ContactEmail;
            txtCompetitionPoints.Text = selectedItem.CompetitionPoints.ToString();
        }
    }

// Clear form back to empty state.
private void ClearFields()
    {
        txtTeamName.Text = "";
        txtContactName.Text = "";
        txtContactPhone.Text = "";
        txtContactEmail.Text = "";
        txtCompetitionPoints.Text = "";

        txtTeamName.Focus();
    }

// Update the table view when called.
private void UpdateTableView()
    {
        // Reset binding
        dgvteamTable.ItemsSource = null;
        // Redbind updated list
        dgvteamTable.ItemsSource = teamList;
        // Refresh the DataGridView
        dgvteamTable.Items.Refresh();
    }

// Button keyboard event.
private void StackPanel_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) 
        {
            SaveNewEntry();
            // Ensures table is updated.
            UpdateTableView();
        }
    }
}