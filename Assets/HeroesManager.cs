using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroesManager : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;

    private Coroutine _mergeHeroes;
    private Coroutine _levelEnd;
    private float _timeElapsed;
    private float _lerpDuration = 0.5f;
    private List<GameObject> _tempHeroes = new List<GameObject>();

    private void Start()
    {
        InputManager.CollectedHeroesEvent.AddListener(MergeHeroes);
    }
    private void MergeHeroes(List<GameObject> collectedHeroes)
    {
        _tempHeroes = collectedHeroes;
        _mergeHeroes = StartCoroutine(MergeHeroesCoroutine(_tempHeroes));
    }
    IEnumerator MergeHeroesCoroutine(List<GameObject> collectedHeroes)
    {
        foreach (GameObject x in collectedHeroes)
        {
            _timeElapsed = Time.deltaTime;
            while (x.transform.position != new Vector3(0, 0, 7))
            {          
                if (_timeElapsed < _lerpDuration)
                {
                    x.transform.position = Vector3.Lerp(x.transform.position, new Vector3(0, 0, 7), _timeElapsed / _lerpDuration);
                    _timeElapsed += Time.deltaTime;
                    yield return new WaitForFixedUpdate();
                }               
            }         
        }
        MergeDestroy();       
    }
    private void MergeDestroy()
    {       
        StopCoroutine(_mergeHeroes);
        _mergeHeroes = null;
        for (int i = _tempHeroes.Count - 1; i > 0; i--)
        {
            Destroy(_tempHeroes[i]);
        }
        _levelEnd = StartCoroutine(LevelEnd());
        _tempHeroes[0].GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = _tempHeroes.Count.ToString();   
    }
    IEnumerator LevelEnd()
    {
        yield return new WaitForSecondsRealtime(1f);
        if (_tempHeroes.Count > 12)
        {
            _winScreen.SetActive(true);
        }
        else
        {
            _loseScreen.SetActive(true);
        }
    }
    public void LevelStart()
    {
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
        StopCoroutine(_levelEnd);
        _levelEnd = null;
        _tempHeroes.Clear();
    }
}
