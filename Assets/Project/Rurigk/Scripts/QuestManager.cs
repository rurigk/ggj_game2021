using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    Dictionary<string, int> existingItems;
    Dictionary<string, int> collectedItems;

    public TextMeshProUGUI cherryCounter;
    public TextMeshProUGUI petaloCounter;

    public GameObject questProgressPanel;

    public UnityEvent eventoFinalizado;
    // Start is called before the first frame update
    void Start()
    {
		if (!Instance)
		{
            Instance = this;
        }

        existingItems = new Dictionary<string, int>();
        collectedItems = new Dictionary<string, int>();
    }

    // Update is called once per frame
    void Update()
    {
        int cherrys = 0;
        int cherrysCollected = 0;
        if (existingItems.ContainsKey("cherry"))
		{
            cherrys = existingItems["cherry"];
        }

        if (collectedItems.ContainsKey("cherry"))
        {
            cherrysCollected = collectedItems["cherry"];
        }
        cherryCounter.text = cherrysCollected.ToString() + "/" + cherrys.ToString();

        int petalos = 0;
        int petalosCollected = 0;
        if (existingItems.ContainsKey("petalo"))
        {
            petalos = existingItems["petalo"];
        }

        if (collectedItems.ContainsKey("petalo"))
        {
            petalosCollected = collectedItems["petalo"];
        }
        petaloCounter.text = petalosCollected.ToString() + "/" + petalos.ToString();

    }

    public void RegisterQuestItem(string type)
	{
        if(existingItems.ContainsKey(type))
		{
            existingItems[type] += 1;
		}
		else
		{
            existingItems.Add(type, 1);
		}
        
	}

    public void CollectQuestItem(string type)
    {
        if (collectedItems.ContainsKey(type))
        {
            collectedItems[type] += 1;
        }
        else
        {
            collectedItems.Add(type, 1);
        }
    }

    public void StartQuest()
	{
        questProgressPanel.SetActive(true);
    }

    public void EndQuest()
    {
        questProgressPanel.SetActive(false);
    }


    public void QuestComplete()
    {
        QuestIsComplete();
        if (QuestIsComplete())
        {
            eventoFinalizado.Invoke();
        }
        else
        {
            
            Debug.Log("Aun no completas las mision");
        }
    }

    public bool QuestIsComplete()
	{
        foreach (KeyValuePair<string, int> entry in existingItems)
        {
            if(!collectedItems.ContainsKey(entry.Key))
			{
                return false;
			}
            else if(collectedItems[entry.Key] != entry.Value)
			{
                return false;
            }
        }
        return true;
	}
}
