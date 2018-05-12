namespace AADx.Common.Auth
{
  public class AzureAdOptions
  {
    public string Instance { get; set; }
    
    /// <summary>
    /// Gets or sets the Audience id, which is the id of the Client
    /// </summary>
    public string Audience { get; set; }
    public string Domain { get; set; }
    public string TenantId { get; set; }
    
  }
}
