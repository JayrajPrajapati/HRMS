using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLibrary.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProspectModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ProspectID { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the dob.
        /// </summary>
        /// <value>
        /// The dob.
        /// </value>
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DOB { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>
        /// The mobile.
        /// </value>
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0: ##########}")]
        public decimal Mobile { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ProspectModel"/> is gender.
        /// </summary>
        /// <value>
        ///   <c>true</c> if gender; otherwise, <c>false</c>.
        /// </value>
        public bool Gender { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value> 
        [DisplayName("File Name")]
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public byte Status { get; set; } = 1;
        /// <summary>
        /// Gets or sets the skill.
        /// </summary>
        /// <value>
        /// The skill.
        /// </value>
        public int[] Skill { get; set; }

        /// <summary>
        /// Gets or sets the scheduled date.
        /// </summary>
        /// <value>
        /// The scheduled date.
        /// </value>
        public DateTime SchduleDate { get; set; }
    }
}
