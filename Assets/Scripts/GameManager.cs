using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    private LevelManager _levelManager;
    private Level _level;

    [SerializeField] Text _moves;

    [Header("Camera controls")]
    [SerializeField] Camera _mainCamera;
    [SerializeField] Vector3 _posCameraTree = new Vector3(0.5f, 13, -10);
    [SerializeField] Vector3 _posCameraLevel = new Vector3(0f, 1.5f, -10);
    [SerializeField] float _speed = 2f;
    public bool _button = false;

    [Header("Animal List")]
    [SerializeField] Animal[] _animalList;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _level = FindObjectOfType<Level>();
    }
    private void Update()
    {
        //MoveCamera();
        UpdateMoves();
        OnSuccess();
        OnGameOver();
    }

    private void OnSuccess()
    {
        if (AreAnimalsDeath())
        {
            _levelManager.LoadNextLevel();
        }
    }

    private void OnGameOver()
    {
        if (_level._moves == 0)
        {
            _levelManager.ReloadLevel();
        }
    }

    private void UpdateMoves()
    {
        _moves.text = $"Mosse: {_level._moves}";
    }

    private void MoveCameraDown()
    {
        _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _posCameraLevel, _speed * Time.deltaTime);
        _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, 70, (0.8f) * Time.deltaTime);
        if ((Mathf.Abs(_mainCamera.fieldOfView - 70f) < 0.05f))
            _mainCamera.fieldOfView = 70;
    }



    public void RemoveMoves()
    {
        _level._moves--;
    }
    
    private bool AreAnimalsDeath()
    {
        foreach(Animal animal in _animalList)
        {
            if(animal.gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    //public void PlayButton()
    //{
    //    StartCoroutine(MoveCameraDown());
    //}

    

    //private void MoveCamera()
    //{

    //    if (_button)
    //    {            
    //        _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _posCameraTree, _speed * Time.deltaTime);
    //        _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, 60, (0.8f)*Time.deltaTime);
    //        if (((_mainCamera.fieldOfView - 60f) < 0.05f))
    //            _mainCamera.fieldOfView = 60;
    //    }
    //    else
    //    {

    //        _mainCamera.transform.position = Vector3.MoveTowards(_mainCamera.transform.position, _posCameraLevel, _speed * Time.deltaTime);
    //        _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, 70, (0.8f) * Time.deltaTime);
    //        if ((Mathf.Abs(_mainCamera.fieldOfView - 70f) < 0.05f))
    //            _mainCamera.fieldOfView = 70;
    //    }
    //}
}
