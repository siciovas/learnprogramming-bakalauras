﻿using AutoMapper;
using LearnProgramming.Core.Dto;
using LearnProgramming.Core.Interfaces;
using LearnProgramming.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearnProgramming.API.Controllers
{
    [ApiController]
    [Route("api/learningtopic/{learningtopicId}/subtopic")]
    public class SubTopicsController : Controller
    {
        private readonly ISubTopicsRep _subTopicsRep;
        private readonly ILearningTopicsRep _learningTopicsRep;
        private readonly IMapper _mapper;

        public SubTopicsController(ISubTopicsRep subTopicsRep, ILearningTopicsRep learningTopicsRep, IMapper mapper)
        {
            _subTopicsRep = subTopicsRep;
            _learningTopicsRep = learningTopicsRep;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubTopicDto>>> GetAll(int learningtopicId)
        {
            var subTopics = await _subTopicsRep.GetAll(learningtopicId);

            return Ok(subTopics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubTopicDto>> Get(int id, int learningtopicId)
        {
            var topic = await _learningTopicsRep.Get(learningtopicId);
            if (topic == null) return NotFound();

            var subTopic = await _subTopicsRep.Get(id);
            if (subTopic == null) return NotFound();

            return new SubTopicDto
            {
                SubTopicId = subTopic.Id,
                SubTopicName = subTopic.SubTopicName,

            };

        }

        [HttpPost]
        public async Task<ActionResult<SubTopicPostDto>> Post(int learningtopicId, SubTopicPostDto subTopicPost)
        {
            var topic = await _learningTopicsRep.Get(learningtopicId);
            if (topic == null) return NotFound();

            var subTopic = new SubTopic
            {
                LearningTopicId = learningtopicId,
                SubTopicName = subTopicPost.SubTopicName,
            };

            await _subTopicsRep.Create(subTopic);

            return Created($"api/learningtopic/{topic.Id}/subtopic/{subTopic.Id}", _mapper.Map<SubTopicPostDto>(subTopic));

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<SubTopicUpdateDto>> Update(int learningtopicId, int id, SubTopicUpdateDto subTopicDto)
        {
            var topic = await _learningTopicsRep.Get(learningtopicId);
            if (topic == null) return NotFound();

            var subTopic = await _subTopicsRep.Get(id);
            if (subTopic == null) return NotFound();

            subTopic.SubTopicName = subTopicDto.SubTopicName;


            await _subTopicsRep.Update(subTopic);

            return Ok(subTopic);

        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int learningtopicId, int id)
        {
            var topic = await _learningTopicsRep.Get(learningtopicId);
            if (topic == null) return NotFound();

            var subTopic = await _subTopicsRep.Get(id);
            if (subTopic == null) return NotFound();

            await _subTopicsRep.Delete(subTopic);

            return NoContent();
        }

    }
}
