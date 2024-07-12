using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MyBlogger.Data;
using MyBlogger.Models;

namespace MyBlogger.Components.Pages;

public partial class Home
{
    [Inject]
    private ApplicationDbContext Context { get; set; } = null!;
    public IEnumerable<Blog> Blogs { get; set; } = [];
    [SupplyParameterFromForm]
    public Blog NewPost { get; set; } = new() { UserId = string.Empty };
    [SupplyParameterFromForm]
    public IFormFile? UploadedImage { get; set; } = null;

    protected async override Task OnInitializedAsync()
    {
        Blogs = await Context.Blog.ToListAsync();
    }

    public async Task CreateNewPost()
    {
        // Handle saving the image file (if any)
        var now = DateTime.Now;
        if (UploadedImage is not null)
        {
            string imageFileName = $"img_{now:yyyyMMddHHmm}.{Path.GetExtension(UploadedImage.FileName)}";
            var filePath = $"~/wwwroot/img/{imageFileName}";
            using (FileStream stream = new(filePath, FileMode.Create))
            {
                await UploadedImage.CopyToAsync(stream);
            }
            NewPost.ImageFileName = imageFileName;
        }

        NewPost.BlogId = Guid.NewGuid();
        NewPost.PostedAt = now;

        Context.Add(NewPost);
        await Context.SaveChangesAsync();
    }
}
