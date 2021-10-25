using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBoneManager : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] List<Bone> bones;
    [SerializeField] public List<Bone> nonTouchingBones;
    [SerializeField] public List<Bone> touchingBones;

    [SerializeField] float touchPercent;
    float targetTouchPercent;
    float lerpSpeed=0.05f;
    public static CatBoneManager Instance = null;

    private void Update()
    {
        targetTouchPercent = Mathf.Lerp(targetTouchPercent, touchPercent, lerpSpeed);
        UIManager.Instance.UpdateFillBar(targetTouchPercent / 100);


    }

    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }


    public void AddTouchingBone(Bone b)
    {
        if (!touchingBones.Contains(b))
        {
            touchingBones.Add(b);
            touchPercent = (1- ((float)touchingBones.Count /(float) bones.Count)) *100;
           // UIManager.Instance.UpdateFillBar(touchPercent / 100);
        }
    }
    public void RemoveTouchingBone(Bone b)
    {
        if (touchingBones.Contains(b))
        {
            touchingBones.Remove(b);
            touchPercent = (1 - ((float)touchingBones.Count / (float)bones.Count)) * 100;
           // UIManager.Instance.UpdateFillBar(touchPercent / 100);
        }
        if (touchingBones.Count <= 0)
        {

            Invoke("Win", 1.5f);
        }
    }
    public void Win()
    {
        this.enabled = false;
        GameManager.Instance.WinLevel();
    }

    public void AddBone(Bone b)
    {
        bones.Add(b);
    }

}
