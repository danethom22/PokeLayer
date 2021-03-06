using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeLayer.Application.ApiClients.PokeApi;
using PokeLayer.Application.ApiClients.FunTranslationsApi;
using PokeLayer.Application.Services;

namespace PokeLayer.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();
      services.AddHttpClient();
      services.AddResponseCaching();
      services.AddSwaggerGen();
      services.AddScoped<IPokeApiClient, PokeApiClient>();
      services.AddScoped<IFunTranslationsApiClient, FunTranslationsApiClient>();
      services.AddScoped<IPokemonService, PokemonService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseResponseCaching();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
