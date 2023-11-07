namespace Engine.Assets;

public interface IAssetLoader<out TAsset>
    where TAsset : Asset
{
    TAsset Load(string path);
}
