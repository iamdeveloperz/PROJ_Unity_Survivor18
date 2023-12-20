
using UnityEngine;

public class MainGameScene : MonoBehaviour
{
    public GameObject SpawnPointGroup;
    
    private void Awake()
    {
        /* 1. UI Scene 셋팅 */
        // Managers.UI.ShowSceneUI<UIMainGame>(Literals.UI_MAINGAME_Scene_Main);
        
        /* 2. Systems 셋팅 */
        SystemSetting();
        
        /* 3. Spawner and SpawnPoint 셋팅 */
        SpawnerSetting();

    }

    private void SystemSetting()
    {
        /* DayCycleSystem */
        var dayCycle = new GameObject { name = "---- Day Cycle System ----" };
        dayCycle.AddComponent<DayCycleSystem>();
        
        /* CooldownSystem */
        var cooldown = new GameObject { name = "---- Cooldown System ----" };
        cooldown.AddComponent<CooldownSystem>();
    }

    private void SpawnerSetting()
    {
        var spawner = new GameObject { name = "@ Origin Resource Spawner" };
        var originResourceSpawner = spawner.AddComponent<OriginResourceSpawner>();
        
        originResourceSpawner.SpawnPointGroup = SpawnPointGroup.transform;
    }
}
