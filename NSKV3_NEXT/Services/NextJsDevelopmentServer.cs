namespace NSKV3_NEXT.Services
{
    using System.Diagnostics;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class NextJsDevelopmentServer : IHostedService
    {
        private Process _nextJsProcess;
        private readonly ILogger<NextJsDevelopmentServer> _logger;

        public NextJsDevelopmentServer(ILogger<NextJsDevelopmentServer> logger)
        {
            _logger = logger;
        }
        /*
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запуск сервера разработки Next.js...");

            _nextJsProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Users\Turgambayev.G\AppData\Roaming\npm\pnpm.cmd",
                    Arguments = "next dev",
                    WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp"),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                },
                EnableRaisingEvents = true
            };

            _nextJsProcess.Start();
            _nextJsProcess.BeginOutputReadLine();
            _nextJsProcess.BeginErrorReadLine();

            _nextJsProcess.OutputDataReceived += (sender, args) => _logger.LogInformation(args.Data);
            _nextJsProcess.ErrorDataReceived += (sender, args) => _logger.LogError(args.Data);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Остановка сервера разработки Next.js...");

            if (!_nextJsProcess.HasExited)
            {
                _nextJsProcess.Kill(entireProcessTree: true);
                _nextJsProcess.WaitForExit();
            }

            _nextJsProcess.Dispose();
            return Task.CompletedTask;
        }
        */
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Запуск сервера разработки Next.js...");

            _nextJsProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = "/c pnpm run dev",
                    WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp"),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };
            /*
            _nextJsProcess.OutputDataReceived += (sender, args) =>
            {
                
                _logger.LogInformation(args.Data);
                //if (args.Data != null && args.Data.Contains("started server on")) // Убедитесь, что это сообщение, которое выводит ваш сервер Next.js при успешном запуске
                {
                    OpenBrowser("http://localhost:3000"); // Замените URL на актуальный адрес сервера разработки Next.js
                }
                
            };
            */
            

            

            _nextJsProcess.Start();
            _nextJsProcess.BeginOutputReadLine();
            _nextJsProcess.BeginErrorReadLine();

            Task.Delay(5000); // Задержка на 5 секунд
            OpenBrowser("http://localhost:3000");

            _nextJsProcess.Exited += (sender, args) =>
            {
                _logger.LogInformation($"Процесс Next.js завершился с кодом {((Process)sender).ExitCode}");
            };

            return Task.CompletedTask;
        }

        private void OpenBrowser(string url)
        {
            try
            {
                _logger.LogInformation($"Открытие {url} в браузере...");
                //Process.Start("chrome", url);
                //Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", url);

                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Важно для .NET Core и .NET 5/6+
                });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Не удалось открыть браузер: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Остановка сервера разработки Next.js...");

            if (!_nextJsProcess.HasExited)
            {
                _nextJsProcess.Kill(entireProcessTree: true);
                _nextJsProcess.WaitForExit();
            }

            _nextJsProcess.Dispose();
            return Task.CompletedTask;
        }
    }

}
