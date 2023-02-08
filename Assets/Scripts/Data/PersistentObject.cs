using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// A special singleton compnent This will keep an object
/// persistent until call to be disabled.
/// </summary>
public class PersistentObject : MonoBehaviour
{
    public bool forceSingleton = false;
    [SerializeField]
    private Component[] _componentReferences = new Component[0];
    public string POID { get; private set; }
    private GameObject Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
            if (forceSingleton && PersistentObjectHierarchy.OneExists(Instance))
            {
                Debug.Log($"An object by the name \"{Instance.name}\" already exists");
                Destroy(gameObject);
                return;
            }
            POID = PersistentObjectHierarchy.GetID(Instance);
            if (transform.parent != null) return;
            DontDestroyOnLoad(Instance);
        }
    }

    public void Disable()
    {
        if (!gameObject.activeSelf) return;

        gameObject.SetActive(false);
    }

    public void Enable()
    {
        if (gameObject.activeSelf) return;

        gameObject.SetActive(true);
    }

    public T GetComponent<T>(int index)
    {
        var types = from type in _componentReferences where type is T select type;
        return (T)Convert.ChangeType(types.ToArray()[index], typeof(T));
    }
}
