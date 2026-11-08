using System.IO; // File을 읽어오기를 위해서 선언
using UnityEngine;

// 파일에서 바로 에셋번들을 읽는 예제 (메모리 복사가 없어 LoadFromMemory보다 가볍다)
public class LoadFromFileExample : MonoBehaviour
{
    public string bundleName = "blue";
    public string assetName = "Blue";

    AssetBundle loaded;

    void Start()
    {
        string path = AssetBundleLoadExample.FindBundlePath(bundleName);
        loaded = path == null ? null : AssetBundle.LoadFromFile(path);
        if (loaded == null)
        {
            Debug.Log("Failed to load AssetBundle! 메뉴 Assets > Build AssetBundles로 번들을 먼저 만드세요.");
            return;
        }
        var prefab = loaded.LoadAsset<GameObject>(assetName);
        if (prefab == null)
        {
            Debug.LogError($"번들 '{bundleName}' 안에 '{assetName}' 프리팹이 없습니다.");
            return;
        }
        Instantiate(prefab);
    }

    void OnDestroy()
    {
        if (loaded != null) loaded.Unload(false);
    }
}
