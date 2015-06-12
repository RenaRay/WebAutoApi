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
    }
}
