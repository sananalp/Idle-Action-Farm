using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    private Transform wheatTransform;
    private Collider wheatCollider;
    private int lifeCount = 3;
    [SerializeField] private GameObject blockObject;
    public int LifeCount { get { return lifeCount; } set { lifeCount = value; } }
    public GameObject BlockObject { get { return blockObject; } }

    private void Start()
    {
        wheatTransform = GetComponent<Transform>();
        wheatCollider = GetComponent<Collider>();
    }
    private void Update()
    {
        
    }
    public void Crop()
    {
        var harvestedWheatPosition = new Vector3(wheatTransform.position.x, 0.1f, wheatTransform.position.z);
        var harvestedWheatScale = new Vector3(wheatTransform.localScale.x, 0, wheatTransform.localScale.z);
        wheatTransform.position = harvestedWheatPosition;
        wheatTransform.localScale = harvestedWheatScale;
        wheatCollider.enabled = false;
        lifeCount = 3;
        StartCoroutine(Growth());
    }
    private void Grow()
    {
        var wheatScale = wheatTransform.localScale;
        wheatScale = new Vector3(wheatScale.x, wheatScale.y + 1, wheatScale.z);
        wheatTransform.localScale = wheatScale;
    }
    IEnumerator Growth()
    {
        float growTime = 1.0f;
        while (growTime <= 10.0f)
        {
            yield return new WaitForSeconds(1.0f);
            Grow();
            growTime++;
        }
        wheatCollider.enabled = true;
    }
}