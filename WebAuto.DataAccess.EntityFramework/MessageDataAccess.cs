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
        //Послать новое сообщение
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
        //Сколько новых непрочитанных сообщений?
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
        //Список всех сообщений по Id
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
        //переделываем сообщение из бд в местный формат
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
        //Прочитать сообщения по id?
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
        //Присваиваем сообщения к юзеру через номер авто
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
        //вывод список отправленных сообщений
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
    }
}
