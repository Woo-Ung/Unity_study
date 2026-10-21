using System;
using UnityEngine;

// JsonUtility는 [Serializable] 클래스의 [SerializeField] 혹은 public 필드만 키로 기록함.
[Serializable]
public class SaveData
{
    [SerializeField] private Vector3 _playerPosition;
    [SerializeField] private int _saveCount;
    // JsonUtility는 DateTime을 직렬화하지 못하므로 문자열로 보관
    // 이렇게 직렬화할 수 없는 타입이 있는지 체크할 것
    [SerializeField] private string _savedAt;

    public Vector3 PlayerPosition => _playerPosition;
    public int SaveCount => _saveCount;
    public string SavedAt => _savedAt;

    public SaveData(Vector3 playerPosition, int saveCount, string savedAt)
    {
        _playerPosition = playerPosition;
        _saveCount = saveCount;
        _savedAt = savedAt;
    }
}
