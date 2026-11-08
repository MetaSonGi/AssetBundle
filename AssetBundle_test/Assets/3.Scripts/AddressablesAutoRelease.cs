using UnityEngine;
using UnityEngine.AddressableAssets;

// InstantiateAsync로 만든 오브젝트를 lifetime초 뒤 ReleaseInstance로 정리 (Destroy만 하면 참조 카운트가 남음)
public class AddressablesAutoRelease : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Invoke(nameof(Release), lifetime);
    }

    void Release()
    {
        if (!Addressables.ReleaseInstance(gameObject))
            Destroy(gameObject); // Addressables로 만든 것이 아니면 일반 삭제
    }
}
