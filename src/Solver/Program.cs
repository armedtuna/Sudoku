using System;
using Microsoft.Extensions.DependencyInjection;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            var parser = provider.GetRequiredService<DataParsers.TextFile.TextFileParser>();
            var cells = parser.Load("src/solver/data/01.txt");

            // todo-at: thinking about how to build a render class / interface

            var boardBuilder = provider.GetRequiredService<DataParsers.General.BoardBuilder>();
            var board = boardBuilder.BuildBoard(cells);
            
            var solver = provider.GetRequiredService<Solver.SolveBoard>();
            solver.Solve(board);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DataParsers.TextFile.Validators.ContentsArrayValidator>();
            services.AddSingleton<DataParsers.General.BoardBuilder>();
            services.AddSingleton<DataParsers.TextFile.TextFileParser>();
            services.AddSingleton<Solver.SolveBoard>();
        }
    }
}
