using System.IO; // Directory 함수 사용하기 위해 선언
using UnityEditor;
using UnityEngine;

// 에디터 메뉴: Assets > Build AssetBundles
// 빌드한 번들은 StreamingAssets에 저장해 에디터와 실제 빌드(앱) 양쪽에서 같은 경로로 읽을 수 있게 한다.
public static class BuildAssetBuledss
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAssetBundles()
    {
        // 에셋번들 저장할 폴더 위치 (Application.streamingAssetsPath와 같은 위치)
        string assetBundlesDirectory = "Assets/StreamingAssets";
        if (!Directory.Exists(assetBundlesDirectory)) // 에셋번들 저장할 폴더가 없다면
        {
            Directory.CreateDirectory(assetBundlesDirectory); // 폴더 생성하기
        }

        // 현재 선택된 빌드 플랫폼용으로 생성 (Windows 고정 시 Android·WebGL에서 번들을 못 읽음)
        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
        BuildPipeline.BuildAssetBundles(assetBundlesDirectory, BuildAssetBundleOptions.None, target);
        AssetDatabase.Refresh();
        Debug.Log($"에셋번들 빌드 완료: {assetBundlesDirectory} ({target})");
    }
}
