using AADx.Common.Models;
using Microsoft.EntityFrameworkCore;
  
  namespace AADx.EventApi.Data
   {
       public class EventContext : DbContext
       {
           public EventContext(DbContextOptions<EventContext> options): base(options)
           {
           }
   
           public DbSet<EventItem> EventItems { get; set; }
       }
   }