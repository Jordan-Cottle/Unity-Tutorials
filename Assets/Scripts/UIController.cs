using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    private City city;

    [SerializeField]
    private Text dayText;
    [SerializeField]
    private Text cityText;
    // Start is called before the first frame update
    void Start()
    {
        city = GetComponent<City>();
    }

    // Update is called once per frame
    public void UpdateDayCount()
    {
        dayText.text = $"Day: {city.Day}";
    }

    public void UpdateCityData()
    {
        cityText.text = $"Cash: ${city.Cash} (+ ${city.CalculateCash()})\n"
        + $"Population: {city.PopulationCurrent}/{city.PopulationCeiling}\n"
        + $"Food: {city.Food}\n"
        + $"Jobs: {city.JobsCurrent}/{city.JobsCeiling}";
    }
}
