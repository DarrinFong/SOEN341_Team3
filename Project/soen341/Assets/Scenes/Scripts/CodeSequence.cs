using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeSequence : MonoBehaviour {
    Transform slots;
    public static string CodeblocksSequence;
	// Use this for initialization
	void Start () {
        HasChanged();
	}

    public void HasChanged()
    {
        print("I'm in has changed");
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(",");
        foreach (Transform slotTransform in slots) {
            GameObject item = slotTransform.GetComponent<Slot>().item;
            if (item) {
                print(item.name);
                builder.Append(item.name);
                builder.Append(",");
            }
        }
        CodeblocksSequence = builder.ToString();
    }
}

namespace UnityEngine.EventSystems {
    public interface IHasChanged : IEventSystemHandler {
        void HasChanged();
    }
}
