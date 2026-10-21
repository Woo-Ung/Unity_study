using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTableParser : MonoBehaviour
{
    // CSV 파일 참조
    [SerializeField] private TextAsset _csvFile;

    // 파싱 결과 보관
    private Dictionary<int, EnemyData> _enemyDatas = new();

    private void Awake() => LoadTable();

    /// <summary>
    /// 파싱된 결과를 반환
    /// </summary>
    /// <param name="id">고유 키 값</param>
    /// <param name="data">찾은 적 데이터</param>
    /// <returns></returns>

    // 파실된 결과를 반환
    public bool TryGetEnemy(int id, out EnemyData data)
    {
        return _enemyDatas.TryGetValue(id, out data);
    }

    private void LoadTable()
    {
        // 1. 줄 단위로 자름
        //      RemoveEmptyEntries : \r, \n 사이, 파일 끝에 생기는 빈 줄 제거
        string[] lines = _csvFile.text.Split(
            new char[] { '\r', '\n' }, 
            StringSplitOptions.RemoveEmptyEntries
            );

        for( int i = 2; i < lines.Length;i++)
        {
            // 2. 쉼표 기준으로 나누기
            //      TSV인 경우: Split('\t');
            string[] cells = lines[i].Split(',');

            if(cells.Length != 7)
            {
                Debug.LogError($"뭔가 이상함");
                continue;
            }

            int id, maxHp, attackValue;
            string name;
            float detectRange, attackRange, speed;

            // 3. 각 데이터 형으로 변환
            if(!int.TryParse(cells[0], out id)
                || !int.TryParse(cells[2], out maxHp)
                || !float.TryParse(cells[3], out detectRange)
                || !float.TryParse(cells[4], out attackRange)
                || !float.TryParse(cells[5], out speed)
                || !int.TryParse(cells[6], out attackValue))
            {
                Debug.LogError("여기도 뭔가 이상함");
                continue;
            }

            name = cells[1];

            _enemyDatas[id] = new EnemyData(
                id,
                name,
                maxHp,
                detectRange,
                attackRange,
                speed,
                attackValue
                );

        }
    }



}
