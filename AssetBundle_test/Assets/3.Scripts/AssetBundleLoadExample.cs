using System.Collections;
using System.IO; // File을 읽어오기를 위해서 선언
using UnityEngine;

// 메모리(바이트 배열)에서 에셋번들을 비동기로 읽어 프리팹을 생성하는 예제
public class AssetBundleLoadExample : MonoBehaviour
{
    [Tooltip("번들 파일 이름 (StreamingAssets 아래)")]
    public string bundleName = "red";

    [Tooltip("번들 안의 프리팹 이름")]
    public string assetName = "RED_Cube";

    AssetBundle bundle;

    private void Start()
    {
        StartCoroutine(LoadFromMemoryAsync()); // 코루틴으로 실행
    }

    // StreamingAssets를 먼저 찾고, 에디터에서는 예전 빌드 위치(Assets/AssetBundles)도 확인
    public static string FindBundlePath(string bundleName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, bundleName);
        if (File.Exists(path)) return path;
        string legacy = Path.Combine("Assets/AssetBundles", bundleName);
        if (Application.isEditor && File.Exists(legacy)) return legacy;
        return null;
    }

    IEnumerator LoadFromMemoryAsync()
    {
        string path = FindBundlePath(bundleName);
        if (path == null)
        {
            Debug.LogError($"에셋번들 '{bundleName}'을(를) 찾지 못했습니다. 메뉴 Assets > Build AssetBundles를 먼저 실행하세요.");
            yield break;
        }

        // 에셋 번들 데이터가 들어있는 바이트 배열로 읽어 메모리에서 로드
        AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
        yield return createRequest;

        bundle = createRequest.assetBundle;
        if (bundle == null)
        {
            Debug.LogError($"에셋번들 로드 실패: {path} (다른 플랫폼용으로 빌드되었거나 이미 로드된 번들일 수 있음)");
            yield break;
        }

        // 매니페스트 파일에 있는 이름으로 프리팹 찾기
        var prefab = bundle.LoadAsset<GameObject>(assetName);
        if (prefab == null)
        {
            Debug.LogError($"번들 '{bundleName}' 안에 '{assetName}' 프리팹이 없습니다.");
            yield break;
        }
        Instantiate(prefab);
    }

    // 번들을 해제하지 않으면 메모리가 남고, 다시 로드할 때 "already loaded" 오류가 난다.
    private void OnDestroy()
    {
        if (bundle != null) bundle.Unload(false);
    }
}
