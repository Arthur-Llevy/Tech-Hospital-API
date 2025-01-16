using Api.Utils.Enums;

namespace Api.Domain.ModelsViews;

public class DoctorsModelView
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string User { get; set; } = default!;
    public string Specialty { get; set; } = default!;
}