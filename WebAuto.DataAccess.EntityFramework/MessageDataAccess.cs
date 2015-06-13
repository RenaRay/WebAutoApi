using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace WebAuto.DataAccess.EntityFramework
{
    public class MessageDataAccess : IMessageDataAccess
    {
        public async Task CreateAsync(DataAccess.Message message)
        {
            var entity = new Message();
            UpdateEntityFromMessage(entity, message);
            using (var entities = new Entities())
            {
                entities.Message.Add(entity);
                await entities.SaveChangesAsync();
            }
        }

        private void UpdateEntityFromMessage(Message entity, DataAccess.Message message)
        {
            entity.CarRegNumber = message.ToPlate;
            entity.DateCreated = message.Sent;
            entity.MessageText = message.Text;
            entity.ReceiverID = message.ToUserId;
            entity.Score = message.IsLiked;
            entity.Viewed = message.IsRead;
            entity.UserID = message.FromUserId;
        }

        public Task<int> GetUnreadCount(int userId)
        {
            using (var entities = new Entities())
            {
                var count = entities.Message
                    .Where(m => m.ReceiverID == userId && m.Viewed == false)
                    .Count();
                return Task.FromResult(count);
            }
        }

        public async Task<List<DataAccess.Message>> GetInboxMessages(int userId)
        {
            using (var entities = new Entities())
            {
                var messages = await entities.Message
                    .Where(m => m.ReceiverID == userId)
                    .OrderByDescending(m => m.DateCreated)
                    .ToListAsync();

                var inboxMessages = messages
                    .Select(GetMessageFromEntity)
                    .ToList();

                return inboxMessages;
            }
        }

        private static DataAccess.Message GetMessageFromEntity(Message entity)
        {
            return
                new DataAccess.Message
                {
                    Id = entity.MessageID,
                    ToPlate = entity.CarRegNumber,
                    Sent = entity.DateCreated,
                    Text = entity.MessageText,
                    ToUserId = entity.ReceiverID,
                    IsRead = entity.Viewed ?? false,
                    IsLiked = entity.Score ?? false,
                    FromUserId = entity.UserID
                };
        }

        public async Task ReadInboxMessages(int userId)
        {
            using (var entities = new Entities())
            {
                var messages = await entities.Message
                    .Where(m => m.ReceiverID == userId && m.Viewed == false)
                    .ToListAsync();
                foreach (var message in messages)
                {
                    message.Viewed = true;
                }

                await entities.SaveChangesAsync();
            }
        }

        public async Task AddMessagesToUser(int userId)
        {
            using (var entities = new Entities())
            {
                var plates = await entities.Car
                    .Where(c => c.CarOwnerId == userId)
                    .Select(c => c.RegNumber)
                    .ToListAsync();

                var messages = await entities.Message
                    .Where(m => m.ReceiverID == null && plates.Contains(m.CarRegNumber))
                    .ToListAsync();

                foreach (var message in messages)
                {
                    message.ReceiverID = userId;
                }

                await entities.SaveChangesAsync();
            }
        }

        public async Task<List<DataAccess.Message>> GetSentMessages(int userId)
        {
            using (var entities = new Entities())
            {
                var messages = await entities.Message
                    .Where(m => m.UserID == userId)
                    .OrderByDescending(m => m.DateCreated)
                    .ToListAsync();

                var sentMessages = messages
                    .Select(GetMessageFromEntity)
                    .ToList();

                return sentMessages;
            }
        }


        public async Task<DataAccess.Message> FindById(int messageId)
        {
            using (var entities = new Entities())
            {
                var message = await GetMessageFromDatabase(entities, messageId);
                if (message == null)
                {
                    return null;
                }
                return GetMessageFromEntity(message);
            }
        }

        private static Task<Message> GetMessageFromDatabase(Entities entities, int messageId)
        {
            return entities.Message
                .FirstOrDefaultAsync(m => m.MessageID == messageId);
        }

        public async Task UpdateAsync(DataAccess.Message message)
        {
            using (var entities = new Entities())
            {
                var messageFromDatabase = await GetMessageFromDatabase(entities, message.Id);
                if (messageFromDatabase == null)
                {
                    return;
                }
                UpdateEntityFromMessage(messageFromDatabase, message);
                await entities.SaveChangesAsync();
            }
        }
    }
}
