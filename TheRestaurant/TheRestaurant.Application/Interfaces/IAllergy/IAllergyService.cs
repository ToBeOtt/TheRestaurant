﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRestaurant.Contracts.Requests.Allergy;
using TheRestaurant.Contracts.Requests.MenuItem;
using TheRestaurant.Domain.Entities.Menu;

namespace TheRestaurant.Application.Interfaces.IAllergy
{
    public interface IAllergyService
    {
        Task<Allergy> CreateAllergyAsync(AllergyRequest request);
        Task<List<Allergy>> GetAllAllergies();
        Task DeleteAllergyAsync(int id);
        Task<Allergy> UpdateAllergyAsync(int id, AllergyRequest request);
    }
}
