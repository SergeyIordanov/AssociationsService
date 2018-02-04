﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AssociationsService.Entities.Association;

namespace AssociationsService.Services.Interfaces
{
    public interface IAssociationService
    {
        Task<IEnumerable<Association>> GetAssociationsAsync(IEnumerable<string> words, int limitForEach);

        Task<Association> GetAssociationAsync(string word, int limit);
    }
}