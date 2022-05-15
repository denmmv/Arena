using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour
{
    [SerializeField] private GameObject heroPrefab;
    public List<Vector3> Heart;
    private int _randomGuy;
    private void Start()
    {
        Reload();
    }
    public void Reload()
    {
        if (this.transform.childCount > 0)
        {
            for(int i = this.transform.childCount-1; i > -1; i--)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
        _randomGuy = Random.Range(0, 15);
        for (int x = 0; x < Heart.Count; x++)
        {
            GameObject temp = Instantiate(heroPrefab, Heart[x], Quaternion.identity, this.transform);
            if (x == _randomGuy)
            {
                temp.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = "2";
                temp.tag = "Hero2";
            }
            else
            {
                temp.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = "1";
                temp.tag = "Hero1";
            }
        }
    }
}
