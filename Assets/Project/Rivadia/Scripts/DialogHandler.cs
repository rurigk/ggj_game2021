
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DialogHandler : MonoBehaviour
{
    private Dialog newDia = new Dialog();

    private string m_Path;
    // Start is called before the first frame update
    void Start()
    {
        m_Path = Application.dataPath;

        Debug.Log(m_Path);


        Debug.Log("DialogHandler.Start");
        newDia.health = 80;

      
       string json=  JsonUtility.ToJson(newDia);

       Debug.Log(json);

       File.WriteAllText(Application.dataPath + "/saveFile.json" , json);


        string jsonReturn=File.ReadAllText(Application.dataPath + "/Project/Rivadia/Json/Prueba.json");

        //Dialog loadedDialog=  JsonUtility.FromJson<Dialog>(jsonReturn);
        Debug.Log("healt: "+ jsonReturn);
      

    }


}

public class Dialog
{
    public int health;
}
