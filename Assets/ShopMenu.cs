using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    GameObject hazelAlonoireObj;

    private const int ItemShopIndex = 0;
    private const int SkillShopIndex = 1;
    private const int MadoShopIndex = 2;
    private const int DeityShopIndex = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Immediately on start up, register this object
        // to the Heaven's Plaza System
        HeavensPlazaSystem.Register(this);
    }

    public void GoIntoItemShop()
    {
        EnterShop(ItemShopIndex);
    }

    public void GoIntoSkillShop()
    {
        EnterShop(SkillShopIndex);
    }

    public void GoIntoMadoShop()
    {
        EnterShop(MadoShopIndex);
    }

    public void GoIntoDeityShop()
    {
        EnterShop(DeityShopIndex);
    }

    void EnterShop(int index)
    {
        HeavensPlazaSystem.SetShopIndex(index);
        HeavensPlazaSystem.OpenShop();
    }
}
