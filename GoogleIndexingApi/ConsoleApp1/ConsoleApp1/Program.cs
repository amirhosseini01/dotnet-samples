// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Services;
string jsonPrivateKey = @"C:\Users\Amir\Desktop\New folder\ConsoleApp1\ConsoleApp1\PrivateKey\testsampledemo-7e56efcf8110.json";
var googleSingleIndexingService = new GoogleSingleIndexingService(jsonPrivateKey);

var updateResult = await googleSingleIndexingService.AddOrUpdateGoogleIndex(@"https://someurl");
Console.WriteLine("Hello, World!");
