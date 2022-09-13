using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Core.Entities;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}