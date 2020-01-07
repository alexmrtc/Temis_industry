using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public float workersHappiness = 0.5f;
    public float companyHappiness = 0.5f;
    public float investorsHappiness = 0.5f;

    public int hour;
    public int initialTime = 6;

    public float sunSpeed = 6f;
    public float durationOfHourInSeconds = 2.5f;

    public int numWorkers = 10;
    public float earningsPerWorker = 200;
    public float workerCost = 1000;

    public int piecesBuiltPerHourByWorker = 4;
    public float priceOfSalePerPieceBuilt = 750;

    public int housesForWorkers = 1;
    public float houseConstructionPrice = 100000;
    public float houseMaintainancePrice = 2000;


    public const float startingMoney = 500000;
    public float money;

    public float incomePerDay = 0;

    public bool lunchShiftGranted = false;
    public int lunchShiftBegin = 13;
    public int lunchShiftEnd = 15;

    public int shiftStart = 6;
    public int shiftEnd = 21;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
