using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SistemaLanches.Context;
using SistemaLanches.Models;
using SistemaLanches.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaLanches
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
            
            // Configura�ao do banco de dados
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Session
            services.AddMemoryCache();
            services.AddSession();

            // Registrando o meu servi�o para fazer inje��o de depend�ncia no meu controlador
            // AddTransient - objeto do servi�o ser� criado toda vez que for requisitado
            // AddSingleton - objeto do servi�o ser� criado para todas as requisi��es que forem feitas
            //                todas as requisi��es retornam o mesmo objeto
            // AddScoped - servi�o ser� criado para cada requisi��o
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ILancheRepository, LancheRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // para ter acesso a sess�o no contexto
            // evita criar carrinhos duplicados - Se houver duas requisi��es simult�neas vai gerar inst�ncias diferentes
            services.AddScoped(cp => CarrinhoCompra.GetCarrinho(cp));


            // MVC - criado pelo sistema
            // services.AddControllersWithViews(); alterado para visualizar as atualiza��o das p�ginas enquanto compila
            // incluir dependencia - Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Session
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
