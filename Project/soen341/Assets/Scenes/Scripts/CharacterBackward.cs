using UnityEngine;
using UnityEngine.UI;

public class CharacterBackward : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var backButton = transform.GetComponent<Button>();
        backButton.GetComponent<Button>().onClick.AddListener(delegate {
            clicked();
        });
    }

    public void clicked()
    {
        print("backward");
        CharacterActions charController = GameObject.FindObjectOfType<CharacterActions>();
        charController.backward();
    }
}