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
        transform.RotateAroundLocal(Vector3.up, 1f * Time.deltaTime);
    }

	void OnTriggerEnter(Collider collision)
	{
        if(collision.gameObject.tag == "Player")
		{
            PlayerAudioManager.Instance.PlayCollectSound();
            QuestManager.Instance.CollectQuestItem(type);
            Destroy(gameObject);
        }
	}
}
