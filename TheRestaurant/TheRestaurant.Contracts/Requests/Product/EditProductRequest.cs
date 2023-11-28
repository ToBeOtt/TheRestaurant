﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant.Contracts.Requests.Product
{
    public record EditProductRequest(
        string Name,
        double Price,
        string Description,
        byte[] MenuPhoto,
        bool IsFoodItem,
        bool IsDeleted
        );

}