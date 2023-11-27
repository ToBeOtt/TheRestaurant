﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant.Contracts.Requests.Item
{
    public record EditItemRequest(
        string Name,
        double Price,
        string Description,
        byte[] MenuPhoto,
        bool IsFoodItem,
        bool IsDeleted
        );

}