using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class JsonTester : MonoBehaviour
{
    [SerializeField] private EnemyData _enemy;
    [SerializeField] private List<EnemyData> _enemies;

    private void Start()
    {
        RoundTripList();
    }

    private void RoundTripList()
    {
        List<EnemyData> enemies = new List<EnemyData>();
        enemies.Add(new EnemyData(1, "Goblin", 3, 6, 1.5f, 3, 1));
        enemies.Add(new EnemyData(2, "Orc", 5, 5, 1.5f, 2, 2));

        string json = JsonUtility.ToJson(new EnemyList(enemies));
        Debug.Log($"JsonTester: {json}");

        EnemyList loaded = JsonUtility.FromJson<EnemyList>(json);
        Debug.Log($"JsonTester: 적 {loaded.Enemies.Count}종류");
    }

    private void PrintListDirectly()
    {
        List<EnemyData> enemies = new List<EnemyData>();
        enemies.Add(new EnemyData(1, "Goblin", 3, 6, 1.5f, 3, 1));
        enemies.Add(new EnemyData(2, "Orc", 5, 5, 1.5f, 2, 2));

        string json = JsonUtility.ToJson(enemies);
        Debug.Log($"JsonTester: {json}");
    }

    private void RoundTripEnemy()
    {
        EnemyData goblin = new EnemyData(1, "Goblin", 3, 6, 1.5f, 3, 1);
        string json = JsonUtility.ToJson(goblin);

        EnemyData loaded = JsonUtility.FromJson<EnemyData>(json);
        Debug.Log($"JsonTester: {loaded.Name} 감지 {loaded.DetectRange}");
    }

    private void CheckKeyTypo()
    {
        string json = "{\"_name\":\"Orc\",\"_maxHP\":5}";
        EnemyData loaded = JsonUtility.FromJson<EnemyData>(json);
        Debug.Log($"JsonTester: {loaded.Name} 체력 {loaded.MaxHP}");
    }

    private void PrintInspectorEnemy()
    {
        string json = JsonUtility.ToJson(_enemy);
        Debug.Log($"JsonTester: {json}");
    }

    private void PrintJson()
    {
        EnemyData goblin = new EnemyData(1, "Goblin", 3, 6f, 1.5f, 3f, 1 );
        string json = JsonUtility.ToJson( goblin, true );
        Debug.Log($"JsonTester: {json}");
    }
}
