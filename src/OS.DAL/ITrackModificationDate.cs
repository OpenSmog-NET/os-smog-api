using System;

namespace OS.DAL
{
    public interface ITrackModificationDate
    {
        DateTime ModifiedAt { get; set; }
    }
}