using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    private LevelManager _levelManager;
    private Level _level;

    [Header("Camera controls")]
    [SerializeField] Camera _mainCamera;
    [SerializeField] Vector3 _posCameraTree = new Vector3(0.5f, 13, -10);
    [SerializeField] Vector3 _posCameraLevel = new Vector3(0f, 1.5f, -10);
    [SerializeField] float _speed = 2f;
    [SerializeField] bool _button;


    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _level = FindObjectOfType<Level>();
    }
    private void Update()
    {
        MoveCamera();
        if (Input.GetKeyDown(KeyCode.L))
        {
            _levelManager.LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _levelManager.ReloadLevel();
        }
    }

    private void MoveCamera()
    {
        
        if (_button)
        {
            
            _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _posCameraTree, _speed * Time.deltaTime);
            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, 60, (0.8f)*Time.deltaTime);
            if (((_mainCamera.fieldOfView - 60f) < 0.05f))
                _mainCamera.fieldOfView = 60;
        }
        else
        {
            
            _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _posCameraLevel, _speed * Time.deltaTime);
            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, 70, (0.8f) * Time.deltaTime);
            if ((Mathf.Abs(_mainCamera.fieldOfView - 70f) < 0.05f))
                _mainCamera.fieldOfView = 70;
        }
    }

    private void RemoveMoves()
    {
        //onMovement()
        //_levelMoves --

    }    
}
