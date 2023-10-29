using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XVNML2U.Mono;

public class MainDialogueReceiver : MonoBehaviour
{
    [SerializeField]
    XVNMLDialogueControl _target;

    // Start is called before the first frame update
    void Start()
    {
        XVNMLModule mainModule = GameManager.MainXVNMLModule;
        _target.SetModule(mainModule);
    }
}
