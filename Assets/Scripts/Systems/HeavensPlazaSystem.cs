#nullable enable

using UnityEngine;
using static SharedData.Constants;


public class HeavensPlazaSystem : GameSystem
{
    private static HeavensPlazaSystem? Self;

    // Reference to the shop menu
    private static ShopMenu? Menu;

    // The index of the current shop being displayed
    public static int ShopIndex {get; private set;}


    protected override void OnInit()
    {
        Self ??= GameManager.GetSystem<HeavensPlazaSystem>();
    }

    protected override void Main()
    {
        base.Main();
    }

    public static void OpenShop()
    {
        int buildIndex = ShopBaseIndex + ShopIndex;
        GameSceneManager.LoadScene(buildIndex, true);
    }

    public static void SetShopIndex(int targetIndex)
    {
        if (Self == null) return;
        ShopIndex = targetIndex;
    }

    public static void Register(ShopMenu instance)
    {
        Menu ??= instance;
    }
}