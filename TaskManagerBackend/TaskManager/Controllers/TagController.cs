using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Models.Tag;
using TaskManager.Service.Entities.Tag;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("tags")]
    [Produces("application/json")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetAll()
        {
            List<Tag> tags;
            try
            {
                tags = (List<Tag>)await _tagService.GetAllTags();
            }
            catch (ObjectNotFoundException exception)
            {
                return NotFound(exception.Message);
            }

            var mappedTags = tags.Select(x => 
                _mapper.Map<TagDataModel>(x)
                ).ToList();

            return Ok(mappedTags);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetById(int id)
        {
            Tag tag;
            try
            {
                tag = await _tagService.GetById(id);
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Data);
            }

            var mappedTag = _mapper.Map<TagDataModel>(tag);

            return Ok(mappedTag);
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> AddTag([FromBody] TagDataModel tagDataModel)
        {
            var mappedTag = _mapper.Map<Tag>(tagDataModel);

            var resultTag = await _tagService.PostNew(mappedTag);
            var mappedResultTag = _mapper.Map<TagDataModel>(resultTag);

            return CreatedAtAction(
                nameof(GetById),
                new {id = mappedResultTag.TagId},
                mappedResultTag
                );
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> PutTag(int id, [FromBody] TagDataModel tagDataModel)
        {
            if (id != tagDataModel.TagId)
            {
                return BadRequest();
            }

            var mappedTag = _mapper.Map<Tag>(tagDataModel);
            StatusCodeResult status;
            try
            {
                status = await _tagService.UpdateTag(id, mappedTag);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                return NotFound(exception.Data);
            }

            if (status.StatusCode == StatusCodes.Status204NoContent)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var result = await _tagService.DeleteTag(id);

            if (result.StatusCode == StatusCodes.Status404NotFound)
            {
                NotFound();
            }

            return NoContent();
        }
    }
}
