using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyTableParser _parser;
    [SerializeField] private int _id;
    [SerializeField] private EnemyData _data;

    private void Start()
    {
        _parser.TryGetEnemy(_id, out _data);
    }



}
