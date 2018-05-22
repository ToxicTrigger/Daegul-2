using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DynamicPreset
{
    public int lev, amount;
    public int item_per, ani_per;
    public DynamicPreset(int Lev, int IPer, int APer, int Amount)
    {
        lev = Lev;
        amount = Amount;
        item_per = IPer;
        ani_per = APer;
    }
}

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public List<Preset> spawner_preset, lev_preset;
    public List<DynamicPreset> spawner_info;
    public string spawner_info_name = "data_dynamic_spawner";
    public string preset_name = "data_tmp_spawn";
    public int Level = 1;
    public int max_amount = 70;
    public DynamicPreset cur_spawer_info;
    public Move pig;
    public bool is_load_done;
    public UIManager ui;

    public List<GameObject> spawners;
    public int spawner_num;
    public GameObject Spawner;
    public SphereCollider sc;
    public float rad;

    public float play_time;
    public float max_play_time;
    public bool game_over = true;


    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
        Debug.Log("Loading GameManager");
        //DontDestroyOnLoad(gameObject);

            InitGame();
        
        
    }

    void update_spawner()
    {
        while ( spawners.Count < max_amount )
        {
            Vector3 pos = pig.gameObject.transform.position;
            float x, z;
            x = UnityEngine.Random.Range(-rad, rad);
            z = UnityEngine.Random.Range(-rad, rad);
            pos.x += x;
            pos.z += z;
            RaycastHit hit;
            if(Physics.SphereCast(pos,10,Vector3.down,out hit, 100))
            {
                if(hit.collider.gameObject.tag.Equals("Obstacle"))
                {
                    GameObject tmp = Instantiate(Spawner, pos, Quaternion.identity, null);
                    spawners.Add(tmp);
                }
                else
                {
                    continue;
                }
            }
        }
    }

    void delete_spawner()
    {
        IEnumerator iter = spawners.GetEnumerator();
        while (iter.MoveNext())
        {
            GameObject tmp = iter.Current as GameObject;
            if(tmp.GetComponent<Spawner>().live == false)
            {
                spawners.Remove(tmp);
            }
        }
    }

    //Initializes the game for each level.
    void InitGame()
    {


        try
        {
            pig = GameObject.Find("pig").GetComponent<Move>();
            spawner_preset = new List<Preset>();
            spawner_info = new List<DynamicPreset>();
            spawners = new List<GameObject>();

            Debug.Log("Load_" + preset_name);
            ReadCSV("preset/" + preset_name);
            Debug.Log("Done_" + preset_name);
            Debug.Log("Load_" + spawner_info_name);
            ReadCSV("preset/" + spawner_info_name);
            Debug.Log("Done_" + spawner_info_name);


            rad = sc.radius;

            Level = pig.Level;
            lev_preset = get_level(pig.Level);

            cur_spawer_info = get_spawner_info();
            max_amount = cur_spawer_info.amount;
            ui = GetComponent<UIManager>();

            update_spawner();
            is_load_done = true;
            is_done = false;
        }
        catch (Exception e)
        {

        }


    }

    List<Preset> get_level(int lev)
    {
        List<Preset> tmp = new List<Preset>();

        IEnumerator iter = spawner_preset.GetEnumerator();
        while(iter.MoveNext())
        {
            Preset p = iter.Current as Preset;
            if (p.lev == lev) tmp.Add(p);
        }
        return tmp;
    }

    DynamicPreset get_spawner_info()
    {
        IEnumerator iter = spawner_info.GetEnumerator();
        while(iter.MoveNext())
        {
            DynamicPreset dp = iter.Current as DynamicPreset;
            if (dp.lev == Level) return dp;
        }
        return null;
    }

    public bool is_done;
    AsyncOperation ao;
    public void Change_Scene()
    {
        if(game_over)
        {
            if(!is_done)
            {
                SceneManager.LoadScene(0);
                SceneManager.UnloadSceneAsync(1);
                is_done = true;
            }
        }else if(is_done)
        {
            ao = SceneManager.LoadSceneAsync(1);
            SceneManager.UnloadSceneAsync(0);
            //InitGame();
            is_done = false;
        }
    }

    private void Update()
    {
        Change_Scene();
        if(!game_over)
        {
            if (play_time <= max_play_time)
            {
                if(!ui.stop)
                play_time += Time.deltaTime;
            }
            else
            {
                game_over = true;
            }

            if (pig.Level != Level)
            {
                lev_preset = get_level(pig.Level);
                Level = pig.Level;
                cur_spawer_info = get_spawner_info();
                max_amount = cur_spawer_info.amount;
            }
            spawner_num = spawners.Count;
            delete_spawner();
        }
    }

    void ReadCSV(string name)
    {
        List<Dictionary<string, object>> data = CSVReader.Read(name);
        if(name.Contains("spawner"))
        {
            for (var i = 0; i < data.Count; i++)
            {
                //Debug.Log("index " + (i).ToString() + " : " + data[i]["Lv"] + " " + data[i]["Item"] + " " + data[i]["Animal"] + " " + data[i]["Amount"]);
                spawner_info.Add(new DynamicPreset(
                    (int)data[i]["Lv"],
                    (int)data[i]["Item"],
                    (int)data[i]["Animal"],
                    (int)data[i]["Amount"])
               );
            }
        }
        else
        {
            for (var i = 0; i < data.Count; i++)
            {
                //Debug.Log("index " + (i).ToString() + " : " + data[i]["lev"] + " " + data[i]["name"] + " " + data[i]["per"]);
                spawner_preset.Add(new Preset(
                    (int)data[i]["lev"],
                    (string)data[i]["name"],
                    (float)data[i]["per"])
               );
            }
        }
    }
}
