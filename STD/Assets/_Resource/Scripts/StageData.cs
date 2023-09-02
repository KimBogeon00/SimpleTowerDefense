using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class StageData : MonoBehaviour
{
    [SerializeField]
    public class Stage
    {
        public int stageMonsterCount;
        public int stageSpawnMonster;
        public int stageWeightI;
        public int stageWeightII;
        public int stageWeightIII;
    }
    [SerializeField]
    public class StageLoad
    {
        public Stage[] stage;
    }
    public StageLoad stage;
    // Start is called before the first frame update
    void Start()
    {
        TextAsset loadedJson = Resources.Load<TextAsset>("StageData");
        stage = JsonUtility.FromJson<StageLoad>(loadedJson.ToString());
        //a = JsonUtility.FromJson<int>(loadedJson.ToString());
        //Debug.Log(a);
        Debug.Log(loadedJson);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ts()
    {

        for (int i = 0; i < 2; i++)
        {
            Debug.Log(stage.stage[i].stageMonsterCount);

        }

    }

}
