using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OnButtonHoverEffect : MonoBehaviour
{ 
    [SerializeField]
    private Button _target;

    [SerializeField]
    private UnityEvent _onButtonHover;

    [SerializeField]
    private UnityEvent _onButtonOUt;
    
    void OnMouseOver()
    {
        _onButtonHover.Invoke();
    }

    private void OnMouseExit()
    {
        _onButtonOUt.Invoke();
    }
}
