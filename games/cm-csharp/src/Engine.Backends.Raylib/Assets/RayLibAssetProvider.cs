using Engine.Assets;
using Engine.Graphics;

namespace Engine.Backends.Raylib.Assets;

public sealed class RayLibAssetProvider : IAssetProvider
{
    private readonly IDictionary<Guid, object> _assets = new Dictionary<Guid, object>();

    public TAsset Load<TAsset>(string path) where TAsset : Asset
    {
        switch (typeof(TAsset))
        {
            case var type when type == typeof(Sprite):
                var texture = Raylib_cs.Raylib.LoadTexture(path);
                var sprite =  new Sprite(texture.width, texture.height) as TAsset;

                _assets.Add(sprite.Id, texture);

                return sprite;
        }

        throw new NotImplementedException();
    }

    internal TAsset Fetch<TAsset>(Guid id)
    {
        return (TAsset)_assets[id];
    }
}
