namespace Engine.Services;

public interface IAssetService<out TAsset>
    where TAsset : Asset
{
    TAsset Load(string path);
}
