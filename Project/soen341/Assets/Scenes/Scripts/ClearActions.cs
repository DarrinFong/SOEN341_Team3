using UnityEngine;
using UnityEngine.UI;

public class ClearActions : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var runButton = transform.GetComponent<Button>();
        runButton.GetComponent<Button>().onClick.AddListener(delegate {
            clicked();
        });
    }

    public void clicked()
    {
        print("clear actions");
        CharacterActions charController = GameObject.FindObjectOfType<CharacterActions>();
        charController.clearActions();
    }
}