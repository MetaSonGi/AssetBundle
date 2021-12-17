using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO; //Directory 함수 사용하기 위해 선언

public class BuildAssetBuledss : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        //에셋번들 저장할 폴더 위치 선언
        string assetBundlesDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundlesDirectory)) //에셋번들 저장할 폴더가 없다면 
        {
            //폴더 생성하기
            Directory.CreateDirectory(assetBundlesDirectory);
        }
        //assetBundlesDirectory  -> 에셋번들 생성 결로
        //BuildAssetBundleOptions.None -> 에셋번들 빌드 옵션
        //BuildTarget.StandaloneWindows -> 빌드 대상 플랫폼
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

    }
}
