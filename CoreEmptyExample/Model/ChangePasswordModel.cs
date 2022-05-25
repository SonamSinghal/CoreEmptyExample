using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample.Model
{
    public class ChangePasswordModel
    {
        [Required, DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
