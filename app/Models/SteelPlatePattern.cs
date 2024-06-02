using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models;
public class SteelPlatePattern
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid(); // 自動生成Guid
    public required string PatternId { get; set; }
    public required DateTime Date { get; set; } // DateTime更適合表示日期
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public required string ImagePath { get; set; }
}
