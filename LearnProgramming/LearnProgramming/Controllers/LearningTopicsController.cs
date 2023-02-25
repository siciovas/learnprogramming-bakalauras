﻿using AutoMapper;
using LearnProgramming.Core.Dto;
using LearnProgramming.Core.Dto.DtoPost;
using LearnProgramming.Core.Interfaces;
using LearnProgramming.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearnProgramming.API.Controllers
{
    [ApiController]
    [Route("api/learningtopic")]
    public class LearningTopicsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILearningTopicsRep _learningTopicsRep;
        
        public LearningTopicsController(IMapper mapper, ILearningTopicsRep learningTopicsRep)
        {
            _mapper = mapper;
            _learningTopicsRep = learningTopicsRep;
        }

        [HttpGet]
        public async Task<ActionResult<List<LearningTopicsDto>>> GetAll()
        {
            var topics = await _learningTopicsRep.GetAll();

            var topicsDto = topics.Select(x => _mapper.Map<LearningTopicsDto>(x)).ToList();

            return Ok(topicsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LearningTopicsDto>> Get(int id)
        {
            var topics = await _learningTopicsRep.Get(id);
            if (topics == null) return NotFound();

            var topicsDto = _mapper.Map<LearningTopicsDto>(topics);

            return Ok(topicsDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var topics = await _learningTopicsRep.Get(id);
            if (topics == null) return NotFound();

            await _learningTopicsRep.Delete(topics);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<LearningTopicsDto>> Update(LearningTopicsDto learningTopics, int id) 
        {
            var topics = await _learningTopicsRep.Get(id);
            if(topics == null) return NotFound();

            topics.Photo = learningTopics.Photo;
            topics.Title = learningTopics.Title;
            topics.NumberOfSubTopics = learningTopics.NumberOfSubTopics;
            topics.NumberOfAllTasks = learningTopics.NumberOfAllTasks;
            topics.DifficultyInText = learningTopics.DifficultyInText;
            topics.DifficultyInStars = learningTopics.DifficultyInStars;

            await _learningTopicsRep.Update(topics);

            return Ok(topics);
        }

        [HttpPost]
        public async Task<ActionResult<LearningTopicsPostDto>> Post(LearningTopicsPostDto learningTopics)
        {
            var newTopic = new LearningTopic
            {
                Photo = learningTopics.Photo,
                Title = learningTopics.Title,
                NumberOfSubTopics = learningTopics.NumberOfSubTopics,
                NumberOfAllTasks = learningTopics.NumberOfAllTasks,
                DifficultyInStars = learningTopics.DifficultyInStars,
                DifficultyInText = learningTopics.DifficultyInText
            };

            await _learningTopicsRep.Create(newTopic);

            return Created($"api/learningtopic{newTopic.Id}", _mapper.Map<LearningTopicsPostDto>(newTopic));
        }
    }
}
