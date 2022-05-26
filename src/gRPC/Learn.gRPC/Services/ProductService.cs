using Grpc.Core;
using Learn.gRPC;
using Learn.gRPC.Model;
using Tynamix.ObjectFiller;

namespace Learn.gRPC.Services
{
    public class ProductService : ProductInfo.ProductInfoBase
    {
        private readonly ILogger<ProductService> _logger;
        private readonly Filler<Products> products;
        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
            products = new Filler<Products>();
            products.Setup()
                    .OnProperty(x => x.Id).Use(Enumerable.Range(1, 100))
                    .OnProperty(x => x.Name).Use(new MnemonicString(1))
                    .OnProperty(x => x.Description).Use(new MnemonicString(10, 5, 10))
                    .OnProperty(x => x.PricePerUnit).Use(new Tynamix.ObjectFiller.DoubleRange(10, 100));
        }

        public override Task<ProductResponse> GetProduct(ProductRequest request, ServerCallContext context)
        {
            return Task.FromResult(products.Create(100).Where(x => x.Id == request.Id).Select(x => new ProductResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                PricePerUnit = (double)x.PricePerUnit
            }).First());
        }
        public override async Task GetProductList(ProductListRequest request, IServerStreamWriter<ProductResponse> responseStream, ServerCallContext context)
        {
            var list = products.Create(request.Count).Select(x => new ProductResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                PricePerUnit = (double)x.PricePerUnit
            }).ToList();
            foreach (var item in list)
            {
                await responseStream.WriteAsync(item);
            }
        }

        public override async Task<ProductUploadResponse> UploadProduct(IAsyncStreamReader<ProductUploadRequest> requestStream, ServerCallContext context)
        {
            var list = new List<ProductUploadRequest>();
            while (await requestStream.MoveNext())
            {
                ProductUploadRequest message = requestStream.Current;
                list.Add(message);
            }
            return await Task.FromResult(new ProductUploadResponse() { Count = list.Count, TotalAmount = list.Sum(x => x.PricePerUnit) });
        }

        public override async Task SyncProduct(IAsyncStreamReader<ProductUploadRequest> requestStream, IServerStreamWriter<ProductUploadResponse> responseStream, ServerCallContext context)
        {
            var list = new List<ProductUploadRequest>();
            await foreach(var item in requestStream.ReadAllAsync())
            {
                list.Add(item);
                await responseStream.WriteAsync(new ProductUploadResponse { Count = list.Count, TotalAmount = list.Sum(x => x.PricePerUnit) });
            }
        }
    }
}
