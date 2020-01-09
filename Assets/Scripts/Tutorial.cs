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

    private int maxFire = 0;
    private int maxHire = 0;

    #region Workers Panel
    public GameObject workerPanel;
    public Text numWorkersText;

    public Dropdown shiftStartDropdown;
    public Dropdown shiftEndDropdown;

    public GameObject hirePanel;
    public Text hireXworkers;
    public Text hireQuestion;

    public GameObject firePanel;
    public Text fireXworkers;
    public Text fireQuestion;
    #endregion

    public GameObject companyPanel;
    public Text totalPieceText;
    public Text piecePerWorkerText;
    public Text piecePriceText;
    public Text maintainanceText;
    public Text paycheck;


    public GameObject investorsPanel;
    public Text housePriceText;
    public Text productionCostText;
    public Text totalProductionCostText;

    public GameObject win;
    public GameObject lose;
    public GameObject menu;

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

    public AudioSource cheer;
    public AudioSource boo;
    public AudioSource buttonClick;
    public AudioSource gameOver;
    public AudioSource winSound;
    public AudioSource construction;

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

        totalPieceText.text = (piecesBuiltPerHourByWorker * numWorkers * (shiftEnd - shiftStart)).ToString();
        piecePerWorkerText.text = piecesBuiltPerHourByWorker.ToString();
        piecePriceText.text = priceOfSalePerPieceBuilt.ToString();
        maintainanceText.text = ((numWorkers * workerCost) + (piecesBuilt * costOfProductionPerPieceBuilt) + (housesForWorkers * houseMaintainancePrice)).ToString();
        paycheck.text = workerCost.ToString();

        housePriceText.text = houseConstructionPrice.ToString();
        productionCostText.text = costOfProductionPerPieceBuilt.ToString();
        totalProductionCostText.text = (((piecesBuiltPerHourByWorker * numWorkers) * costOfProductionPerPieceBuilt) * (shiftEnd - shiftStart)).ToString();

        if (workersHappiness == 0)
        {
            GameOver();
        }

        if (companyHappiness == 0)
        {
            GameOver();
        }

        if (investorsHappiness == 0)
        {
            GameOver();
        }

        if (money == 0)
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Win();
        }

        if (daysSurvived == 30)
        {
            Win();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
    }

    public void InitTutorial()
    {
        StartCoroutine(RandomEvent());

        StartCoroutine(Payment());
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
                for (int i = 0; i < 8; i++)
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
        if (money - houseConstructionPrice >= 0)
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
        if (housesForWorkers + 1 <= houses.Length)
        {
            houses[housesForWorkers].SetActive(true);
            IncreaseWorkerHappiness(0.1f);
            cheer.Play();
            money -= houseConstructionPrice;
            housesForWorkers++;
            construction.Play();
        }

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

    #region Show and Hide UI
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

    public void ShowHirePeople()
    {
        hirePanel.SetActive(true);
        LookForWorkers();
    }

    public void HideHirePeople()
    {
        hirePanel.SetActive(false);
    }

    public void ShowFirePeople()
    {
        firePanel.SetActive(true);
        FireWorkers();
    }

    public void HideFirePeople()
    {
        firePanel.SetActive(false);
    }

    public void ShowCompanyPanel()
    {
        companyPanel.SetActive(true);
    }

    public void HideCompanyPanel()
    {
        companyPanel.SetActive(false);
    }

    public void ShowInvestorPanel()
    {
        investorsPanel.SetActive(true);
    }

    public void HideInvestorPanel()
    {
        investorsPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    #endregion

    #region Hire/Fire
    public void LookForWorkers()
    {
        workersHire = Random.Range(5, 10);
        maxHire = workersHire;
        UpdateFireHireTexts();
    }

    public void FireWorkers()
    {
        workersFire = numWorkers;
        UpdateFireHireTexts();
    }

    public void IncreaseFire()
    {
        if (workersFire + 1 <= numWorkers)
        {
            workersFire++;
        }

        UpdateFireHireTexts();
    }

    public void IncreaseHire()
    {
        if (workersHire + 1 <= maxHire)
        {
            workersHire++;
        }

        UpdateFireHireTexts();
    }

    public void DecreaseFire()
    {
        if (workersFire - 1 >= 0)
        {
            workersFire--;
        }

        UpdateFireHireTexts();
    }

    public void DecreaseHire()
    {
        if (workersHire - 1 >= 0)
        {
            workersHire--;
        }

        UpdateFireHireTexts();
    }

    void UpdateFireHireTexts()
    {
        hireQuestion.text = "We have " + workersHire + " applicants, how many you want to recruit?";
        hireXworkers.text = workersHire.ToString();

        fireQuestion.text = "We have " + workersFire + " workers, how many you want to fire?";
        fireXworkers.text = workersFire.ToString();
    }

    public void Hire()
    {
        RecruitWorkers(workersHire);
        HideHirePeople();
    }

    public void Fire()
    {
        numWorkers -= workersFire;
        HideFirePeople();
    }
    #endregion

    public void playClickSound()
    {
        buttonClick.Play();
    }

    public void AcceptEvent()
    {
        switch (eventCase)
        {
            case 1:
                CheckIfCanAfford();
                break;
            case 2:
                IncreaseWorkerHappiness(0.1f);
                cheer.Play();
                ReduceInvestorsHappiness(0.1f);
                StartCoroutine(ResearchMaterial());
                HideEvent();
                break;
            case 3:
                IncreaseWorkerHappiness(0.1f);
                cheer.Play();
                ReduceCompanyHappiness(0.1f);
                money += 5000;
                incomePerDay -= piecesBuiltPerHourByWorker * 2;
                HideEvent();
                break;
            case 4:
                IncreaseInvestorsHappiness(0.1f);
                RecruitWorkers(10);
                HideEvent();
                break;
            case 5:
                IncreaseWorkerHappiness(0.1f);
                cheer.Play();
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
                boo.Play();
                HideEvent();
                break;
            case 2:
                ReduceCompanyHappiness(0.1f);
                HideEvent();
                break;
            case 3:
                ReduceWorkerHappiness(0.1f);
                boo.Play();
                HideEvent();
                break;
            case 4:
                ReduceInvestorsHappiness(0.1f);
                HideEvent();
                break;
            case 5:
                ReduceWorkerHappiness(0.2f);
                boo.Play();
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
                title.text = "School trip";
                dilemaText.text = "A school wants to visit the factory, they will pay but some workers will have to guide";
                acceptText.text = "Grant visit";
                declineText.text = "Another time";
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
        FindObjectOfType<Camera>().freezeMouseCamera = true;
    }

    IEnumerator Payment()
    {
        yield return new WaitForSeconds(120);
        money -= ((numWorkers * workerCost) + (piecesBuilt * costOfProductionPerPieceBuilt) + (housesForWorkers * houseMaintainancePrice));
        StartCoroutine(Payment());
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

    public void GameOver()
    {
        lose.SetActive(true);
        gameOver.Play();
    }

    public void Win()
    {
        win.SetActive(true);
        winSound.Play();
    }
}