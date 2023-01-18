using System.Diagnostics;

Console.WriteLine("Hello, World!");
Console.WriteLine("show bash? (y/n)");
bool showBash = Console.ReadLine()  == "y";
Console.WriteLine("show output? (y/n)");
bool showOutput = Console.ReadLine() == "y";

while (true)
{
    Console.WriteLine("write your command:");

    string? userInput = showBash ? Console.ReadLine() : ReadUserInputInvisible();

    if (string.IsNullOrWhiteSpace(userInput))
        continue;

    string res = ExecuteBashCommand(userInput);
    if (showOutput)
        Console.WriteLine(res);
}

static string ExecuteBashCommand(string command)
{
    command = command.Replace("\"", "\"\"");
    var proc = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = "-c \"" + command + "\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        }
    };
    proc.Start();
    proc.WaitForExit();
    return proc.StandardOutput.ReadToEnd();
}

static string ReadUserInputInvisible()
{
    string userInput = string.Empty;
    while (true)
    {
        var key = System.Console.ReadKey(true);
        if (key.Key == ConsoleKey.Enter)
            break;
        userInput += key.KeyChar;
    }
    return userInput;
}