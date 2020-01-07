using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListWorkers : MonoBehaviour
{
    public WorkerPrefab workerPrefab;

    [SerializeField]
    List<Worker> workersList;
    [SerializeField]
    List<WorkerPrefab> workerPrefabsList;

    public GameObject prefab;

    private void Awake()
    {
        workersList = new List<Worker>();
        workerPrefabsList = new List<WorkerPrefab>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            StartCoroutine(AddWorker());
        }
        StartCoroutine(List());
        gameObject.GetComponent<WorkerPrefab>().assignInfo(0, workersList[0].workerInfo.name, workersList[0].workerInfo.workingSkill, workersList[0].workerInfo.happiness, workersList[0].workerInfo.age);
        //StartCoroutine(AddWorker());
        //StartCoroutine(List());
        //StartCoroutine(AddWorker());
        //StartCoroutine(List());

        //workerPrefab.setName(workersList[0].workerInfo.name);
        for (int i = 0; i < workersList.Count(); i++)
        {
            StartCoroutine(AddWorkerPrefab(i, new Vector3(100, 200, 100)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Workers: " + workersList.Count());
    } 

    IEnumerator AddWorker()
    {
        Worker worker = new Worker();
        InitWorker(worker);
        //Debug.Log("NAme: "+ worker.GetWorkerName());

        workersList.Add(worker);
        yield return new WaitForSeconds(3);
        //AddWorker();
    }

    IEnumerator AddWorkerPrefab(int i, Vector3 offset)
    {
        WorkerPrefab workerPrefab = new WorkerPrefab();

        workerPrefab.assignInfo(i, workersList[i].workerInfo.name, workersList[i].workerInfo.workingSkill, workersList[i].workerInfo.happiness, workersList[i].workerInfo.age);
        workerPrefabsList.Add(workerPrefab);

        Instantiate(prefab, offset, Quaternion.identity);
        yield return new WaitForSeconds(1);
    }

    public void InitWorker(Worker worker)
    {
        worker.generateCharacter();
    }

    IEnumerator List()
    {
        for (int i= 0; i < workersList.Count(); i++)
        {
            Debug.Log("Name: " + workersList[i].workerInfo.name + " Skill: " + workersList[i].workerInfo.workingSkill+ " Happiness: "+ workersList[i].workerInfo.happiness+ " Age: "+ workersList[i].workerInfo.age);
        }
        yield return new WaitForSeconds(3);
        List();
    }
}
