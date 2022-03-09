using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private GameObject Pausemenu;
    
    [HideInInspector] Pausemenu _currentPausemenu;

    private void Awake()
    {
        if (Pausemenu == null) Debug.LogWarning("No Pausemenu Referenced!");
    }

    public void InitPausemenu()
    {
        GameObject tempPausemenu = Instantiate(Pausemenu, this.transform.position, Quaternion.identity);
        tempPausemenu.transform.SetParent(this.transform);
        _currentPausemenu = tempPausemenu.GetComponent<Pausemenu>();
        Time.timeScale = 0.0f;
    }

    public void ResumePausemenu()
    {
        _currentPausemenu._resume();
        Time.timeScale = 1.0f;
    }
}
