using WebChat.Shared.Models.Messages;

namespace WebChat.Infrastructure.DataAccess.Mappers;

internal static class MessageMappers
{
    public static Message MapToModel(this Entities.Message message)
    {
        return new Message
        {
            Id = message.Id,
        };
    }

    public static Entities.Message MapToEntity(this Message model)
    {
        return new Entities.Message
        {
            Id = model.Id,
        };
    }
}