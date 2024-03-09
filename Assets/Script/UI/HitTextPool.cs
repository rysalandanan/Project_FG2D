using System.Collections.Generic;
using UnityEngine;

public class HitTextPool : MonoBehaviour
{
    public static HitTextPool Instance;
    private List<GameObject> hitTextObjects = new List<GameObject>();
    [SerializeField] private int poolCapacity;

    [SerializeField] private GameObject hitTextPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < poolCapacity; i++)
        {
            GameObject obj = Instantiate(hitTextPrefab, this.transform);
            obj.SetActive(false);
            hitTextObjects.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < hitTextObjects.Count; i++)
        {
            if (!hitTextObjects[i].activeInHierarchy)
            {
                return hitTextObjects[i];
            }
        }
        return null;
    }
    public void DeactivateAllHitText()
    {
        for (int i = 0; i < hitTextObjects.Count; i++)
        {
            hitTextObjects[i].SetActive(false);
        }
    }
}
