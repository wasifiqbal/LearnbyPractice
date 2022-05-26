// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using Learn.gRPC;

Console.WriteLine("GRPC Client for Product Info!");
/// <summary>
/// Simple gRPC Method - GetProduct
/// </summary>
var channel = GrpcChannel.ForAddress("http://localhost:5000");
var productclient = new ProductInfo.ProductInfoClient(channel);
var getProductResult = await productclient.GetProductAsync(new ProductRequest() { Id = 1 });
Console.WriteLine($"Product Info: " +
    $"\n\tName: {getProductResult.Name}, " +
    $"\n\tDescription: {getProductResult.Description}, " +
    $"\n\tPrice: {getProductResult.PricePerUnit}");

/// <summary>
/// Server Stream gRPC Method - GetProductList
/// </summary>
var getProductListResult = productclient.GetProductList(new ProductListRequest() { Count = 5 });
int counter = 0;
while (await getProductListResult.ResponseStream.MoveNext())
{
    var product = getProductListResult.ResponseStream.Current;

    Console.WriteLine($"Product Info {++counter}: " +
    $"\n\tName: {product.Name}, " +
    $"\n\tDescription: {product.Description}, " +
    $"\n\tPrice: {product.PricePerUnit}");
}
/// <summary>
/// Client Stream gRPC Method - UploadProduct
/// </summary>
var list = new List<ProductUploadRequest>()
        {   new ProductUploadRequest(){ Name = "Product A", Description = "A", PricePerUnit = 100 },
            new ProductUploadRequest(){ Name = "Product B", Description = "B", PricePerUnit = 50 },
            new ProductUploadRequest(){ Name = "Product C", Description = "C", PricePerUnit = 10 }
        };
var uploadCall = productclient.UploadProduct();
foreach (var item in list)
{
    await uploadCall.RequestStream.WriteAsync(new ProductUploadRequest() { Name = item.Name, Description = item.Description,  PricePerUnit = item.PricePerUnit });
}
await uploadCall.RequestStream.CompleteAsync();
var responseUploadCall = await uploadCall;
Console.WriteLine($"Total Product Uploaded: " +
    $"\n\tCount: {responseUploadCall.Count}," +
    $"\n\tTotal Amount: {responseUploadCall.TotalAmount}");
/// <summary>
/// Bi-Directional Stream gRPC Method - SyncProduct
/// </summary>
var syncCall = productclient.SyncProduct();
foreach(var item in list)
{
    await syncCall.RequestStream.WriteAsync(item);
}
await syncCall.RequestStream.CompleteAsync();
await Task.Run(async () =>
{
    await foreach(var responseSyncCall in syncCall.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"Total Product Sync: " +
            $"\n\tCount: {responseSyncCall.Count}," +
            $"\n\tTotal Amount: {responseSyncCall.TotalAmount}");
    }    
});

Console.WriteLine("Press any key to exit...");
Console.ReadKey();