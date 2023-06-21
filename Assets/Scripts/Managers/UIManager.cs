using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private PauseMenu _pauseMenu;




    //private PlayerInput _input;
    private void Start()
    {
        PlayerMovement._input.UI.Pause.performed += context => OnPause();
    }


    private void OnPause()
    {
        _pauseMenu.SwitchPause();
    }

    public void ReturnToMainMenu()
    {
        SceneChanger.ChangeScene(0);
    }

}
