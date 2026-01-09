using System;
using System.ComponentModel.DataAnnotations;

namespace P1WebAppRazor.Models;

public class Blog
{

    [Key]
    public required Guid BlogId { get; set; } = Guid.NewGuid();  // auto genarate a unique blogId
    public required string Title { get; set; }
    public required string FeaturedImageUrl { get ;set;}
    public required string ShortDesc { get; set; }
    public required string BlogContent { get; set; }
    public required string Author { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // auto generate time at which blog is submitted 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;  // auto genarate time at the time of updation 


}
