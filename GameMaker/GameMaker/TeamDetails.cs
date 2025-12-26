using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMaker
{
    /// <summary>
    /// Sets up getter and setter properties for the Teams.
    /// </summary>
    public class TeamDetails
    {
        public string TeamName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int CompetitionPoints { get; set; }

        // Default Construcor.
        public TeamDetails() { }

        // Constructor with parameters.
        public TeamDetails(string teamName, string contactName, string contactPhone, string contactEmail, int competitionPoints)
        {
            TeamName = teamName;
            ContactName = contactName;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
            CompetitionPoints = competitionPoints;
        }
    }
}
