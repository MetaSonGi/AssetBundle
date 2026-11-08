# AssetBundle
에셋번들·Addressables 저장/로드 방법 예제 (Unity 프로젝트: `AssetBundle_test/`)

## 구성
| 파일 | 역할 |
|---|---|
| `Assets/Editor/BuildAssetBuledss.cs` | 메뉴 **Assets → Build AssetBundles** — 현재 빌드 타깃용 번들을 `Assets/StreamingAssets`에 생성 |
| `Assets/3.Scripts/AssetBundleLoadExample.cs` | 번들을 비동기로 열어 프리팹 생성, `FindBundlePath()`로 경로 탐색 |
| `Assets/3.Scripts/LoadFromFileExample.cs` | 동기 `LoadFromFile` 예제 |
| `Assets/3.Scripts/AddressablesManager.cs` | Addressables로 프리팹 생성·스프라이트 로드, 해제 관리 |
| `Assets/3.Scripts/AddressablesAutoRelease.cs` | Addressables로 만든 오브젝트를 정해진 시간 뒤 `ReleaseInstance`로 정리 |

## 2026-11 수정 내역
- 빌드 출력 위치를 `StreamingAssets`로 바꿔 **빌드한 앱에서도** 번들을 찾도록 수정 (기존 `Assets/AssetBundles`는 에디터 전용으로 계속 탐색)
- 빌드 타깃을 하드코딩하지 않고 현재 플랫폼(`activeBuildTarget`) 사용, 빌드 후 `AssetDatabase.Refresh`
- 번들·에셋이 없을 때 NullReference 대신 경고 로그, 파괴 시 `Unload(false)`로 메모리 해제
- `AddressablesManager`가 Start 0.1초 뒤 **자기 자신을 해제하던 버그** 제거, 키 유효성(`RuntimeKeyIsValid`)·로드 상태 확인, 스프라이트 핸들은 OnDestroy에서 해제

## 검증
Unity·Addressables API 스텁 + Mono `mcs -warnaserror+`로 컴파일, 경로 탐색·null 처리·해제 등 8항목 통과. Unity 에디터에서 실제 번들 빌드는 미검증.
