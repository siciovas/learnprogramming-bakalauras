﻿using AutoMapper;
using LearnProgramming.Core.Dto;
using LearnProgramming.Core.Interfaces;
using LearnProgramming.Domain.Entities;
using LearnProgramming.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnProgramming.Infrastructure.Repositories
{
    public class LearningTopicsRepository : ILearningTopicsRep
    {
        private readonly DatabaseContext _db;

        public LearningTopicsRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<LearningTopicsDto>> GetAll()
        {
            var learningTopics = await _db.LearningTopics
                .Select(x => new LearningTopicsDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    DifficultyInText = x.DifficultyInText,
                    NumberOfSubTopics = _db.SubTopics.Count(y => y.LearningTopicId == x.Id),
                    NumberOfAllTasks = _db.ProgrammingTask.Count(y => y.LearningTopicId == x.Id)
                })
                .ToListAsync();

            return learningTopics;
        }
        public async Task<LearningTopic> Create(LearningTopic learningTopics)
        {
            _db.Add(learningTopics);
            await _db.SaveChangesAsync();

            return learningTopics;
        }

        public async Task Delete(LearningTopic learningTopics)
        {
            _db.LearningTopics.Remove(learningTopics);
            await _db.SaveChangesAsync();
        }

        public async Task<LearningTopic> Get(int id)
        {
            return await _db.LearningTopics.FirstOrDefaultAsync(topics => topics.Id == id);
        }


        public async Task<LearningTopic> Update(LearningTopic learningTopics)
        {
           _db.LearningTopics.Update(learningTopics);
           await _db.SaveChangesAsync();

            return learningTopics;
        }
    }
}
