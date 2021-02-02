﻿using Ardalis.Result;
using EmploymentApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmploymentApp.Core.Interfaces
{
    public interface IStatusService
    {
        Result<IEnumerable<Status>> GetAll();
    }
}
