using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //File을 읽어오기를 위해서 선언

public class AssetBundleLoadExample : MonoBehaviour
{
    private void Start()
    {
        //코루틴으로 실행
        StartCoroutine(LoadFromMemoryAsync());
    }
    IEnumerator LoadFromMemoryAsync()
    {
        //에셋 번들 데이터가 들어있는 바이트 형식의 배열가져옴
        AssetBundleCreateRequest createRequest = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes("Assets/AssetBundles/red"));

        yield return createRequest;

        AssetBundle bundle = createRequest.assetBundle;
        //LoadAsset -> 저장한 파일 불러오기 프리팹의 이름 RED_Cube
        //매니페스트 파일에 있는 RED_Cube이름을 찾음
        var prefab = bundle.LoadAsset<GameObject>("RED_Cube");
        Instantiate(prefab);
    }
}
