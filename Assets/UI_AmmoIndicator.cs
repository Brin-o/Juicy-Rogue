using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq;
using Bolt;
using TMPro;


public class UI_AmmoIndicator : MonoBehaviour
{
    
    [Header("Refferences")]
    [SerializeField] GameObject player = default;
    [SerializeField] TextMeshProUGUI text = default;
    
    //ammoUpdates
    public float maxAmmo ()
    {
        return (int)Variables.Object(player).Get("maxAmmo");
    }
    public int currentAmmo ()
    {
        return (int)Variables.Object(player).Get("currentAmmo");
    }


    void Start()
    {
        UpdateCurrentAmmoUI();
    }

    // Update is called once per frame
    public void UpdateCurrentAmmoUI()
    {
        int ammo = currentAmmo();
        text.text = ammo.ToString("D2");
        if (ammo == 10)
            text.CrossFadeAlpha(0, 1.5f, true);
        if (ammo < 10)
            text.CrossFadeAlpha(1, 0.1f, true);
    }

    void UpdateMaxAmmo()
    {
        
    }


}
