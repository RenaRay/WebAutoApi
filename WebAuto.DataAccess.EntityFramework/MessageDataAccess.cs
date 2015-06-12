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
            var newMessage =
                new Message
                {
                    CarRegNumber = message.ToPlate,
                    DateCreated = message.Sent,
                    MessageText = message.Text,
                    ReceiverID = message.ToUserId,
                    Score = false,
                    Viewed = false,
                    UserID = message.FromUserId
                };
            using (var entities = new Entities())
            {
                entities.Message.Add(newMessage);
                await entities.SaveChangesAsync();
            }
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
                    .Select(m => new DataAccess.Message
                    {
                        Id = m.MessageID,
                        ToPlate = m.CarRegNumber,
                        Sent = m.DateCreated,
                        Text = m.MessageText,
                        ToUserId = m.ReceiverID,
                        IsRead = m.Viewed ?? false,
                        IsLiked = m.Score ?? false,
                        FromUserId = m.UserID
                    })
                    .ToList();

                return inboxMessages;
            }
        }

        public async Task ReadInboxMessages(int userId)
        {
            using (var entities = new Entities())
            {
                var messages = await entities.Message
                    .Where(m => m.ReceiverID == userId && m.Viewed == false)
                    .ToListAsync();
                foreach(var message in messages)
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

                foreach(var message in messages)
                {
                    message.ReceiverID = userId;
                }

                await entities.SaveChangesAsync();
            }
        }
    }
}
