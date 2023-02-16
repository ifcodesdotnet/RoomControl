using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoomControl.API.Abstractions;
using RoomControl.API.Factories;
using RoomControl.API.Services;

namespace RoomControl.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences
            //should be using some sort of dns ... something that wont change with dhcp...
            builder.Services.AddSingleton<IDeviceControl>(new WemoService("http://10.0.0.4")); 

            //what if i have multiple wemo devices....
            //this probably won't work since this is a singleton
            //builder.Services.AddSingleton<IDeviceControl>(new WemoService("http://10.0.0.4")); 


            builder.Services.AddSingleton<IDeviceControl>(new SonosService("10.0.0.3"));


            
            builder.Services.AddTransient<IDeviceControlFactory, DeviceControlFactory>();


            //https://thecodeblogger.com/2022/09/16/net-dependency-injection-one-interface-and-multiple-implementations/
            //going to actual need a factory for this...





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}