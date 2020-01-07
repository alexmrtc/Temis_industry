using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : GameInfo
{
    public GameObject[] houses;

    public GameObject image;

    public Text moneyText;

    public Slider workerSlider;
    public Slider companySlider;
    public Slider investorSlider;

    public float timeToWaitForGetMoney;

    //public Renderer[] rend;
    public Material windowlightOn;
    public Material windowlightOff;
    Material[] materials;

    #region Workers Panel
    public GameObject workerPanel;
    public Text numWorkersText;

    public Dropdown shiftStartDropdown;
    public Dropdown shiftEndDropdown;
    #endregion
    #region Event related
    #region Event Components in Scene
    public Text title;
    public Text dilemaText;
    public Text acceptText;
    public Text declineText;
    #endregion

    private int eventCase;

    public Dropdown ShiftEndDropdown { get => shiftEndDropdown; set => shiftEndDropdown = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        numWorkersText.text = numWorkers.ToString();


        timeToWaitForGetMoney = (shiftEnd - initialTime) * durationOfHourInSeconds;
        hour = initialTime;

        shiftStartDropdown.value = shiftStart;
        shiftEndDropdown.value = shiftEnd;

        //money = startingMoney;

        InitTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        workerSlider.value = workersHappiness;
        companySlider.value = companyHappiness;
        investorSlider.value = investorsHappiness;
    }

    public void InitTutorial()
    {
        StartCoroutine(RandomEvent());
    }


    public void ChangeShiftEnd()
    {
        shiftEnd = shiftEndDropdown.value;

        if (shiftEndDropdown.value <= shiftStart)
        {
            shiftStart = shiftEnd - 1;

            if (shiftStart < 0)
            {
                shiftStart = 23;
            }

            shiftStartDropdown.value = shiftStart;
        }

        Debug.Log("ShiftEnds at: " + shiftEnd);
    }

    public void ChangeShiftStart()
    {
        shiftStart = shiftStartDropdown.value;

        if (shiftStartDropdown.value >= shiftEnd)
        {
            shiftEnd = shiftStart + 1;

            if (shiftEnd > 23)
            {
                shiftEnd = 0;
            }

            shiftEndDropdown.value = shiftEnd;
        }


        Debug.Log("ShiftStarts at: " + shiftStart);
    }

    public void CheckIfCanAfford()
    {
        if (FindObjectOfType<DayCycle>().money - houseConstructionPrice > 0)
        {
            BuildHouse();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void BuildHouse()
    {
        houses[housesForWorkers].SetActive(true);
        IncreaseWorkerHappiness(0.1f);
        FindObjectOfType<DayCycle>().money -= houseConstructionPrice;
        //moneyText.text = money.ToString();
        image.SetActive(false);
        housesForWorkers++;
        Debug.Log(money);
    }

    #region Increase/Decrease Happiness
    void ReduceWorkerHappiness(float percent)
    {
        workersHappiness -= percent;
    }

    void ReduceCompanyHappiness(float percent)
    {
        companyHappiness -= percent;
    }

    void ReduceInvestorsHappiness(float percent)
    {
        investorsHappiness -= percent;
    }

    void IncreaseWorkerHappiness(float percent)
    {
        workersHappiness += percent;
    }

    void IncreaseCompanyHappiness(float percent)
    {
        companyHappiness += percent;
    }

    void IncreaseInvestorsHappiness(float percent)
    {
        investorsHappiness += percent;
    }
    #endregion

    public void ShowEvent()
    {
        image.SetActive(true);
    }

    public void ShowWorkerPanel()
    {
        workerPanel.SetActive(true);
    }

    public void AcceptEvent()
    {
        switch (eventCase)
        {
            case 1:
                CheckIfCanAfford();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }

    public void DeclineEvent()
    {
        switch (eventCase)
        {
            case 1:
                ReduceWorkerHappiness(0.1f);
                break;
            case 2:
                ReduceCompanyHappiness(0.1f);
                break;
            case 3:
                ReduceWorkerHappiness(0.1f);
                break;
            case 4:
                ReduceInvestorsHappiness(0.1f);
                break;
            case 5:
                ReduceWorkerHappiness(0.2f);
                break;
        }
    }

    public void GenerateEvent()
    {
        eventCase = Random.Range(1, 5);
        eventCase = 1;
        switch (eventCase)
        {
            case 1:
                title.text = "New apartments";
                dilemaText.text = "Workers would appreciate a new building for them to live so they don't have to live like they are in a prison";
                acceptText.text = "Build new house";
                declineText.text = "Don't waste money on them";
                break;
            case 2:
                title.text = "New material";
                dilemaText.text = "Our scientists have found a combination of materials that may increase the sale value of our products, but it could result in a failure";
                acceptText.text = "Research";
                declineText.text = "Keep it like always";
                break;
            case 3:
                title.text = "Workers rights";
                dilemaText.text = "Our workers demand an 8 hour shift instead of working until late";
                acceptText.text = "Grant rights";
                declineText.text = "Keep working slaves!";
                break;
            case 4:
                title.text = "We found new workers";
                dilemaText.text = "Our investors believe that we need more workers";
                acceptText.text = "Recruit 10 workers";
                declineText.text = "Don't do it";
                break;
            case 5:
                title.text = "Injuries";
                dilemaText.text = "Some workers have been injured during their shifts and now ask for a compensation";
                acceptText.text = "Give money";
                declineText.text = "Don't pay them";
                break;
        }
    }

    IEnumerator RandomEvent()
    {
        yield return new WaitForSeconds(Random.Range(30, 60));
        GenerateEvent();
        ShowEvent();
        StartCoroutine(RandomEvent());
    }

    IEnumerator MonthPayment()
    {
        yield return new WaitForSeconds(3600);
        money -= (numWorkers * workerCost);
    }
}
