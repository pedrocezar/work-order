namespace WorkOrder.Domain.Models;

public class BaseModel
{
    public BaseModel()
    {
        Active = true;
    }

    public int Id { get; set; }

    public bool Active { get; set; }

    public int? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }
    public DateTime? DataAlteracao { get; set; }
}
