using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AADx.Common.Models
{
    public class EventItem
    {
        public long Id { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        public string Owner { get; set; }
        
        public TeamType Team { get; set; }
        
        public FactionType Faction { get; set; }      
    }
}