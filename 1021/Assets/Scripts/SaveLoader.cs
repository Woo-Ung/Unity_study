using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveLoader : MonoBehaviour
{
    private const string SAVE_FOLDER_NAME = "Saves";
    private const string FILE_NAME = "save.json";
    private const string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

    [SerializeField] private Transform _player;
    [SerializeField] private InputActionReference _saveAction;
    [SerializeField] private InputActionReference _loadAction;
    [SerializeField] private InputActionReference _deleteAction;

    private string _folder;
    private string _path;
    private int _saveCount;

    private void Awake()
    {
        CachePath();
        PrintSaveFolder();
    }

    private void OnEnable() => BindInputEvents();
    private void Start() => Load();
    private void OnDisable() => UnbindInputEvents();

    public void Save()
    {
        _saveCount++;

        // 1. 저장할 값을 직렬화 가능한 객체 하나로 묶음
        string savedAt = DateTime.Now.ToString(TIME_FORMAT);
        SaveData data = new SaveData(_player.position, _saveCount, savedAt);

        // 2. 객체에서 JSON 문자열로
        //   prettyPrint true : 사람이 파일을 열어 읽기 쉽게 줄바꿈/들여쓰기
        string json = JsonUtility.ToJson(data, true);

        // 3. 저장 폴더가 없으면 생성 (이미 있으면 아무 일도 하지 않음)
        Directory.CreateDirectory(_folder);

        // 4. 문자열에서 파일로 (같은 이름의 파일이 있으면 통째로 덮어씀)
        File.WriteAllText(_path, json);

        Debug.Log($"SaveLoader : {_saveCount}번째로 저장함 ({savedAt})");
    }
    
    public void Load()
    {
        // 1. 처음 실행이거나 파일을 지운 경우 - 씬에 놓인 자리 그대로 시작
        if (!File.Exists(_path))
        {
            Debug.Log("SaveLoader : 저장 파일이 없음.");
            return;
        }

        // 2. 파일에서 JSON 문자열로
        string json = File.ReadAllText(_path);

        // 3. 문자열에서 객체로 (형식이 깨진 파일이면 null 또는 예외)
        if (!TryParseSaveData(json, out SaveData data))
        {
            return;
        }

        // 4. 객체의 값을 게임 상태에 반영
        _player.position = data.PlayerPosition;
        _saveCount = data.SaveCount;

        Debug.Log($"SaveLoader : {_saveCount}번째 저장을 불러왔음. ({data.SavedAt})");
    }

    public void DeleteSave()
    {
        if (!File.Exists(_path))
        {
            Debug.Log("SaveLoader: 지울 저장 파일이 없음.");
            return;
        }

        File.Delete(_path);
        _saveCount = 0;
        Debug.Log("SaveLoader: 저장 파일을 지웠음.");
    }

    private void CachePath()
    {
        // dataPath: 에디터에서는 프로젝트의 Assets 폴더의 상위(프로젝트 루트)의 Saves 폴더 사용
        //   Assets 안에 쓰면 Unity가 파일을 에셋으로 가져오므로 Assets 밖에 둠
        //   빌드에서는 dataPath가 <게임>_Data 폴더라 쓰기 권한이 없을 수 있음 - 배포용은 persistentDataPath 사용
        _folder = Path.GetFullPath(Path.Combine(Application.dataPath, "..", SAVE_FOLDER_NAME));

        // Path.Combine : 폴더와 파일 이름 사이 구분자('/')를 빠뜨리지 않고 연결
        _path = Path.Combine(_folder, FILE_NAME);
    }

    private void BindInputEvents()
    {
        _saveAction.action.Enable();
        _loadAction.action.Enable();
        _deleteAction.action.Enable();
        _saveAction.action.performed += OnSavePerformed;
        _loadAction.action.performed += OnLoadPerformed;
        _deleteAction.action.performed += OnDeletePerformed;
    }

    private void UnbindInputEvents()
    {
        _saveAction.action.Disable();
        _loadAction.action.Disable();
        _deleteAction.action.Disable();
        _saveAction.action.performed -= OnSavePerformed;
        _loadAction.action.performed -= OnLoadPerformed;
        _deleteAction.action.performed -= OnDeletePerformed;
    }

    private void OnSavePerformed(InputAction.CallbackContext context) => Save();

    private void OnLoadPerformed(InputAction.CallbackContext context) => Load();

    private void OnDeletePerformed(InputAction.CallbackContext context) => DeleteSave();

    private bool TryParseSaveData(string json, out SaveData data)
    {
        data = null;

        try // 시도하셈
        {
            // JSON의 키를 같은 이름의 필드에 채워 새 SaveData 생성 (없는 키는 기본값으로 남음)
            data = JsonUtility.FromJson<SaveData>(json);
        }
        catch (ArgumentException e) // 실패하면 이렇게 하셈. e는 실패 원인.
        {
            // 쉼표/괄호가 어긋난 JSON은 예외로 게임이 멈추지 않게 로그만 남김
            Debug.LogWarning($"SaveLoader : 저장 파일 형식이 잘못되어 불러오지 않음 - {e.Message}");
            return false;
        }

        // 빈 파일은 예외 없이 null을 돌려줌
        if (data == null)
        {
            Debug.LogWarning("SaveLoader : 저장 파일이 비어 있음.");
            return false;
        }

        return true;
    }

    private void PrintSaveFolder()
    {
        Debug.Log($"SaveLoader : 저장 폴더 {_folder}");
    }
}
