using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    
    public UIController UI;

    protected Transform oldTransform;
    protected GameObject parentContainer;
    protected int index;
    protected Vector3 size;
    protected int type;
    protected int containerType;

    private TextMesh text;
    private bool drag;
    private Camera mainCamera;

    // Called upon object instantiation 
    public virtual void Awake () {

        oldTransform = this.GetComponent<Transform>(); //TODO:Use old transform to snap back

        text = this.gameObject.GetComponentInChildren<TextMesh>();
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
            moveBlock();

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
                //print("Enter: " + b.gameObject.name);
                parentContainer = b.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider b)
    {
        if (drag) {
            if (!(b.gameObject.tag == "Action")) {
                //print("Exit: " + b.gameObject.name);
                parentContainer = UI.gameObject;
            }
        }
    }


    public void moveBlock()
    {

        Vector3 uiray = UI.cameraToUI();
        float spaceFromPanel = -0.01f;

        this.transform.position = new Vector3(uiray.x, uiray.y - this.getSize().y*0.50f, UI.transform.position.z + spaceFromPanel);

    }

    public void setType(int t)
    {
        type = t;
        changeType();
    }

    public virtual void changeType()
    {

    }

    public void setContainerType(int t)
    {
        containerType = t;
    }

    public int getContainerType()
    {
        return containerType;
    }

    public virtual int getType()
    {
        return type;
    }

    public void setText(string str)
    {
        text.text = str;
    }

    public void setIndex(int i)
    {
        index = i;
    }

    public int getIndex()
    {
        return index;
    }


    public Vector3 getSize()
    {
        return size;
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
        //print("Setting parent with " + parentContainer.tag);
        if (parentContainer.tag == "Container")
        {
            //print("parent container from superclass");
            this.transform.parent = parentContainer.transform;
            parentContainer.GetComponent<Container>().addElement(this);
        }

        else if (parentContainer.tag == "UI" && this.tag == "Action")
        {
            //print("Destroy");
            Destroy(this.gameObject);
        }

        else if (parentContainer.tag == "UI" && this.tag == "Container")
        {
            this.transform.parent = UI.transform;
        }

    }

    public bool parentIsNull()
    {
        return !(parentContainer.tag == "Container");
    }

    public void setParentContainerNull()
    {
        parentContainer = UI.gameObject;
    }

    public GameObject getParentContainer()
    {
        return parentContainer;
    }

    public virtual IEnumerator executeBlock()
    {
        yield return new WaitForSeconds(1);
    }


}
