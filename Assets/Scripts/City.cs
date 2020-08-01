using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public int Cash { get; set; }
    public int Day { get; set; }
    public int PopulationCurrent { get; set; }
    public int PopulationCeiling { get; set; }
    public int JobsCurrent { get; set; }
    public int JobsCeiling { get; set; }
    public int Food { get; set; }

    public int[] buildingCounts = new int[3];

    public static readonly int FARM = 0;
    public static readonly int HOUSE = 1;
    public static readonly int FACTORY = 2;

    private UIController uIController;

    // Start is called before the first frame update
    void Start()
    {
        Cash = 10000;

        uIController = GetComponent<UIController>();

        // Start on Day 1
        EndTurn();
    }

    public void EndTurn()
    {
        Day++;
        CalculateJobs();
        CalculateFood();
        Cash += CalculateCash();
        CalculatePopulation();

        uIController.UpdateDayCount();
        uIController.UpdateCityData();
    }

    void CalculateJobs()
    {
        JobsCeiling = buildingCounts[FACTORY] * 10;
        JobsCurrent = Mathf.Min(PopulationCurrent, JobsCeiling);
    }

    public int CalculateCash()
    {
        return JobsCurrent * 2;
    }

    void CalculateFood()
    {
        Food += buildingCounts[FARM] * 5;
    }

    void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[HOUSE] * 5;

        Food -= PopulationCurrent / 4;
        if (Food >= PopulationCurrent && PopulationCurrent < PopulationCeiling)
        {
            PopulationCurrent = Mathf.Min(PopulationCurrent + (Food / 4), PopulationCeiling);
        }
        else if (Food <= PopulationCurrent)
        {
            PopulationCurrent -= (PopulationCeiling - Food) / 2;
        }
    }
}
