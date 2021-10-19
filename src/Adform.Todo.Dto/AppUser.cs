using System;
using System.Collections.Generic;
using System.Text;

namespace Adform.Todo.Dto
{
    public class AppUser
    {
        /// <summary>
        /// User's Name
        /// </summary>
        /// <example>admin</example>
        public string Name { get; set; }
        /// <summary>
        /// User's password
        /// </summary>
        /// <example>admin</example>
        public string Password { get; set; }
    }
}
