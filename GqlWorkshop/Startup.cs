﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GqlWorkshop.DbModel;
using GqlWorkshop.Gql;
using GqlWorkshop.Gql.Schema;
using GraphQL.Conventions;
using GraphQL.DataLoader;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Query = GqlWorkshop.Gql.Schema.Query;

namespace GqlWorkshop
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
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddSingleton(CreateGraphQLEngine());
            services.AddScoped<IDependencyInjector, Injector>();
            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<DataLoaderContext>();

            services.AddMediatR(typeof(Startup).Assembly);
        }

        private static GraphQLEngine CreateGraphQLEngine()
        {
            return new GraphQLEngine()
                .WithQuery<Query>()
                .WithMutation<Mutation>()
                .BuildSchema();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
