using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RetoTecnicoBCP.DataContext;
using RetoTecnicoBCP.Domain;
using RetoTecnicoBCP.Service;
using RetoTecnicoBCP.Util;
using System.Text;

namespace RetoTecnicoBCP
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
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("MyDb")));
            services.AddTransient<IExchangeRateService, ExchangeRateService>();
            services.AddTransient<ITokenAuth, TokenAuth>();
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("SecretKey"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            DataMemory(context);

        }

        public static void DataMemory(AppDbContext context) {

            ExchangeRate obj1 = new ExchangeRate()
            {
                Id = "1",
                ExchangeRateMoney = 3.85,
                Name = "Dolar"
            };

            ExchangeRate obj2 = new ExchangeRate()
            {
                Id = "2",
                ExchangeRateMoney = 4.29,
                Name = "Euro"
            };

            ExchangeRate obj3 = new ExchangeRate()
            {
                Id = "3",
                ExchangeRateMoney = 5.37,
                Name = "Peso Mexicano"
            };


            context.ExchangeRate.Add(obj1);
            context.ExchangeRate.Add(obj2);
            context.ExchangeRate.Add(obj3);
            context.SaveChanges();
        }
    }
}
