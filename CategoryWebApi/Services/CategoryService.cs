namespace CategoryWebApi.Services;

using CategoryWebApi.Data;
using CategoryWebApi.Services;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

public class CategoryService(DataContext dataContext) : CategoryHandler.CategoryHandlerBase
{
    private readonly DataContext _context = dataContext;


    public override async Task<GetCategoriesReply> GetCategories(GetCategoriesRequest request, ServerCallContext context)
    {
        var categories = await _context.Categories.ToListAsync();
        var reply = new GetCategoriesReply();
        foreach (var category in categories)
        {
            reply.Categories.Add(new CategoryModel
            {
                Id = category.Id,
                Description = category.Description
            });
        }
        return reply;
    }

    public override async Task<GetCategoryReply> GetCategoryById(GetCategoryByIdRequest request, ServerCallContext context)
    {
        var category = await _context.Categories.FindAsync(request.Id);
        if (category == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Category not found"));
        }
        return new GetCategoryReply
        {
            Category = new CategoryModel
            {
                Id = category.Id,
                Description = category.Description
            }
        };
    }
}
