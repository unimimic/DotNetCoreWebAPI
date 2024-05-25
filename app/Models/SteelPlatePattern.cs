using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models;
public class SteelPlatePattern
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public required string PatternId { get; set; }
    public required string Date { get; set; }
    public required string CreateDate { get; set; }
    public required string Description { get; set; }
    public required string Category  { get; set; }
    public required string ImagePath { get; set; }
}
