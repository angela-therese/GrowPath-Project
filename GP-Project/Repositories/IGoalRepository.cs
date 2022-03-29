﻿using GrowPath.Models;

namespace GrowPath.Repositories
{
    public interface IGoalRepository
    {
        Goal GetById(int id);
         void Add(Goal goal);
    }
}