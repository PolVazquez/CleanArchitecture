﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL_ApplicationLayer
{
    public interface IExternalServiceAdapter<T>
    {
        Task<IEnumerable<T>> GetDataAsync();
    }
}
