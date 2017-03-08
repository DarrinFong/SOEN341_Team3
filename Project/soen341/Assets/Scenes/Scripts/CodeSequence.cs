using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CodeSequence : MonoBehaviour {
    [SerializeField]Transform slots;
    public static string CodeblocksSequence;
	// Use this for initialization
	void Start () {
	}

    public void HasChanged()
    {
        print("shit");
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
