using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLog : MonoBehaviour
{
    private static WeaponLog Instance;

    /*Weapon log will hold a bunch of information regarding the amount of different kinds of weapons in the
     game, and rather or not that you have obtained the weapon or not. We'll be taking this to a practicality
     level, using bitmaksing to represent the set of boolean of what the player has obtains*/

    public List<bool> obtained = new List<bool>();

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
