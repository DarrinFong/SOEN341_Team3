using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Block {


    public Transform header;
    public Transform arm;
    public Transform footer;

    private static readonly Vector3 DEFAULT_ARMSIZE = new Vector3(0.25f, 1, 0.05f);
    private static readonly Vector3 DEFAULT_ARMPOS = new Vector3(-1.625f, 0.173f, 0);
    private static readonly Vector3 DEFAULT_FOOTPOS = new Vector3(-0.874f, -0.45f, 0);

    protected List<Block> elements = new List<Block>();


	// Use this for initialization
	void Start () {
        this.size = new Vector3(header.localScale.x, header.localScale.y + arm.localScale.y + footer.localScale.y, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(elements.Count > 0)
            positionBlockList();
    }

    public void addElement(Block block)
    {

        //print("Added " + block.gameObject.name + " to the container list");
       
        Vector3 blockSize = block.getSize(); 

        elements.Add(block);
        block.setIndex(elements.Count - 1);

        positionBlockList();

    }

    public void positionBlockList()
    {
        if(elements[0].tag == "Action")
        {
            elements[0].gameObject.transform.localPosition = new Vector3((elements[0].getSize().x - header.localScale.x) / 2 + arm.localScale.x, header.localPosition.y - header.localScale.y / 2 - elements[0].getSize().y / 2, 0);
        }
        else if (elements[0].tag == "Container")
        {
            elements[0].gameObject.transform.localPosition = new Vector3((elements[0].getSize().x - header.localScale.x) / 2 + arm.localScale.x, -header.localScale.y, 0);
        }

        for(int i = 1; i < elements.Count; i++)
        {
            if (elements[i].tag == "Action")
            {
                elements[i].gameObject.transform.localPosition = new Vector3((elements[i].getSize().x - header.localScale.x) / 2 + arm.localScale.x, header.localPosition.y - header.localScale.y / 2 - elements[i].getSize().y / 2 - sumOfHeightsAtIndex(i), 0);
            }
            else if (elements[i].tag == "Container")
            {
                elements[i].gameObject.transform.localPosition = new Vector3((elements[i].getSize().x - header.localScale.x) / 2 + arm.localScale.x, -header.localScale.y - sumOfHeightsAtIndex(i), 0);
            }
        }

        changeArmSize();

    }

    public void changeArmSize()
    {


        this.size = new Vector3(header.localScale.x, header.localScale.y + arm.localScale.y + footer.localScale.y, 0);
       
        arm.localScale = DEFAULT_ARMSIZE + new Vector3(0, 1, 0) * sumOfHeights();
        arm.localPosition = DEFAULT_ARMPOS + new Vector3(0, -.5f, 0) * sumOfHeights();
        footer.localPosition = DEFAULT_FOOTPOS + new Vector3(0, -1, 0) * sumOfHeights();

    }

    //public void resize()
    //{
    //    size = new Vector3(header.localScale.x, header.localScale.y + arm.localScale.y + footer.localScale.y, 0);
    //}

    public float sumOfHeightsAtIndex(int h)
    {
        float sum = 0;

        for(int i = 0; i < h; i++)
            sum += elements[i].getSize().y;
        
        return sum;
    }

    public float sumOfHeights()
    {
        float sum = 0;
        foreach (Block b in elements)
        {
            Vector3 size = b.getSize();
            sum += size.y;
        }
        return sum;

    }

    public void removeElement(int index)
    {
        //fix array
        elements.RemoveAt(index);
        for(int i = index; i < elements.Count; i++)
        {
            elements[i].setIndex(i);
        }
        changeArmSize();
    }
    public void RemoveAll()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            Destroy(elements[i].gameObject);
            elements.RemoveAt(i);
            if(!this.parentIsNull())
                Destroy(this.gameObject);
        }
        changeArmSize();
    }
    public List<Block> getContainerList()
    {
        return elements;
    }

}
