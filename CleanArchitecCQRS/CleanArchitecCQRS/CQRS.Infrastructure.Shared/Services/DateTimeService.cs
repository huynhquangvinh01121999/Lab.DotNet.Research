using EsuhaiHRM.Application.Interfaces;
using System;

namespace CQRS.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
