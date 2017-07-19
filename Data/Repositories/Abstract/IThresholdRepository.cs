using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Domain;

namespace Data.Repositories.Abstract
{
    public interface IThresholdRepository
    {
        IQueryable<Threshold> Thresholds { get; }
        void SaveThreshold(Threshold threshold);
        
    }
}
