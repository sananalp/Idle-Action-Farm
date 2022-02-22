using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.BlockList.Count < player.BlockCountLimit)
            {
                var rigidbody = GetComponent<Rigidbody>();
                rigidbody.isKinematic = true;
                transform.parent = player.Bag;
                player.BlockList.Add(this);
                player.CollectBlocks();
            }
        }
    }
}
