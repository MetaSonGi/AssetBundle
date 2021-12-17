using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //File을 읽어오기를 위해서 선언

public class LoadFromFileExample : MonoBehaviour
{
    void Start()
    {
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "blue"));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Blue");
        Instantiate(prefab);
    }

}
