using UnityEngine;

public class AppManager : MonoBehaviour
{
    [SerializeField] private GameObject _weatherPanel;
    [SerializeField] private GameObject _factsPanel;
    private bool _isWeather = true;

    public void WeatherClick()
    {
        _isWeather = true;
    }

    public void FactsClikc()
    {
        _isWeather = false;
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
