using AccesoDatos.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;

namespace VideoClubWinForms;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();
        services.AddTransient<Func<AplicationDbContext>>(
            _ => () => new AplicationDbContext());
        services.AddTransient<MainForm>();

        using var provider = services.BuildServiceProvider();
        using (var db = provider.GetRequiredService<Func<AplicationDbContext>>()())
            db.Database.EnsureCreated();

        Application.Run(provider.GetRequiredService<MainForm>());
    }
}
