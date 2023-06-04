using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Neqatcom.Infra.Repository;
using Neqatcom.Infra.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheNeqatcomApp.API.Controllers;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;
using TheNeqatcomApp.Infra.Common;
using TheNeqatcomApp.Infra.Repository;
using TheNeqatcomApp.Infra.Service;

namespace TheNeqatcomApp.API
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
            services.AddDbContext<DbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProdConnection"));
            });
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("CorsPolicy",
               builder =>
               { builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                   builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                  
               });
            });
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKeyNeqatcommmmmmmmmmm"))
                    };
                });
            services.AddControllers();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IPurchasingRepository, PurchasingRepository>();
            services.AddScoped<IPurchasingService, PurchasingService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDBContext, DBContext>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ILoaneeRepository, LoaneeRepository>();
            services.AddScoped<ILoaneeService, LoaneeService>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<ILenderStoreRepository, LenderStoreRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ILenderStoreService, LenderStoreService>();
            services.AddControllers();
            services.AddScoped<INotificationsService, NotificationsService>();
            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddHttpClient<ZoomController>();
            services.AddScoped<HttpClient>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ILoaneeRepository, LoaneeRepository>();
            services.AddScoped<ILoaneeService, LoaneeService>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<ILenderStoreRepository, LenderStoreRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ILenderStoreService, LenderStoreService>();
            services.AddControllers();
            //services.AddScoped<ILoaneeComplaintsRepository, LoaneeComplaintsRepository>();
            //services.AddScoped<ILoaneeComplaintsService, LoaneeComplaintsService>();
            services.AddScoped<INotificationsService, NotificationsService>();
            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddHttpClient<ZoomController>();
            services.AddScoped<HttpClient>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
