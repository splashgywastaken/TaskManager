namespace TaskManager.Service.Entities.Tag;

using TaskManager.Entities;

public interface ITagService
{
    IQueryable<Tag> GetAllTags();
    Tag GetById(int tagId);
}