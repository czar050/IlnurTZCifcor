using UnityEngine;
using DG.Tweening;

public class AppManager : MonoBehaviour
{
	[SerializeField] private GameObject _weatherPanel;
	[SerializeField] private GameObject _factsPanel;
	[SerializeField] private GameObject _tabImage;
	private bool _isWeather = true;
	RectTransform tabRectTransform;

	private void Start()
	{
		tabRectTransform = _tabImage.GetComponent<RectTransform>();
	}
	public void WeatherClick()
	{
		_isWeather = true;
		tabRectTransform.DOAnchorPos(new Vector2(-300, -800), 0.5f);
	}

	public void FactsClikc()
	{
		_isWeather = false;
		tabRectTransform.DOAnchorPos(new Vector2(300, -800), 0.5f);
	}

	private void Update()
	{
		if (_isWeather)
		{
			_weatherPanel.SetActive(true);
			_factsPanel.SetActive(false);
		}
		else
		{
			_weatherPanel.SetActive(false);
			_factsPanel.SetActive(true);
		}
	}
}
