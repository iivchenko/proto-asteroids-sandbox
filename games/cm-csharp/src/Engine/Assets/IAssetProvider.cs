namespace Engine.Assets;

public interface IAssetProvider
{
    TContent Load<TContent>(string path) where TContent : Asset;
}
