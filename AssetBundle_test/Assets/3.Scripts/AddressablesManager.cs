using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressablesManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private AssetReference assetReference;
    [SerializeField] private AssetReferenceGameObject assetReferenceGameObject;
    [SerializeField] private AssetReferenceSprite AssetReferenceSprite;
    //해제 
    //Addressables.LoadAssetAsync() -> Addressables.Release();
    //Addressables.InstantiateAsync() -> Addressables.ReleaseInstance();
    //public void OnClickButton()
    //{
    //    Addressables.InstantiateAsync(assetReference).Completed += handle => {
    //        var instance = handle.Result;
    //        StartCoroutine(DestroyInstance(instance));
    //    };
    //}

    //public IEnumerator DestroyInstance(GameObject instance)
    //{
    //    yield return new WaitForSeconds(2f);

    //    Destroy(instance);
    //    Addressables.Release(instance);
    //}
    [SerializeField] private float lifetime = 0.1f;

    private void Start()
    {
        // 지연 후 함수 호출
        Invoke("Release", lifetime);
    }

    void Release()
    {
        // Instantiate <-> ReleaseInstance    
        Addressables.ReleaseInstance(gameObject);
    }
    public void AddressablesPrefab()
    {
        // 메모리 로드 구문 없이 바로 Instantiate
        Addressables.InstantiateAsync(assetReferenceGameObject);
        //Addressables.InstantiateAsync("Assets/Prefabs/Cube.prefab");
    
    }
    public void AddressablesScene()
    {
        // 메모리 로드 구문 없이 바로 씬 로드
        //UnityEngine.SceneManagement.LoadSceneMode.Additive -> 기존씬에서 추가적으로 실행
        assetReference.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single);
        //Addressables.LoadAssetAsync<GameObject>("Assets/2.Prefabs/RED_Cube.prefab").Completed += handle =>
        //{
        //    Instantiate(handle.Result);
        //};
    }

    public void AddressablesSprite()
    {
        // 메모리에 로드 후 콜백 호출
        AssetReferenceSprite.LoadAssetAsync().Completed += OnSpriteLoaded;
    }

    // 콜백 함수
    private void OnSpriteLoaded(AsyncOperationHandle<Sprite> handle)
    {
        image.sprite = handle.Result;
    }
}
