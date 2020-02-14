using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnityCreatorTrigger : EventTrigger
{
    private UnityCreator creator;

    private void Start()
    {
        creator = this.GetComponent<UnityCreator>();
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        //Debug.Log("OnBeginDrag called."+data);

        creator.StartDrag(data.position);
    }

    public override void OnDrag(PointerEventData data)
    {
        //Debug.Log("OnDrag called.");
        creator.OnDrag(data.position);
    }

    public override void OnEndDrag(PointerEventData data)
    {
        //Debug.Log("OnEndDrag called.");
        creator.EndDrag(data.position);
    }
}
