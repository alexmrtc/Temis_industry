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

    public AudioSource backgroundMusic;
    public AudioClip day;
    public AudioClip night;

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

        money = startingMoney;
        moneyText.text = money.ToString() + "$";

        InitTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        workerSlider.value = workersHappiness;
        companySlider.value = companyHappiness;
        investorSlider.value = investorsHappiness;

        moneyText.text = money.ToString() + "$";
        numWorkersText.text = numWorkers.ToString();
    }

    public void InitTutorial()
    {
        StartCoroutine(RandomEvent());

        StartCoroutine(MonthPayment());
    }

    public void GetMoneyFunction()
    {
        money += ((earningsPerWorker * workersHappiness) * numWorkers) + incomePerDay;
        Debug.Log("Money: " + money);
        //moneyText.text = money.ToString() + "$";
        incomePerDay = 0;
    }

    public void ChangeShiftEnd()
    {
        FindObjectOfType<DayCycle>().shiftEnd = shiftEndDropdown.value;

        if (shiftEndDropdown.value <= FindObjectOfType<DayCycle>().shiftStart)
        {
            FindObjectOfType<DayCycle>().shiftStart = FindObjectOfType<DayCycle>().shiftEnd - 1;

            if (FindObjectOfType<DayCycle>().shiftStart < 0)
            {
                shiftStart = 23;
            }

            shiftStartDropdown.value = FindObjectOfType<DayCycle>().shiftStart;
        }

        if (shift8hours == true)
        {
            if (FindObjectOfType<DayCycle>().shiftEnd - FindObjectOfType<DayCycle>().shiftStart > 8)
            {
                FindObjectOfType<DayCycle>().shiftStart = FindObjectOfType<DayCycle>().shiftEnd;
                for (int i = 0; i < 8; i++)
                {
                    FindObjectOfType<DayCycle>().shiftStart--;
                    if (FindObjectOfType<DayCycle>().shiftStart < 0)
                    {
                        FindObjectOfType<DayCycle>().shiftEnd = 23;
                    }
                }
            }
        }
        Debug.Log("ShiftEnds at: " + FindObjectOfType<DayCycle>().shiftEnd);
    }

    public void ChangeShiftStart()
    {
        FindObjectOfType<DayCycle>().shiftStart = shiftStartDropdown.value;

        if (shiftStartDropdown.value >= FindObjectOfType<DayCycle>().shiftEnd)
        {
            FindObjectOfType<DayCycle>().shiftEnd = FindObjectOfType<DayCycle>().shiftStart + 1;

            if (FindObjectOfType<DayCycle>().shiftEnd > 23)
            {
                FindObjectOfType<DayCycle>().shiftEnd = 0;
            }

            shiftEndDropdown.value = FindObjectOfType<DayCycle>().shiftEnd;
        }

        if (shift8hours == true)
        {
            if (FindObjectOfType<DayCycle>().shiftEnd - FindObjectOfType<DayCycle>().shiftStart > 8)
            {
                FindObjectOfType<DayCycle>().shiftEnd = FindObjectOfType<DayCycle>().shiftStart;
                for (int i  = 0; i < 8; i++)
                {
                    FindObjectOfType<DayCycle>().shiftEnd++;
                    if (FindObjectOfType<DayCycle>().shiftEnd > 23)
                    {
                        FindObjectOfType<DayCycle>().shiftEnd = 0;
                    }
                }
            }
        }


        Debug.Log("ShiftStarts at: " + FindObjectOfType<DayCycle>().shiftStart);
    }

    public void CheckIfCanAfford()
    {
        if (money - houseConstructionPrice > 0)
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
        money -= houseConstructionPrice;
        housesForWorkers++;
        
        Debug.Log(money);
        HideEvent();
    }

    public void RecruitWorkers(int _workers)
    {
        numWorkers += _workers;
    }

    public void PayInjuries()
    {
        int numWorkersInjured = Random.Range(1, 5);

        money -= injuries * numWorkersInjured;
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

    public void HideEvent()
    {
        image.SetActive(false);
    }

    public void ShowWorkerPanel()
    {
        workerPanel.SetActive(true);
    }

    public void HideWorkerPanel()
    {
        workerPanel.SetActive(false);
    }

    public void AcceptEvent()
    {
        switch (eventCase)
        {
            case 1:
                CheckIfCanAfford();
                HideEvent();
                break;
            case 2:
                IncreaseWorkerHappiness(0.1f);
                ReduceInvestorsHappiness(0.1f);
                StartCoroutine(ResearchMaterial());
                HideEvent();
                break;
            case 3:
                IncreaseWorkerHappiness(0.2f);
                shift8hours = true;
                ChangeShiftStart();
                HideEvent();
                break;
            case 4:
                IncreaseInvestorsHappiness(0.1f);
                RecruitWorkers(10);
                HideEvent();
                break;
            case 5:
                IncreaseWorkerHappiness(0.1f);
                ReduceCompanyHappiness(0.1f);
                PayInjuries();
                HideEvent();
                break;
        }
    }

    public void DeclineEvent()
    {
        switch (eventCase)
        {
            case 1:
                ReduceWorkerHappiness(0.1f);
                HideEvent();
                break;
            case 2:
                ReduceCompanyHappiness(0.1f);
                HideEvent();
                break;
            case 3:
                ReduceWorkerHappiness(0.1f);
                HideEvent();
                break;
            case 4:
                ReduceInvestorsHappiness(0.1f);
                HideEvent();
                break;
            case 5:
                ReduceWorkerHappiness(0.2f);
                HideEvent();
                break;
        }
    }

    public void GenerateEvent()
    {
        eventCase = Random.Range(1, 5);
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
        yield return new WaitForSeconds(300);
        money -= ((numWorkers * workerCost) + (piecesBuilt * costOfProductionPerPieceBuilt) + (housesForWorkers * houseMaintainancePrice));
        StartCoroutine(MonthPayment());
        Debug.Log("Money after payment: " + money);
    }

    IEnumerator ResearchMaterial()
    {
        money -= 1000;
        yield return new WaitForSeconds(Random.Range(10, 20));
        int random = Random.Range(1, 10);

        if (random > 7)
        {
            costOfProductionPerPieceBuilt -= 50;
        }
        else
        {
            costOfProductionPerPieceBuilt += 50;
        }
    }

    IEnumerator LookForWorkers()
    {
        yield return new WaitForSeconds(15);
        numWorkers += Random.Range(5, 10);
    }
}