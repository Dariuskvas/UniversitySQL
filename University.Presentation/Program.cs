// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using University.BusinessLogic;
using University.BusinessLogic.Interfaces;
using University.BusinessLogic.Service;
using University.Presentation;
using University.Presentation.Interfaces;
using University.Respository;
using University.Respository.Interfaces;
using University.Respository.Repositories;

/// <summary>
/// Programa skirta diebti su SQL lentelemis: Departament, Lecture, Student. Rysiai: Departament su Student - (1:N), Departament - lecture - (m:n).
/// Studentas gali tureti viena departamenta ir visas departamento paskaitas.
/// Programa pasileidus, tikrina ar DB yra tuscia. Jei tuscia pradinius duomenis pasiima is JSNO firstData.
/// Puriant ar koreguojant jei bent viena reiksme paliekama tuscia, kodas uzskaitys kaip "persigalvojima" ir veiksmo nevykdys. Savotiskas ESC is funkcijos.
/// 
/// Sukurus/koreguojant studenta paskaitos priskiriamos pagal fakultetus, tai yra, kad priskyrus fakulteta, sudententui priskiriamos visos departamento paskaitos.
/// Sukuriant/koreguojant paskaitas, reikia pasirinkti kurie fakultetai tures paskaita.
/// Sukuriant fakulteta papildomai priskirimu pasirinkti nereikia, priskirimus galima padaryti koreguojant fakulteta. 
/// 
/// Ataskaitos isvedamos i konsole visos is karto: 1) Studentai pagal departamenta 2) paskaitos pagal departamenta 3) Paskaitos pagal studenta
/// Departament, 
/// </summary>


internal class Program
{
    private static void Main(string[] args)
    {

     var config = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(AppContext.BaseDirectory, "../../.."))
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging => logging.ClearProviders())
            .ConfigureServices((_, services) =>
            {
                services.AddDbContext<UniversityDbContext>(options =>
                {
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                        .LogTo(Console.WriteLine, LogLevel.None);
                }, ServiceLifetime.Scoped);
                services.AddTransient<ILectureRespository, LectureRespository>();
                services.AddTransient<IStudentRespository, StudentRespository>();
                services.AddTransient<IDepartamentRespository, DepartamentRespository>();
                services.AddTransient<IUtilities, Utilities>();
                services.AddTransient<IMainMenu, MainMenu>();
                services.AddTransient<IInputForNewSubjects, InputForNewSubjects>();
                services.AddTransient<IDepartamentService, DepartamentService>();
                services.AddTransient<ILectureService, LectureService>();
                services.AddTransient<IStudentService, StudentService>();
                services.AddTransient<IReportPresenter, ReportPresenter>();
                services.AddTransient<IInputForSubjectCorection, InputForSubjectCorection>();
                services.AddTransient<IDbService, DbService>();
            })
            .Build();


        var productImporter = host.Services.GetRequiredService<IMainMenu>();
        productImporter.CheckIsDbEmptyBeforStart();
        host.Run();
    }
}