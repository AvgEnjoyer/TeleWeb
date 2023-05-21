﻿using TeleWeb.Domain.Models;

namespace TeleWeb.Data.Repositories.Interfaces
{
    public interface IChannelRepository: IRepositoryBase<Channel> 
    {
        public IQueryable<Post> FindPostsByChannelId(int channelId);
    }
}
