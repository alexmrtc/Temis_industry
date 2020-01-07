using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorkerPrefab : MonoBehaviour
{
    public int id;

    public Text workerName;
    public Text workerHappiness;

    public Text workingPlace;
    public Text workingSite;

    public Text workerNameDetail;
    public Text workerAgeDetail;

    public Text workerSkill;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    public void assignInfo(int _id, string _name, string _skill, int _happiness, int _age)
    {
        id = _id;
        workerName.text = _name;
        workerSkill.text = _skill;
        workerHappiness.text = _happiness.ToString();
        workerAgeDetail.text = _age.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        workingPlace.text = workingSite.text;
    }

    public void setName(string _name)
    {
        workerName.text = _name;
    }

    public void setSkill(string _skill)
    {
        workerSkill.text = _skill;
    }

    public void setHappy(int _happiness)
    {
        workerHappiness.text = _happiness.ToString();
    }   

    public void DebugHappiness()
    {
        Debug.Log(Random.Range(0, 100));
    }

    public void sendInfo()
    {
        workerNameDetail.text = workerName.text;
    }
}
