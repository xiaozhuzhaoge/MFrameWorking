using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBag : MonoBehaviour {

    public Transform content;
    public int num;
    public List<GameObject> blocks = new List<GameObject>();
    public string[] itemName;

    void Awake()
    {
        content = transform.Find("Viewport/Content"); 
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < num; i++)
        {
            GameObject item = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Block"));
            item.transform.SetParent(content, false);
            item.name = i.ToString();
            blocks.Add(item);
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject item = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Item"));
             
            item.transform.SetParent(blocks[i].transform, false);
            item.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + itemName[i]);
            item.name = itemName[i];
            item.AddComponent<UIItem>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
