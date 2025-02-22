using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Lms.Models
{
    [PrimaryKey(nameof(Id)), Index(nameof(Username), IsUnique = true)]
    public class User
    {
        // Fields
        public int Id { get; set; }

        [Cli.Parameter]
        public required string Username { get; set; }
    }
}
