using UnityEngine;
using UnityEngine.UI;

public class CharacterForward : MonoBehaviour {
    
    // Use this for initialization
    void Start() {
        var forwardButton = transform.GetComponent<Button>();
        forwardButton.GetComponent<Button>().onClick.AddListener(delegate {
            clicked();
        });
    }
    
    public void clicked()
    {
        print("forward");
        SimpleCharacterControl sc = GameObject.FindObjectOfType<SimpleCharacterControl>();
        sc.forward();
    }
}