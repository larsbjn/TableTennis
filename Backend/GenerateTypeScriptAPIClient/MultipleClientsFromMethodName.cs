using NSwag;
using NSwag.CodeGeneration.OperationNameGenerators;

namespace GenerateTypeScriptAPIClient;

public class MultipleClientsFromMethodName : IOperationNameGenerator
{
    public string GetClientName(OpenApiDocument document, string path, string httpMethod, OpenApiOperation operation)
    {
        return operation.Tags.FirstOrDefault() ?? "DefaultClient";
    }

    public string GetOperationName(OpenApiDocument document, string path, string httpMethod, OpenApiOperation operation)
    {
        return $"{operation.OperationId}";
    }

    public bool SupportsMultipleClients { get; } = true;
}