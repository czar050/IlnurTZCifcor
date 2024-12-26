using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class WeatherResponse
{
    public Properties properties;
}

[System.Serializable]
public class Properties
{
    public Period[] periods;
}

[System.Serializable]
public class Period
{
    public string name;
    public int temperature;
    public string shortForecast;
}

public class WeatherManager : MonoBehaviour
{
    public TextMeshProUGUI weatherText;

    private void Start()
    {
        StartCoroutine(GetWeatherData());
    }

    private IEnumerator GetWeatherData()
    {
        string url = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                OnRequestComplete(webRequest.downloadHandler.text);
            }
        }
    }

    private void OnRequestComplete(string jsonResponse)
    {
        WeatherResponse weatherResponse = JsonUtility.FromJson<WeatherResponse>(jsonResponse);

        if (weatherResponse != null && weatherResponse.properties.periods.Length > 0)
        {
            Period todayWeather = weatherResponse.properties.periods[0];
            weatherText.text = $"Погода: {todayWeather.shortForecast}, Температура: {todayWeather.temperature}°F";
            Debug.Log($"Weather: {todayWeather.shortForecast}, Temperature: {todayWeather.temperature}°F");
        }
        else
        {
            Debug.LogError("Failed to parse weather data.");
        }
    }
}