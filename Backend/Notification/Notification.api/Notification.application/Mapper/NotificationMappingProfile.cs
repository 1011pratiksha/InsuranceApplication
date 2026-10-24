using AutoMapper;
using Notification.application.DTO;

using NotificationEntity = Notification.domain.Entities.Notification;

namespace Notification.application.Mapper
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<CreateNotificationDTO, NotificationEntity>();
            CreateMap<NotificationEntity, NotificationDTO>();
        }
    }
}
