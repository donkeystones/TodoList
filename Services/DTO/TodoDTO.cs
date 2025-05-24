namespace Services.DTO;

public class TodoDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateOnly Date { get; set; }
    public bool Completed { get; set; }
}