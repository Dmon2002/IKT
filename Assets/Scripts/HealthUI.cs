using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private int StartHP;
    [SerializeField]
    private AliveObject playerHP;
    private float currentHP=-1;
    [SerializeField]
    private List<Image> icons;
    // Update is called once per frame

    private void Start()
    {
        playerHP.HP = StartHP;
    }
    void FixedUpdate()
    {
        if (playerHP.HP != currentHP)
        {
            UpdateHP();
        }
    }

    private void UpdateHP()
    {
        for(int i=0;i< icons.Count; i++)
        {
            if (playerHP.HP > i)
            {
                icons[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                icons[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}
