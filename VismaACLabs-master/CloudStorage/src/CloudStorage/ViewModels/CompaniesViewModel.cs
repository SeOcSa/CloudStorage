using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.ViewModels
{
    public class CompaniesViewModel
    {
        [Display(Name = "Search")]
        public string Query { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}
