using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float speed = 12.0f;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Transform bag;
    [SerializeField] private Transform scythe;
    public Transform Bag { get { return bag; } }
    private List<Block> blockList = new List<Block>();
    public List<Block> BlockList { get { return blockList; } }
    private int blockCountLimit = 40;
    public int BlockCountLimit { get { return blockCountLimit; } }
    [SerializeField] private UiView uiView;
    private Wheat foundWheat;

    private void OnEnable()
    {
        uiView.OnHitButtonClicked += HitWheat;
    }
    private void OnDisable()
    {
        uiView.OnHitButtonClicked -= HitWheat;
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        foundWheat = CheckWheat();
        
        Move();

        if (foundWheat)
        {
            uiView.HarvestButtonActivate();
        }
        else
        {
            uiView.HarvestButtonDeactivate();
        }

    }
    private void Move()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;
        
        if (x != 0 || z != 0)
        {
            float angle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
            Vector3 move = transform.forward * (Mathf.Abs(x) + Mathf.Abs(z));

            controller.Move(move * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
        }
        else
        {
            return;
        }
    }
    private Wheat CheckWheat()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.transform.TransformDirection(Vector3.forward), out hit, 2.0f))
        {
            if(hit.collider.TryGetComponent(out Wheat wheat))
            {
                return wheat;
            }
        }

        return null;
    }
    private void HitWheat()
    {
        foundWheat.LifeCount--;
        if (foundWheat.LifeCount == 0)
        {
            Harvest();
        }
    }
    private void Harvest()
    {
        var wheatObject = foundWheat.gameObject;
        var blockObject = foundWheat.BlockObject;
        var parent = wheatObject.transform.parent;
        blockObject.transform.localPosition = wheatObject.transform.localPosition;

        foundWheat.Crop();
        Instantiate(blockObject, parent);
    }
    public void CollectBlocks()
    {
        {
            for (int i = 0; i < blockList.Count; i++)
            {
                blockList[i].transform
                    .DOLocalMove(new Vector3(bag.localPosition.x, 0.1f * i, bag.localPosition.z), 0.5f);
                blockList[i].transform.localRotation = bag.localRotation;
            }
        }
    }
}