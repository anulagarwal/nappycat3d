using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBoneManager : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] List<Bone> bones;
    [SerializeField]public List<Bone> nonTouchingBones;
    [SerializeField] public List<Bone> touchingBones;


    public static CatBoneManager Instance = null;


    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AddNonTouchingBone(Bone b)
    {
        if (!nonTouchingBones.Contains(b))
        {
            nonTouchingBones.Add(b);
        }
    }
    public void RemoveNonTouchingBone(Bone b)
    {
        if (nonTouchingBones.Contains(b))
        {
            nonTouchingBones.Remove(b);
        }
    }
    public void AddTouchingBone(Bone b)
    {
        if (!touchingBones.Contains(b))
        {
            touchingBones.Add(b);
        }
    }
    public void RemoveTouchingBone(Bone b)
    {
        if (touchingBones.Contains(b))
        {
            touchingBones.Remove(b);

        }
        if (touchingBones.Count <= 0)
        {
            GameManager.Instance.WinLevel();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddBone(Bone b)
    {
        bones.Add(b);
    }
    public void UpdateBoneCounter()
    {
        if(nonTouchingBones.Count > bones.Count - 2)
        {
            GameManager.Instance.WinLevel();
        }
        
    }
}
