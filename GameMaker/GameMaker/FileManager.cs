using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMaker
{
    internal class FileManager
    {
        string fileName = "TeamData.csv";
        /// <summary>
        /// Takes a provided array and writes its contents to a csv file
        /// in a comma delimited format.
        /// </summary>
        /// <param name="teamData">An array of TeamDetails objects</param>
        public void WriteDataToFile(TeamDetails[] teamData)
        {
            // The using statement generates the class to connect to a file(streamwriter) and
            // uses it as the structure runs. Once the using statement finishes, the resource
            // will be automatically disconnected and destroyed.
            using (var writer = new StreamWriter(fileName))
            {
                //Cycyles through each element in the array.
                foreach (var team in teamData)
                {
                    //Each property of each entry is printed ona single line with commas to
                    //separate them (Comma Delimmited File or Comma Separated Values(CSV)).
                    writer.WriteLine(team.TeamName + "," + team.ContactName + "," + team.ContactPhone + 
                        "," + team.ContactEmail + "," + team.CompetitionPoints);
                }
            }
        }
        /// <summary>
        /// Reads the data from the specified file and breaks it back down into TeamDetails objects
        /// which are then placed in a list.
        /// </summary>
        /// <returns>A list of DataModel objects representing the lines in the file.</returns>
        public List<TeamDetails> ReadDataFromFile()
        {
            // List to store the data from the file
            List<TeamDetails> teamList = new List<TeamDetails>();
            // The using statement generates the class to connect to a file(streamreader) and
            // uses it as the structure runs. Once the using statement finishes, the resource
            // will be automatically disconnected and destroyed.

            // Check if file even exists, create one if it's missing!
            if (!File.Exists(fileName))
            {
                // Create empty file.
                File.Create(fileName).Close();
                // Return the empty file.
                return teamList;
            }

            using (var reader = new StreamReader(fileName))
            {
                // Variable to store each line as it is processd
                string line;
                // This while loop reads the next line from the file and stores it in the line
                // variable. Once done it uses the IsNullOrWhiteSpace method to check if the line
                // has text or not. If it is not empty the while statement runs.
                while (String.IsNullOrWhiteSpace(line = reader.ReadLine()) == false)
                {
                    // Splits the line into sections where each section its the text between the commas
                    // and line ends. Once done the resulting temp array will hold each piece of data in each of the
                    // separate elements.
                    string[] temp = line.Split(',');
                    // Each element of the temp array into a data model in the required order.
                    TeamDetails details = new TeamDetails(temp[0], temp[1], temp[2], temp[3],int.Parse(temp[4]));
                    // Adds the new DataModel to our list.
                    teamList.Add(details);
                }
            }
            return teamList;
        }
    }
}