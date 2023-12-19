namespace PhotoMetadata.Utility;

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;

public class ConsoleClient
{
    private string command;
    private readonly StringBuilder standardOutput = new();
    private readonly StringBuilder standardError = new();

    public string Command { get => this.command; set => this.command = Environment.ExpandEnvironmentVariables(value); }
    public string Arguments { get; set; }
    public int ExitCode { get; set; }
    public string StandardOutput => this.standardOutput.ToString();
    public string StandardError => this.standardError.ToString();

    public ConsoleClient()
    {
    }

    public ConsoleClient(string command)
    {
        this.Command = command;
    }

    public void Reset()
    {
        this.ExitCode = 0;
        this.standardOutput.Clear();
        this.standardError.Clear();
    }

    public void Run(IEnumerable<string> args)
    {
        this.Arguments = string.Join(" ", args);

        this.Run();
    }

    public void Run(params string[] args)
    {
        this.Arguments = string.Join(" ", args);

        this.Run();
    }

    public void Run(string args)
    {
        this.Arguments = args;

        this.Run();
    }

    public void Run()
    {
        this.Reset();

        using Process process = new();
        process.StartInfo.FileName = this.Command;
        process.StartInfo.Arguments = this.Arguments;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.ErrorDialog = false;
        process.StartInfo.CreateNoWindow = true;

        process.OutputDataReceived += (sender, eventArgs) =>
        {
            if (eventArgs.Data != null)
            {
                this.standardOutput.AppendLine(eventArgs.Data);
            }
        };
        process.ErrorDataReceived += (sender, eventArgs) =>
        {
            if (eventArgs.Data != null)
            {
                this.standardError.AppendLine(eventArgs.Data);
            }
        };

        if (process.Start() == false)
        {
            this.standardError.AppendLine(CultureInfo.InvariantCulture, $"Error starting \"{this.Command}\"");
        }
        else
        {
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            this.ExitCode = process.ExitCode;
        }
    }
}
