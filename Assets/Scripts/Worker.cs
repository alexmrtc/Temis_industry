using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Worker : MonoBehaviour
{
    [System.Serializable]
    public struct WorkerInfo
    {
        public string name;
        public int age;

        public string workingSkill;
        public string workingAt;

        public bool working;
        public bool hasHome;

        public int timeInTheCompany;
        public int happiness;
    }
    //public int numWorkers = 10;

    [SerializeField]
    public WorkerInfo workerInfo;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //generateCharacter();
    }

    public void assignName(int random)
    {
        switch (random)
        {
            case 0:
                workerInfo.name = "Johny";
                break;
            case 1:
                workerInfo.name = "Bravo";
                break;
            case 2:
                workerInfo.name = "Was";
                break;
            case 3:
                workerInfo.name = "Here";
                break;
            case 4:
                workerInfo.name = "Baby";
                break;
        } 
    }

    public void assignSkill(int random)
    {
        switch (random)
        {
            case 0:
                workerInfo.workingSkill = "Developer";
                break;
            case 1:
                workerInfo.workingSkill = "Artist";
                break;
            case 2:
                workerInfo.workingSkill = "Logistics";
                break;
            case 3:
                workerInfo.workingSkill = "Leaderships";
                break;
            case 4:
                workerInfo.workingSkill = "Worker";
                break;
        }
    }

    public void generateCharacter()
    {
        //workerInfo = new WorkerInfo();

        int randName = Random.Range(0, 4);
        int randAge = Random.Range(20, 80);
        int randHappiness = Random.Range(0, 100);
        int randSkill = Random.Range(0, 4);

        assignName(randName);
        workerInfo.age = randAge;
        assignSkill(randName);
        workerInfo.happiness = randHappiness;
        


        //Debug.Log(workerInfo.name+ " "+ workerInfo.age+" "+ workerInfo.workingSkill);
    }

    public string GetWorkerName() { return workerInfo.name; }
    public string GetWorkerSkill() { return workerInfo.workingSkill; }
}
