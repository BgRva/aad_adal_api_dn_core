using AADx.Common.Models;

namespace AADx.TodoApi.Data
{
    public class ItemValidator
    {
        public bool isValid(TodoItem todo)
        {
            var valid = true;
            
            if (string.IsNullOrWhiteSpace(todo.Description)) 
                valid &= false;
                        
            if (string.IsNullOrWhiteSpace(todo.Owner)) 
                valid &= false;
            
            if (todo.Team == TeamType.None) 
                valid &= false;
            
            if (todo.Faction == FactionType.None) 
                valid &= false;

            if ((todo.Team == TeamType.WaterBuffaloes ||
                 todo.Team == TeamType.Goons ||
                 todo.Team == TeamType.Mutants ) && todo.Faction != FactionType.Horde)
                valid &= false;
            
            if ((todo.Team == TeamType.Sparkles ||
                 todo.Team == TeamType.Flowers ||
                 todo.Team == TeamType.ArgyleSox ) && todo.Faction != FactionType.Alliance)
                valid &= false;

            return valid;
        }
        
    }
}