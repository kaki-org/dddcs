using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class UserDataModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; }
    
    [Required]
    [MinLength(3)]
    public string Name { get; set; }
    public string MailAddress { get; set; }
}