using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Models
{
    /// <summary>
    /// CompanyResponse
    /// </summary>
    public class CompanyResponseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the prospect identifier.
        /// </summary>
        /// <value>
        /// The prospect identifier.
        /// </value>
        public Nullable<int> ProspectID { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Nullable<byte> Status { get; set; }
        /// <summary>
        /// Gets or sets the schedule date.
        /// </summary>
        /// <value>
        /// The schedule date.
        /// </value>
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ScheduleDate { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        
        public Nullable<int> UserID { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public Nullable<System.DateTime> CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public Nullable<int> CreatedBy { get; set; }

        /// <summary>
        /// Gets the schedule date formated.
        /// </summary>
        /// <value>
        /// The schedule date formated.
        /// </value>
        public string ScheduleDateFormated
        {
            get
            {
                if (ScheduleDate != null)
                    return ScheduleDate.ToString("MM/dd/yyyy");
                return string.Empty;
            }
        }

    }
}
