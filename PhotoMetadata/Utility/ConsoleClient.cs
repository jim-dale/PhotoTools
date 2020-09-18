using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PhotoMetadata
{
    public class ConsoleClient
    {
        private string _command;
        private readonly StringBuilder _standardOutput = new StringBuilder();
        private readonly StringBuilder _standardError = new StringBuilder();

        public string Command { get => _command; set => _command = Environment.ExpandEnvironmentVariables(value); }
        public string Arguments { get; set; }
        public int ExitCode { get; set; }
        public string StandardOutput => _standardOutput.ToString();
        public string StandardError => _standardError.ToString();

        public ConsoleClient()
        {
        }

        public ConsoleClient(string command)
        {
            Command = command;
        }

        public void Reset()
        {
            ExitCode = 0;
            _standardOutput.Clear();
            _standardError.Clear();
        }

        public void Run(IEnumerable<string> args)
        {
            Arguments = string.Join(" ", args);

            Run();
        }

        public void Run(params string[] args)
        {
            Arguments = string.Join(" ", args);

            Run();
        }

        public void Run(string args)
        {
            Arguments = args;

            Run();
        }

        public void Run()
        {
            Reset();

            using var process = new Process();
            process.StartInfo.FileName = Command;
            process.StartInfo.Arguments = Arguments;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.ErrorDialog = false;
            process.StartInfo.CreateNoWindow = true;

            process.OutputDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null)
                {
                    _standardOutput.AppendLine(eventArgs.Data);
                }
            };
            process.ErrorDataReceived += (sender, eventArgs) =>
            {
                if (eventArgs.Data != null)
                {
                    _standardError.AppendLine(eventArgs.Data);
                }
            };

            if (process.Start() == false)
            {
                _standardError.AppendLine($"Error starting \"{Command}\"");
            }
            else
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();

                ExitCode = process.ExitCode;
            }
        }
    }
}
