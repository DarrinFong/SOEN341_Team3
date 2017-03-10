using UnityEngine;
using UnityEngine.UI;

public class CharacterLeft : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var leftButton = transform.GetComponent<Button>();
        //leftButton.GetComponent<Button>().onClick.AddListener(delegate {
           // clicked();
       // });
    }

    public void clicked()
    {
        print("left");
        CharacterActions charController = GameObject.FindObjectOfType<CharacterActions>();
        charController.left();
    }
}