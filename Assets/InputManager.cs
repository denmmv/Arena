using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static UnityEvent<List<GameObject>> CollectedHeroesEvent = new UnityEvent<List<GameObject>>();

    private List<GameObject> _collectedHeroesGO = new List<GameObject>();
    private RaycastHit hit;
    private bool _enableCollect = true;
    private bool sendinfo = false;
    private Coroutine _collectHeroes;

    private void Update()
    {       
        if (Input.GetMouseButton(0) && _enableCollect)
        {
            _collectHeroes = StartCoroutine(CollectHeroes());           
        }
    }
    IEnumerator CollectHeroes()
    {        
        while (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if(hit.collider != null && hit.transform.tag == "Hero1")
            {
                hit.transform.tag = "HeroCollected";
                _collectedHeroesGO.Add(hit.transform.gameObject);
            }                  
                yield return new WaitForEndOfFrame();            
        }       
        if (!sendinfo&&_collectedHeroesGO.Count>0)
        {
            _enableCollect = false;
            sendinfo = true;
            SendCollectedHeroes();
        }    
    }
    private void SendCollectedHeroes()
    {
        CollectedHeroesEvent.Invoke(_collectedHeroesGO);
    }
    public void LevelStart()
    {
        StopCoroutine(_collectHeroes);
        _collectHeroes = null;
        _collectedHeroesGO.Clear();
        _enableCollect = true;
        sendinfo = false;
    }
}
