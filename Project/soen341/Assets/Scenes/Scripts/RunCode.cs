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
        CharacterActions charController = GameObject.FindObjectOfType<CharacterActions>();
        string sequence = CodeSequence.CodeblocksSequence;
        print(sequence);
        string[] sequenceArray = sequence.Split(',');
        foreach (string item in sequenceArray)
        {
            switch (item)
            {
                case "GoForward": charController.right();
                    break;
                case "GoRight": charController.forward();
                    break;
            }
        }
        charController.runCode();
    }
}