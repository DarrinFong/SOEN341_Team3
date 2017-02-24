using UnityEngine;
using UnityEngine.UI;

public class RunCode : MonoBehaviour
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
        print("run code");
    }
}