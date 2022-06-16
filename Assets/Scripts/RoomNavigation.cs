using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNavigation : MonoBehaviour
{

    VerticalLayoutGroup group;

    private void Start() {
        group = GetComponent<VerticalLayoutGroup>();
    }

    public void MoveUp()
    {
        int maxLength = transform.childCount;
        if((5-maxLength)*50 < group.padding.top)
            group.padding.top -= 100;
        StartCoroutine(UpdateLayoutGroup());
    }

    public void MoveDown()
    {
        group.padding.top += 100;
        if(group.padding.top > 32)
            group.padding.top = 32;
        StartCoroutine(UpdateLayoutGroup());
    }

    IEnumerator UpdateLayoutGroup()
    {
        group.enabled = false;
        yield return new WaitForEndOfFrame();
        group.enabled = true;
    }

}
