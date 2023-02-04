using UnityEngine;
using VSCodeEditor;

public class GameManager : MonoBehaviour
{
    [Header("Camera controls")]
    [SerializeField] Camera _mainCamera;
    [SerializeField] Vector3 _posCameraTree = new Vector3(0.5f, 13, -10);
    [SerializeField] Vector3 _posCameraLevel = new Vector3(0f, 1.5f, -10);
    [SerializeField] float _speed = 2f;
    [SerializeField] bool _button;

    [Header("Levels")]
    [SerializeField] GameObject[] _allLevels;

    private void Update()
    {
        MoveCamera();
    }
    public void GameOver()
    {     
        //change tree
        
        
        

        //change level
    }

    private void MoveCamera()
    {
        if (_button)
        {
            _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _posCameraTree, _speed * Time.deltaTime);
            if (_mainCamera.transform.position == _posCameraTree)
            {
                _mainCamera.orthographic = false;
                ChangeLevel();
            }
        }
        else
        {
            _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _posCameraLevel, _speed * Time.deltaTime);
            if (_mainCamera.transform.position == _posCameraLevel)
                _mainCamera.orthographic = true;
        }
    }

    private void ChangeLevel()
    {
        int actualLevelIndex = FindActualLevelIndex();
        Debug.Log($"Atual Level: {actualLevelIndex}");
        if (actualLevelIndex != _allLevels.Length-1)
        {
            _allLevels[actualLevelIndex].SetActive(false);
            actualLevelIndex++;
            _allLevels[actualLevelIndex].SetActive(true);
        }
    }

    private int FindActualLevelIndex()
    {
        for(int i = 0; i < _allLevels.Length; i++)
        {
            if (_allLevels[i].activeSelf)
                return i;
        }
        return -1;
    }
}
