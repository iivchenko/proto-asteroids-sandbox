using Engine.Core;

namespace Engine.Services.Content;

public interface IContentService
{
    public TResource Load<TResource>(string path)
        where TResource : Resource;
}
