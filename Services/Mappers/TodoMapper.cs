using Persistence.Models;
using Services.DTO;

namespace Services.Mappers;

public class TodoMapper
{
    public static TodoDTO ToDto(TodoItem item)
    {
        return new TodoDTO
        {
            Id = item.Id,
            Title = item.Title,
            Date = item.Date,
            Completed = item.Completed
        };
    }
}