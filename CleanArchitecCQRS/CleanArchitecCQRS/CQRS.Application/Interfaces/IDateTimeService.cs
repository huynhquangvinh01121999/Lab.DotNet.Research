using System;
using System.Collections.Generic;
using System.Text;

namespace EsuhaiHRM.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
