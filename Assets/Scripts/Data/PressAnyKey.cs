using Extensions;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static SharedData.Constants;

public class PressAnyKey : MonoBehaviour
{
    EventCall OnPress;
    IEnumerator response;

    [SerializeField, Header("Title Image")]
    TranslationTween titleImage;

    [SerializeField, Header("Press Any Key Text")]
    TextMeshProUGUI TMP_PressAnyKey;

    [SerializeField, Header("Profile Selection Object")]
    VerticalLayoutGroup profileSelectionGroup;

    private void Awake()
    {
        SetUpEvents();
    }

    private void SetUpEvents()
    {
        OnPress = EventManager.AddEvent(900, "PressAnyButtonEvent", () =>
        {
            titleImage.Activate();
            TMP_PressAnyKey.gameObject.Disable();
            profileSelectionGroup.gameObject.Enable();
            RemoveEvent();
        });
    }

    void RemoveEvent()
    {
        OnPress.Reset();
    }

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
                OnPress.Trigger();
                yield return false;
            }

            yield return null;
        }
    }
}
