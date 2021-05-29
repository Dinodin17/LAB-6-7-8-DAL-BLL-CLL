﻿using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;
namespace BLL.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetCustomers(int page);
    }
}
