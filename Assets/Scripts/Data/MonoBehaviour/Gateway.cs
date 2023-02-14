using UnityEngine;

public class Gateway : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnSystemRegistrationProcessCompleted = GameSceneManager.Start;
    }
}
