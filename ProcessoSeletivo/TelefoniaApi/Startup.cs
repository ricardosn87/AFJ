using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using TelefoniaApi.Data;
using TelefoniaApi.Data.AutoMappings;
using TelefoniaApi.Data.Repository;
using TelefoniaApi.Domain.Entities;
using TelefoniaApi.Domain.Interfaces.Repository;
using TelefoniaApi.Domain.Interfaces.Service;
using TelefoniaApi.Domain.Services;


namespace TelefoniaApi
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Telefonia API",
                    Description = "Projeto criado com tecnologia ASP.NET Core Web API para processo seletivo.",                    
                    Contact = new OpenApiContact
                    {
                        Name = "Ricardo Nogueira",
                        Email = "ricardosn87@hotmail.com"

                    }
                });
                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);

            });

            services.AddDbContext<BaseContext>(opt => opt.UseInMemoryDatabase("Telefone"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            services.AddControllers();

            services.AddTransient<IPlanoService, PlanoService>();
            services.AddTransient<IPlanoRepository, PlanoRepository>();

            services.AddTransient<IOperadoraService, OperadoraService>();
            services.AddTransient<IOperadoraRepository, OperadoraRepository>();

            services.AddTransient<IDDDService, DDDService>();
            services.AddTransient<IDDDRepository, DDDRepository>();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
         
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo JWT Api");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();              
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });          

            InitializeData();
        }
        /// <summary>
        /// InitializeData - Método que inicializa dados da aplicação
        /// </summary>
        public static void InitializeData()
        {

            var builder = new DbContextOptionsBuilder<BaseContext>();
            builder.UseInMemoryDatabase("Telefone");
            var options = builder.Options;
            using (var baseContext = new BaseContext(options))
            {
                var operadoraVivo = new Operadora
                {
                    Nome = "Vivo"
                };

                var operadoraOi = new Operadora
                {
                    Nome = "Oi"
                };

                var operadoraClaro = new Operadora
                {
                    Nome = "Claro"
                };

                baseContext.Operadoras.Add(operadoraVivo);
                baseContext.Operadoras.Add(operadoraOi);
                baseContext.Operadoras.Add(operadoraClaro);

                var ddd21 = new DDD
                {
                    Numero = "21",
                    Operadora = operadoraVivo

                };
                var ddd22 = new DDD
                {
                    Numero = "22",
                    Operadora = operadoraVivo

                };
                var ddd11 = new DDD
                {
                    Numero = "11",
                    Operadora = operadoraOi

                };
                var ddd43 = new DDD
                {
                    Numero = "43",                    
                    Operadora = operadoraClaro

                };
             
                List<DDD> dDDs = new List<DDD>();
                dDDs.Add(ddd21);
                dDDs.Add(ddd22);
                dDDs.Add(ddd11);
                dDDs.Add(ddd43);


                baseContext.DDDs.AddRange(dDDs);

                var plano1 = new Plano
                {
                    CodigoPlano = "10",
                    FranquiaInternet = 100,
                    Minutos = 1000,                   
                    Tipo = Tipo.Controle,
                    Valor = 150.00,
                    DDD = ddd21

                };
                var plano2 = new Plano
                {
                    CodigoPlano = "11",
                    FranquiaInternet = 100,
                    Minutos = 1000,
                    Tipo = Tipo.Pos,
                    Valor = 150.00,
                    DDD = ddd22

                };
                baseContext.Planos.Add(plano1);
                baseContext.Planos.Add(plano2);

                baseContext.SaveChanges();
            }
        }
    }
}
