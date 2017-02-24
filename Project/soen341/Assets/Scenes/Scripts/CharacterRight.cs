using UnityEngine;
using UnityEngine.UI;

public class CharacterRight : MonoBehaviour {
    
    // Use this for initialization
    void Start() {
        var rightButton = transform.GetComponent<Button>();
        rightButton.GetComponent<Button>().onClick.AddListener(delegate {
            clicked();
        });
    }
	
    public void clicked() {
        print("right");
    }
}