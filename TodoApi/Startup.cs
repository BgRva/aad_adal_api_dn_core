using AADx.Common.Auth;
using AADx.TodoApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;

namespace AADx.TodoApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
      services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("UserList"));
      
      // --
      services.AddCors(options =>
      {
        options.AddPolicy("AllowSpecificOrigin",
          builder => builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
            .AllowAnyHeader()
            .WithMethods("GET", "PUT", "POST", "DELETE", "OPTION"));
      });

      // --
      services.AddAuthentication(options =>
        {
          options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddAzureAdBearer(options => Configuration.Bind("AzureAd", options));
      
      // --
      services
        .AddMvc()
        .AddJsonOptions(options =>
        {
          options.SerializerSettings.Converters.Add(new StringEnumConverter());
          options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        });

      // --
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info
        {
          Version = "v1",
          Title = "Todo API",
          Description = "API for Tracking Simple Tasks",
          TermsOfService = "None",
          Contact = new Contact
          {
            Name = "M Pastore",
            Email = "michael.pastore@plus3it.com",
            Url = "www.plus3itsystems.com"
          }
        });
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

      app.UseCors("AllowSpecificOrigin");

      // add the authentication middleware
      app.UseAuthentication();
      
      app.UseMvc();

      app.UseSwagger();
      app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1"); });
    }
  }
}
