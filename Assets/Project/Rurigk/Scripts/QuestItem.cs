using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.RegisterQuestItem(type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
