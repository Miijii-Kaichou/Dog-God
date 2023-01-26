using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressAnyKey : MonoBehaviour
{
    public UnityEvent OnPress;
    IEnumerator response;

    // Start is called before the first frame update
    void Start()
    {
        response = WaitForResponse();
        StartCoroutine(response);
    }


    IEnumerator WaitForResponse()
    {
        while (true)
        {
            //We'll keep running this loop
            //until the player hits any key

            if (Input.anyKeyDown)
            {
                OnPress.Invoke();
                yield return false;
            }

            yield return null;
        }
    }
}
