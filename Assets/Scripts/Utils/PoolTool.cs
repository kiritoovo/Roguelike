using UnityEngine;
using UnityEngine.Pool;

public class PoolTool : MonoBehaviour
{
    public GameObject objPrefab;
    private ObjectPool<GameObject> pool;

    private void Start(){
        pool=new ObjectPool<GameObject>(
            createFunc:()=>Instantiate(objPrefab,transform),
            actionOnGet:(obj)=>obj.SetActive(true),
            actionOnRelease:(obj)=>obj.SetActive(false),
            actionOnDestroy:(obj)=>Destroy(obj),
            collectionCheck:false,
            defaultCapacity:10,
            maxSize:20
        );
        PreFillPool(8);
    }

    private void PreFillPool(int count){
        var preFillPoo=new GameObject[count];
        for(int i=0;i<count;i++)
        {
            preFillPoo[i]=pool.Get();
        }
        foreach(var p in preFillPoo){
            pool.Release(p);
        }
    }

    public GameObject GetPoolObj()
    {
        return pool.Get();
    }

    public void ReleasePoolObj(GameObject gameObject)
    {
        pool.Release(gameObject);
    }
}
