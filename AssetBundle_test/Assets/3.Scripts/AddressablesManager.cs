using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

// Addressables 버튼 예제: 프리팹 생성 / 씬 로드 / 스프라이트 로드
// 해제 규칙: LoadAssetAsync → Addressables.Release(handle), InstantiateAsync → Addressables.ReleaseInstance(obj)
public class AddressablesManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private AssetReference assetReference;                    // 씬
    [SerializeField] private AssetReferenceGameObject assetReferenceGameObject; // 프리팹
    [SerializeField] private AssetReferenceSprite AssetReferenceSprite;         // 스프라이트

    [Tooltip("생성한 프리팹을 자동으로 없앨 시간(초). 0이면 유지")]
    [SerializeField] private float lifetime = 2f;

    AsyncOperationHandle<Sprite> spriteHandle;

    // 이전 버전은 Start에서 이 관리자 오브젝트 자신을 0.1초 뒤 ReleaseInstance 했다.
    // 생성된 프리팹에만 적용되도록 AddressablesAutoRelease 컴포넌트로 분리했다.
    public void AddressablesPrefab()
    {
        if (assetReferenceGameObject == null || !assetReferenceGameObject.RuntimeKeyIsValid())
        {
            Debug.LogWarning("Asset Reference Game Object가 지정되지 않았습니다.");
            return;
        }
        // 메모리 로드 구문 없이 바로 Instantiate
        assetReferenceGameObject.InstantiateAsync().Completed += handle =>
        {
            if (handle.Status != AsyncOperationStatus.Succeeded) { Debug.LogError("프리팹 생성 실패"); return; }
            if (lifetime > 0f) handle.Result.AddComponent<AddressablesAutoRelease>().lifetime = lifetime;
        };
    }

    public void AddressablesScene()
    {
        if (assetReference == null || !assetReference.RuntimeKeyIsValid())
        {
            Debug.LogWarning("Asset Reference(씬)가 지정되지 않았습니다.");
            return;
        }
        // LoadSceneMode.Additive로 바꾸면 기존 씬에 추가로 로드
        assetReference.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void AddressablesSprite()
    {
        if (AssetReferenceSprite == null || !AssetReferenceSprite.RuntimeKeyIsValid() || image == null)
        {
            Debug.LogWarning("Asset Reference Sprite 또는 Image가 지정되지 않았습니다.");
            return;
        }
        if (spriteHandle.IsValid()) { image.sprite = spriteHandle.Result; return; } // 이미 로드됨: 다시 로드하지 않음
        // 메모리에 로드 후 콜백 호출
        spriteHandle = AssetReferenceSprite.LoadAssetAsync();
        spriteHandle.Completed += OnSpriteLoaded;
    }

    // 콜백 함수
    private void OnSpriteLoaded(AsyncOperationHandle<Sprite> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded) image.sprite = handle.Result;
        else Debug.LogError("스프라이트 로드 실패");
    }

    private void OnDestroy()
    {
        if (spriteHandle.IsValid()) Addressables.Release(spriteHandle);
    }
}
