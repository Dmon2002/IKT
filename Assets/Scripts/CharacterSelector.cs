using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image _characterImage;
    [SerializeField] private TMP_Text _characterName;

    private int _currentIndex;

    [SerializeField] private List<SelectableCharacter> selectableCharacters;

    private void Start()
    {
        SelectCharacter(0);
    }


    private void SelectCharacter(int index)
    {
        if (index>=0 && selectableCharacters.Count>index)
        {
            _characterImage.sprite = selectableCharacters[index].sprite;
            _characterName.text= selectableCharacters[index].name;
            _currentIndex = index;
        }
    }


    public void SelectNext()
    {
        if (_currentIndex + 1 < selectableCharacters.Count)
        {
            SelectCharacter(_currentIndex + 1);
        }
        else
        {
            SelectCharacter(0);
        }
    }

    public void SelectPrevious()
    {
        if (_currentIndex - 1 >= 0)
        {
            SelectCharacter(_currentIndex - 1);
        }
        else
        {
            SelectCharacter(selectableCharacters.Count-1);
        }
    }
}
