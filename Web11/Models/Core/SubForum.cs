using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Web11.Models.Core
{
    public class SubForum
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public List<string> Rules { get; set; }

        [ForeignKey("ResponsibleModerator")]
        public int ResponsibleModerator_Id { get; set; }

        public User ResponsibleModerator { get; set; }

    }
}