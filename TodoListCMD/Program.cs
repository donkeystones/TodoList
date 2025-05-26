using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Persistence;
using Services;
using TodoListCMD;
using TodoListCMD.Menu;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(cfg =>
    {
        cfg.AddJsonFile("config.json", optional: false, reloadOnChange: true);
    }).ConfigureServices((ctx, services) =>
    {
        // Bind strongly-typed options
        services.Configure<StorageOptions>(
            ctx.Configuration.GetSection("Storage"));

        // Register repository with a factory that reads the bound option
        services.AddSingleton<ITodoRepository>(sp =>
        {
            var opt = sp.GetRequiredService<IOptions<StorageOptions>>().Value;
            return new TodoJsonRepository(opt.JsonStorageLocation);
        });

        // Register the service
        services.AddSingleton<ITodoService, TodoService>();

        // Register the menu (no statics any more)
        services.AddSingleton<MainMenu>();
    }).Build();
    
    var menu = host.Services.GetRequiredService<MainMenu>();
    MainMenu.Run();