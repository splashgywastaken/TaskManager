using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Service.Entities.Tag;

using TaskManager.Entities;

public interface ITagService
{
    Task<IEnumerable<Tag>> GetAllTags();
    Task<Tag> GetById(int tagId);
    Task<Tag> PostNew(Tag tag);
    Task<StatusCodeResult> UpdateTag(int id, Tag tag);
    Task<StatusCodeResult> DeleteTag(int id);
}