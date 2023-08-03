using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _pauseVisual;


    private bool _isPaused;
    public bool IsPaused {
        get
        {
            return _isPaused;
        }
        set
        {
            _pauseVisual.SetActive(value);
            _isPaused = value;
        }
    }


    private void Start()
    {
        IsPaused= false;
    }
    

    public void SwitchPause()
    {
        IsPaused=!IsPaused;
    }
}
