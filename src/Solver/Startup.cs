using System;
using Microsoft.Extensions.DependencyInjection;

namespace SudokuSolver
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DataParsers.TextFile.Validators.ContentsArrayValidator>();
        }
    }
}