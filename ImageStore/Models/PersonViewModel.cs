using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageStore.Models
{
    public class PersonViewModel
    {
        public string Name { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
