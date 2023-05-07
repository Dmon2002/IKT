using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordUI : MonoBehaviour
{
    [SerializeField]
    private Weapon PlayerWeapon;

    [SerializeField]
    private float waitTime;
    private float currentTime;
    [SerializeField]
    private Image Icon;

    [SerializeField]
    private GameObject[] Sword;

    private void Start()
    {
        Sword[0].SetActive(true);
        Sword[1].SetActive(false);
    }
    public void SetCritical()
    {
        AudioManager.instance.PlayDamageBuff();
        StopAllCoroutines();
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        currentTime = waitTime;
        PlayerWeapon.IsCritical = true;
        Sword[0].SetActive(false);
        Sword[1].SetActive(true);
        while (currentTime>0)
        {
            Icon.fillAmount = currentTime / waitTime;
            yield return new WaitForFixedUpdate();
            currentTime -= Time.deltaTime;
        }
        Sword[0].SetActive(true);
        Sword[1].SetActive(false);
        PlayerWeapon.IsCritical = false;
    }
}
