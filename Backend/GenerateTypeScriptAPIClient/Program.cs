using GenerateTypeScriptAPIClient;
using NSwag;
using NSwag.CodeGeneration.OperationNameGenerators;
using NSwag.CodeGeneration.TypeScript;

var document = await OpenApiDocument.FromUrlAsync("https://localhost:7116/swagger/v1/swagger.json");

var settings = new TypeScriptClientGeneratorSettings
{
    ClassName = "{controller}Client",
    Template = TypeScriptTemplate.Fetch,
    OperationNameGenerator = new MultipleClientsFromMethodName()
};

var generator = new TypeScriptClientGenerator(document, settings);
var typeScriptCode = generator.GenerateFile();

// Your project's name
const string targetFolder = "Frontend/api-client.ts"; // The folder within your project where the file should be saved

// Find the project root directory (assuming the current directory is somewhere within the project)
var projectDir = Directory.GetCurrentDirectory();
while (!Directory.EnumerateDirectories(projectDir, "Frontend").Any())
{
    projectDir = Directory.GetParent(projectDir)?.FullName ??
                 throw new InvalidOperationException("Could not find project root.");
}

// Compute the path to the target folder
var path = Path.Combine(projectDir, targetFolder);

File.WriteAllText(path, typeScriptCode);

Console.WriteLine($"File generated at: {path}");
Console.WriteLine("TypeScript client generated.");