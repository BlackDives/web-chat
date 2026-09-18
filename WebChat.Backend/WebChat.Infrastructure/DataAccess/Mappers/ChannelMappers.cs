using WebChat.Shared.Models.Channels;

namespace WebChat.Infrastructure.DataAccess.Mappers;

internal static class ChannelMappers
{
    public static Channel ToModel(this Entities.ChannelEntity channelEntity)
    {
        return new Channel
        {
            Id = channelEntity.Id,
        };
    }

    public static Entities.ChannelEntity ToEntity(this Channel model)
    {
        return new Entities.ChannelEntity
        {
            Id = model.Id,
        };
    }
}