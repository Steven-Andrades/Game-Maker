# Game Maker
A desktop application for managing team details that includes the team's names, contact information and competition points. 

![Main Window](Screenshots/main-window.png)

## Features
- **Add New Entries**: Add new team entries with validation checks to ensure all fields are correctly filled.
- **Update Entries**: Edit existing team details from the table.
- **Delete Entries**: Remove existing team details from the table.
- **Data Persistence**: Team data is saved to a CSV file and loaded on application startup.
- **Input Validation**: Ensures competition points are entered as valid numbers.
- **User Feedback**: Provides messages for success, warnings and errors.

## How to use
1. Launch the application.
2. Fill in team details in the form and click "Save" to add the new entry to the table.
3. To update and entry, select an existing entry from the table and edit the details in the form, then click "Update".
4. To delete an entry, select an existing entry from the table and click "Delete".

## File Structure
The application uses a single data file to store the team information. If the file doesn't exist on startup, one will be created automatically.
Changes are saved to the data file when new entries are added, updated or deleted.

## Technologies / Tools
- **Language**: C#.
- **Framework**: .NET Framework.
- **UI Framework**: Windows Presentation Foundation (WPF).
- **Data Management**: CSV file format for data persistence.
