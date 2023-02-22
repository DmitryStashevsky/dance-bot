using System;
using DanceBotShared.Db.Messages.Models;
using DanceBotShared.Db.Messages.Results;
using MongoDB.Bson;

namespace DanceBotDb.Common
{
	public interface IDbContext
	{
        Task<IList<T>> GetAll<T>() where T : Document;
        Task<T> GetById<T>(string id) where T : Document;
        Task<T> Add<T>(T entity) where T : Document;
    }
}

