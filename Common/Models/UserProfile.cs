﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AADx.Common.Models
{
    public class UserProfile : IValidatableObject
    {
        public long Id { get; set; }
        
        [Required]
        public Guid OID { get; set; }
                        
        [Required]
        public string UPN { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public TeamType Team { get; set; }
        
        public FactionType Faction { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Team == TeamType.None) 
                yield return new ValidationResult("Team is required", new[] { "Team" });
            
            if (Faction == FactionType.None) 
                yield return new ValidationResult("Faction is required", new[] { "Faction" });

            if ((Team == TeamType.WaterBuffaloes || Team == TeamType.Goons || Team == TeamType.Mutants)
                && Faction != FactionType.Horde)
                yield return new ValidationResult(
                    $"Team {Team} is in Faction Horde",new[] { "Team" });
            
            if ((Team == TeamType.Sparkles || Team == TeamType.Flowers || Team == TeamType.ArgyleSox)
                && Faction != FactionType.Alliance)
                yield return new ValidationResult(
                    $"Team {Team} is in Faction Alliance",new[] { "Team" });
        }
    }
}