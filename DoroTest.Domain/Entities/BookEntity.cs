using DoroTest.Domain.Common;
using DoroTest.Domain.Enums;

namespace DoroTest.Domain.Entities;
public class BookEntity : AuditableEntity
{
    public string Title { get; set; }

    public GendersBookEnum Gender { get; set; }
}
