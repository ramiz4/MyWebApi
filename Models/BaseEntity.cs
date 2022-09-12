using System.ComponentModel.DataAnnotations;

/// <summary>
/// BaseEntity
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Primary key
    /// </summary>
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}