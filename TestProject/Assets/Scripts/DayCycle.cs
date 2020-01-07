using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DayCycle : GameInfo
{
    public Text hourText;
    public Text moneyText;
    private bool moneyEarnedDay = false;

    public GameObject sun;
    public GameObject moon;

    public GameObject[] housesForMaterials;
    public Material windowlightOn;
    public Material windowlightOff;
    Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        hour = initialTime;

        hourText.text = hour.ToString() + ":00";
        StartCoroutine(NextTime());

        //money = startingMoney;
        //moneyText.text = money.ToString() + "$";
    }

    // Update is called once per frame
    void Update()
    {
        sun.transform.RotateAround(Vector3.zero, Vector3.forward, sunSpeed * Time.deltaTime);
        sun.transform.LookAt(Vector3.zero);

        moon.transform.RotateAround(Vector3.zero, Vector3.forward, sunSpeed * Time.deltaTime);
        moon.transform.LookAt(Vector3.zero);

        //moneyText.text = money.ToString() + "$";

        if (hour > 18 && hour < 7)
        {
            for (int i = 0; i < 8; i++)
            {
                materials = housesForMaterials[i].GetComponent<Renderer>().materials;
                materials[7] = windowlightOn;
                materials[8] = windowlightOn;
                housesForMaterials[i].GetComponent<Renderer>().materials = materials;
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                materials = housesForMaterials[i].GetComponent<Renderer>().materials;
                materials[7] = windowlightOff;
                materials[8] = windowlightOff;
                housesForMaterials[i].GetComponent<Renderer>().materials = materials;
            }
        }
    }

    void PrintHour(float _hour)
    {
        if (_hour < 10)
        {
            hourText.text = "0" + hour.ToString() + ":00";
        }
        else
        {
            hourText.text = hour.ToString() + ":00";
        }
        Debug.Log("Num Workers DayCycle: "+numWorkers);
    }

    IEnumerator NextTime()
    {
        yield return new WaitForSeconds(durationOfHourInSeconds);
        if (hour + 1 > 23)
        {
            hour = 0;
        }
        else
        {
            hour += 1;
        }

        if (lunchShiftGranted == false)
        {
            if (hour > shiftStart && hour < shiftEnd)
            {
                FindObjectOfType<Tutorial>().incomePerDay += (numWorkers * piecesBuiltPerHourByWorker) * priceOfSalePerPieceBuilt;

                FindObjectOfType<Tutorial>().piecesBuilt += numWorkers * piecesBuiltPerHourByWorker;
            }
        }
        else
        {
            if (hour > shiftStart && hour < lunchShiftBegin)
            {
                FindObjectOfType<Tutorial>().incomePerDay += (numWorkers * piecesBuiltPerHourByWorker) * priceOfSalePerPieceBuilt;
                FindObjectOfType<Tutorial>().piecesBuilt += numWorkers * piecesBuiltPerHourByWorker;
            }
            else if (hour > lunchShiftEnd && hour < shiftEnd)
            {
                FindObjectOfType<Tutorial>().incomePerDay += (numWorkers * piecesBuiltPerHourByWorker) * priceOfSalePerPieceBuilt;
                FindObjectOfType<Tutorial>().piecesBuilt += numWorkers * piecesBuiltPerHourByWorker;
            }
            else
            {
                Debug.Log("Workers resting");
            }
        }

        if(hour == 6)
        {
            FindObjectOfType<Tutorial>().GetMoneyFunction();
        }


        PrintHour(hour);
        Debug.Log("Income: " + FindObjectOfType<Tutorial>().incomePerDay);
        StartCoroutine(NextTime());
    }

    IEnumerator GetMoney()
    {
        yield return new WaitForSeconds(60);
        //if (moneyEarnedDay == false)
        //{
        money += ((earningsPerWorker * workersHappiness) * numWorkers) + incomePerDay;
        Debug.Log("Money: " + money);
        //moneyText.text = money.ToString() + "$";
        moneyEarnedDay = true;
        incomePerDay = 0;
        //}
        StartCoroutine(GetMoney());
    }

    void GetMoneyFunction()
    {
        money += ((earningsPerWorker * workersHappiness) * numWorkers) + incomePerDay;
        Debug.Log("Money: " + money);
        //moneyText.text = money.ToString() + "$";
        moneyEarnedDay = true;
        incomePerDay = 0;
    }
}
