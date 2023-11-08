﻿using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IFieldLogic
{
    public Task<IEnumerable<FieldLookupDto>> GetAsync(int OwnerId);
}