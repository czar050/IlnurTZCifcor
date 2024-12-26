using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using DG.Tweening;

public class FactsManager : MonoBehaviour
{
    public TextMeshProUGUI dogFactText;
    [SerializeField] private GameObject _factsTextPanel;
    RectTransform tabRectTransform;

    private void Start()
    {
        tabRectTransform = _factsTextPanel.GetComponent<RectTransform>();
        StartCoroutine(GetRandomDogFact());
    }

    private IEnumerator GetRandomDogFact()
    {
        string dogFactUrl = "https://dogapi.dog/api/v2/facts";
        UnityWebRequest dogFactRequest = UnityWebRequest.Get(dogFactUrl);
        yield return dogFactRequest.SendWebRequest();

        if (dogFactRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка при получении факта о собаке: " + dogFactRequest.error);
        }
        else
        {
            DogFactResponse dogFactResponse = JsonUtility.FromJson<DogFactResponse>(dogFactRequest.downloadHandler.text);

            if (dogFactResponse != null && dogFactResponse.data != null && dogFactResponse.data.Count > 0)
            {
                string randomFact = dogFactResponse.data[Random.Range(0, dogFactResponse.data.Count)].attributes.body;
                dogFactText.text = $"Факт о собаке: {randomFact}";
                Debug.Log($"Факт о собаке: {randomFact}");
            }
            else
            {
                Debug.LogError("Не удалось разобрать данные о факте.");
            }
        }
    }

    public void NextFact()
    {
        StartCoroutine(GetRandomDogFact());
        _factsTextPanel.transform.localScale = Vector3.zero;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_factsTextPanel.transform.DOScale(1, 0.5f));
        sequence.Join(tabRectTransform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360));
    }
}

[System.Serializable]
public class DogFactResponse
{
    public List<DogFact> data;
}

[System.Serializable]
public class DogFact
{
    public string id;
    public string type;
    public DogFactAttributes attributes;
}

[System.Serializable]
public class DogFactAttributes
{
    public string body;
}