using AADx.Common.Models;

namespace AADx.EventApi.Data
{
    public class EventValidator
    {        
        public bool isValid(EventItem togo)
        {
            var valid = true;
            
            if (string.IsNullOrWhiteSpace(togo.Description)) 
                valid &= false;
                        
            if (string.IsNullOrWhiteSpace(togo.Owner)) 
                valid &= false;
            
            if (togo.Team == TeamType.None) 
                valid &= false;
            
            if (togo.Faction == FactionType.None) 
                valid &= false;

            if ((togo.Team == TeamType.WaterBuffaloes ||
                 togo.Team == TeamType.Goons ||
                 togo.Team == TeamType.Mutants ) && togo.Faction != FactionType.Horde)
                valid &= false;
            
            if ((togo.Team == TeamType.Sparkles ||
                 togo.Team == TeamType.Flowers ||
                 togo.Team == TeamType.ArgyleSox ) && togo.Faction != FactionType.Alliance)
                valid &= false;

            return valid;
        }
    }
}