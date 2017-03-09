using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    
    public UIController UI;

    protected Transform oldTransform;
    protected GameObject parentContainer;
    protected int index;
    protected Vector3 size;

    private bool drag;
    private Camera mainCamera;

    // Use this for initialization
    public virtual void Awake () {

        oldTransform = this.GetComponent<Transform>();
        print("Created: " +  this.gameObject.name + " at " + oldTransform.position);

        mainCamera = FindObjectOfType<Camera>();
        UI = FindObjectOfType<UIController>();
        this.transform.parent = UI.transform;
        drag = true;
        parentContainer = UI.gameObject;
    }
	

	// Update is called once per frame
    void Update () {

        if (drag)
        {
            MoveBlock();

            if (Input.GetMouseButtonUp(0))
            {
                if (drag)
                    drag = false;

                setParent();


            }

        }

    }


    void OnTriggerEnter(Collider b)
    {
        if (drag)
        {
            if (!(b.gameObject.tag == "Action"))
            {
                print("Enter: " + b.gameObject.name);
                parentContainer = b.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider b)
    {
        if (drag) {
            if (!(b.gameObject.tag == "Action")) {
                print("Exit: " + b.gameObject.name);
                parentContainer = UI.gameObject;
            }
        }
    }


    public void MoveBlock()
    {

        Vector3 uiray = UI.cameraToUI();
        float spaceFromPanel = -0.01f;

        this.transform.position = new Vector3(uiray.x, uiray.y - this.getSize().y*0.50f, UI.transform.position.z + spaceFromPanel);


    }

    public void setIndex(int i)
    {
        index = i;
    }

    public int getIndex()
    {
        return index;
    }

    public void dragEnable()
    {
        drag = true;
    }

    public void dragDisable()
    {
        drag = false;
    }

    public virtual void setParent()
    {
        print("Setting parent with " + parentContainer.tag);
        if (parentContainer.tag == "Container")
        {
            print("parent container from superclass");
            this.transform.parent = parentContainer.transform;
            parentContainer.GetComponent<Container>().addElement(this);
        }

        else if (parentContainer.tag == "UI" && this.tag == "Action")
        {
            print("Destroy");
            Destroy(this.gameObject);
        }

        else if (parentContainer.tag == "UI" && this.tag == "Contained")
        {
            this.transform.parent = UI.transform;
        }

    }

    public bool parentIsNull()
    {
        return !(parentContainer.tag == "Container");
    }

    public Vector3 getSize()
    {
        return size;
    }

    public void setParentContainerNull()
    {
        parentContainer = UI.gameObject;
    }

    public GameObject getParentContainer()
    {
        return parentContainer;
    }

}
