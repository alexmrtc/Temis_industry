using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : GameInfo
{
    // Start is called before the first frame update
    void Start()
    {
        money = startingMoney;
        StartCoroutine(GetMoney());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    IEnumerator GetMoney()
    {
        money += ((earningsPerWorker*workersHappiness) * numWorkers) - (workerCost * numWorkers);
        Debug.Log("Money: " + money);
        yield return new WaitForSeconds(4);
        StartCoroutine(GetMoney());
    }
}
