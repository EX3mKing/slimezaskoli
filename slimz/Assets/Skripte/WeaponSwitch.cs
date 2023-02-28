using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private GameObject Spear;
    [SerializeField] private GameObject Shield;

    public void EnableSpear()
    {
        Spear.GetComponent<Spear>().Follow();
        Spear.SetActive(true);
        Shield.SetActive(false);
    }

    public void EnableShield()
    {
        Spear.GetComponent<Spear>().Follow();
        Spear.SetActive(false);
        Shield.SetActive(true);
    }

    public void DisableAll()
    {
        Spear.GetComponent<Spear>().Follow();
        Shield.SetActive(false);
        Spear.SetActive(false);
    }
}
