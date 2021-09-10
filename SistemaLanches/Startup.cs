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
            
            // Configuraçao do banco de dados
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Session
            services.AddMemoryCache();
            services.AddSession();

            // Registrando o meu serviço para fazer injeção de dependência no meu controlador
            // AddTransient - objeto do serviço será criado toda vez que for requisitado
            // AddSingleton - objeto do serviço será criado para todas as requisições que forem feitas
            //                todas as requisições retornam o mesmo objeto
            // AddScoped - serviço será criado para cada requisição
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ILancheRepository, LancheRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // para ter acesso a sessão no contexto
            // evita criar carrinhos duplicados - Se houver duas requisições simultâneas vai gerar instâncias diferentes
            services.AddScoped(cp => CarrinhoCompra.GetCarrinho(cp));


            // MVC - criado pelo sistema
            // services.AddControllersWithViews(); alterado para visualizar as atualização das páginas enquanto compila
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
