using DataAccess.Contracts;

namespace MAAM.Models
{
    public record DisplayBlockDefinition : IModel
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public required string Name { get; set; }

        public required DisplayBlockDefinitionType Type { get; set; }

        public Dictionary<string, string> TemplateData { get; set; } = new Dictionary<string, string>();

    }

    public enum DisplayBlockDefinitionType
    {
        None = 0,
    }
}
