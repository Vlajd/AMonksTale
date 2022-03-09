using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueDontDestroy : MonoBehaviour
{
    private static UniqueDontDestroy _instance;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
