using Logistics.Services;
using Logistics.Services.Edges.Commands;
using Logistics.Services.Edges.Queries;
using Logistics.Services.Nodes.Commands;
using Logistics.Services.Nodes.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Logistics.Web.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("AppPolicy",
                    builder =>
                    {
                        builder.AllowAnyHeader();
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                    });
            });

            services.AddControllers();

            services.AddTransient<IGetNodesQuery, GetNodesDbQuery>();
            services.AddTransient<IGetEdgesQuery, GetEdgesDbQuery>();
            services.AddTransient<IGetNodeByIdQuery, GetNodeByIdDbQuery>();
            services.AddTransient<ICreateNodeCommand, CreateNodeDbCommand>();
            services.AddTransient<IUpdateNodeCommand, UpdateNodeDbCommand>();
            services.AddTransient<IDeleteNodeCommand, DeleteNodeDbCommand>();
            services.AddTransient<IDeleteEdgeCommand, DeleteEdgeDbCommand>();
            services.AddTransient<ICreateEdgeCommand, CreateEdgeDbCommand>();
            services.AddTransient<IGetLogisticCenterIdQuery, GetLogisticCenterIdDbQuery>();
            services.AddSingleton<IDbConnectionFactory>(
                sp => new DbConnectionFactory(Configuration.GetConnectionString("DefaultConnection"))
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(
                options => options.SetIsOriginAllowed(x => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
