using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour {
    public float tile_size;
    public TargetInfo[] tile_obj;
    public float[] tile_obj_per;
    public int seed;

    public List<GameObject> real_obj;
    //8방향
    public List<GameObject> near_tiles;
    //타일 내 도로
    public List<Roadinfo> roads;

    /// <summary>
    /// 타일이 도로를 포함하는가?
    /// </summary>
    public bool have_road;

    /// <summary>
    /// 오브젝트들이 젠이 되었는가?
    /// </summary>
    public bool hasGenObject;

    /// <summary>
    /// 타일의 생성이 끝났나?
    /// </summary>
    public bool hasGenerated;

    public TileUpdate tile;

    void Start() {
        Random.InitState(seed: (int)(Time.realtimeSinceStartup * 1000));
        seed = Random.Range(-1000, 1000);
        real_obj = new List<GameObject>();

        /*
         * 1. 해당 타일셋의 최대 크기를 알아낸 후
         * 2. 생성을 요하는 타겟의 사이즈를 구해,
         * 3. 2와 1을 나눠 최대 생성 가능 수량을 구한다.
         * 4. 3을 백분률로 하는 per 값을 사용하여 해당 타일에 몇개의 타일이 생성되어야 하는지 계산
         * 5. 4를 사용해 갯수만큼 생성 <- real_obj
         * 6. 위치는 seed 값을 사용해 해당 타일 내부에 랜덤한 위치에 생성.
         */
        if(!hasGenObject)
        {
            for (int i = 0; i < tile_obj.Length; i++)
            {
                float per = tile_obj_per[i];
                TargetInfo info = tile_obj[i];
                float max_target_num = tile_size / info.size;
                int real_gen_num = (int)((max_target_num * per) / 100);

                for (int num = 0; num < real_gen_num; num++)
                {
                    bool done = false;
                    while (!done)
                    {
                        GameObject tmp = Instantiate(info.gameObject, this.gameObject.transform);
                        Vector3 rand_pos = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                        Vector3 pos = rand_pos * tile_size;
                        pos.y = tmp.transform.localPosition.y;
                        tmp.transform.localPosition = pos;

                        real_obj.Add(tmp);
                        done = true;
                    }

                }
            }
            hasGenObject = true;
        }
        
        hasGenerated = true;
	}

	void Update ()
    {
	}
}
