using Microsoft.AspNetCore.Components;

namespace Templates.Models
{
    public record TemplateRef<TValue>
    {
        public RenderFragment<TValue>? Fragment { get; set; }
    }

    public record TemplateRef : TemplateRef<object> { }
}
