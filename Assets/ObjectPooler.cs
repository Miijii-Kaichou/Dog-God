using Extensions;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using static SharedData.Constants;

#if UNITY_EDITOR
[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Draw();
    }

    void Draw()
    {
        if (GUILayout.Button("Pool Into Scene"))
        {
            (serializedObject.targetObject as ObjectPooler).Initialize();
        }
    }
}
#endif //UNITY_EDITOR

public class ObjectPooler : MonoBehaviour
{
    public List<ObjectPoolItem> itemsToPool;

    public List<GameObject> pooledObjects;
    public Dictionary<int, PoolItemDependencyContainer> PooledContainers { get; private set; } = new();

    public bool spawnItemsInParent = false;

    // Start is called before the first frame update

    public int poolIndex;

    public delegate void PoolCountChangeEvent();


    private void Awake()
    {
        GatherDependencies();
    }

    /// <summary>
    /// Start up the object pooler, instantiating prefabs based on set settings.
    /// </summary>
    public void Initialize()
    {
#if UNITY_EDITOR
        Debug.Log("Initializing");
#endif //UNITY_EDITOR

        InitObjectPooler();
    }

    /// <summary>
    /// Disabled all pooled object
    /// </summary>
    private void DeactivateAllPooledObjects()
    {
        foreach (GameObject obj in pooledObjects)
        {
            obj.Disable();
        }
    }

    /// <summary>
    /// Add an item to the pool
    /// </summary>
    /// <param name="poolItem"></param>
    public void AddToPool(ObjectPoolItem poolItem)
    {
        itemsToPool.Add(poolItem);
    }

    /// <summary>
    /// Returned all of the objects pooled in the object pooler.
    /// </summary>
    /// <returns></returns>
    public GameObject[] GetPooledObjects() => pooledObjects.ToArray();


    /// <summary>
    /// Instantiate all pool items for pooling
    /// </summary>
    void InitObjectPooler()
    {
#if UNITY_EDITOR
        ObjectPoolItem item;
        GameObject newMember;
        PoolItemDependencyContainer newContainer;
        pooledObjects = new List<GameObject>();
        PooledContainers = new Dictionary<int, PoolItemDependencyContainer>();
        for (int itemID = ZERO; itemID < itemsToPool.Count; itemID++)
        {
            item = itemsToPool.Get(itemID);
            for (int i = ZERO; i < item.size; i++)
            {
                newMember = PrefabUtility.InstantiatePrefab(item.prefab, spawnItemsInParent ? transform : null) as GameObject;
                newContainer = newMember.GetComponent<PoolItemDependencyContainer>();
                item.prefab.name = item.name;
                newMember.SetActive(false);
                pooledObjects.Add(newMember);            
            }
        }
#endif
    }

    void GatherDependencies()
    {
        int id = -ONE;
        foreach (var obj in pooledObjects)
        {
            id++;
            obj.name = $"{obj.name}_{id}";
            PooledContainers.Add(id, obj.GetComponent<PoolItemDependencyContainer>());
        }
    }

    public T Fetch<T>(ref GameObject target) where T : Component
    {
        var index = target.name.Split('_')[ONE].AsNumericalValue();
        if (PooledContainers[index].Contains(typeof(T)) == false) return null;
        return (T)PooledContainers[index][typeof(T)];
    }


    /// <summary>
    /// Get a member from the object bool. If object exist, result will be the type of component
    /// the gameObject has.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public GameObject GetMember<T>(string name, out T result) where T : Component
    {
        #region Iteration

        GameObject pooledObject = pooledObjects.Find(member => member != null &&
            !member.activeInHierarchy &&
            member.name.Contains(name) &&
            PooledContainers[member.name.Split('_')[ONE].AsNumericalValue()].Contains(typeof(T)));

        if (pooledObject != null)
        {
            result = (T)PooledContainers[pooledObject.name.Split('_')[ONE].AsNumericalValue()][typeof(T)];
            return pooledObject;
        }

        #endregion

        for (int itemID = ZERO; itemID < itemsToPool.Count; itemID++)
        {
            ObjectPoolItem item = itemsToPool.Get(itemID);
            if (name == item.prefab.name && item.expandPool)
            {

                GameObject newMember = Instantiate(item.prefab);
                newMember.SetActive(false);
                pooledObjects.Add(newMember);
                if (newMember.GetComponent(typeof(T)) != null)
                {
                    result = (T)newMember.GetComponent(typeof(T));
                    return newMember;
                }
            }
        }
#if UNITY_EDITOR
        Debug.LogWarning("We couldn't find a prefab of this name " + name);
#endif //UNITY_EDITOR
        result = null;
        return null;
    }

    /// <summary>
    /// Get a member from the object pool by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetMember(string name)
    {
        ObjectPoolItem item;;

        #region Iteration
        GameObject newMember = pooledObjects.Find(member => member != null &&
        !member.activeInHierarchy &&
        member.name.Contains(name));

        if (newMember != null) return newMember;
        #endregion

        PoolItemDependencyContainer newContainer;

        for (int itemID = 0; itemID < itemsToPool.Count; itemID++)
        {
            item = itemsToPool.Get(itemID);
            if (name == item.prefab.name && item.expandPool)
            {
                newMember = Instantiate(item.prefab);
                newContainer = newMember.GetComponent<PoolItemDependencyContainer>();
                newMember.SetActive(false);
                pooledObjects.Add(newMember);
                PooledContainers.Add(pooledObjects.Count - 1, newContainer);
                return newMember;
            }
        }
#if UNITY_EDITOR
        Debug.LogWarning("We couldn't find a prefab of this name " + name);
#endif //UNITY_EDITOR
        return null;
    }

    /// <summary>
    /// Empty out the object pool
    /// </summary>
    void ClearPool()
    {
        GameObject pooledObject;
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObject = pooledObjects.Get(0);
            if (pooledObjects[i] != null && !pooledObjects[i].activeInHierarchy)
            {
                Destroy(pooledObjects[i]);
            }
        }

#if UNITY_EDITOR
        Debug.Log("Pool Cleared");
#endif //UNITY_EDITOR
    }
}
