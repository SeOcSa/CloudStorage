using Core.Entities;
using Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorage.ViewModels
{
    public class DetailsViewModel
    {
        public FileInfo fileInfo { get; set; }

        public string photoUrl { get; set; }
    }
}
