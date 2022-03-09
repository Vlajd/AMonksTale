using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Animator DoorAnimatorRef;
    [SerializeField] private string[] TriggerTags;

    private Animator _buttonAnimator;

    private void Awake()
    {
        _buttonAnimator = this.transform.GetChild(0).GetComponent<Animator>();

        if (DoorAnimatorRef == null) Debug.LogWarning("No Referenced Animator for DoorAnimator on Prefab \"Button\"");
        if (_buttonAnimator == null) Debug.LogWarning("No Animator Component on prefab \"Button\"");

    }

    private void OnDisable() => _buttonAnimator.SetBool("IsOpen", false);

    private void FixedUpdate()
    {
        if (DoorAnimatorRef != null) DoorAnimatorRef.SetBool("IsOpen", false);
        _buttonAnimator.SetBool("IsOpen", false);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!this.enabled)
        {
            _buttonAnimator.SetBool("IsOpen", false);
            return;
        }
        if (!TriggerTags.Contains(collider.gameObject.tag)) return;
        if (DoorAnimatorRef != null) DoorAnimatorRef.SetBool("IsOpen", true);
        _buttonAnimator.SetBool("IsOpen", true);
    }
}
