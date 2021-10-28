using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RagdollMecanimMixer;
public class CatManager : MonoBehaviour
{
    [Header ("Attributes")]
    [SerializeField] float wakeThreshold;
    [SerializeField] float wakeValue;
    [SerializeField] float targetWakeValue;
    [SerializeField] float lerpSpeed;
    [SerializeField] CatState currentState;
    [SerializeField] CatState startState;


    [SerializeField] private RamecanMixer rmm;
    [SerializeField] GameObject sleepingVfx;
    [SerializeField] GameObject angryVfx;



    public static CatManager Instance = null;

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
        UpdateCatState(startState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != CatState.WalkSleep)
        {
            UpdateWakeValue();

            UIManager.Instance.UpdateSleepFillBar((wakeValue / wakeThreshold));
        }

    }
    public void UpdateWakeValue()
    {
       // wakeValue = 0;
        foreach (Bone b in CatBoneManager.Instance.bones)
        {
            if(b.GetComponent<Rigidbody>().velocity.magnitude > wakeThreshold)
            {
                targetWakeValue += b.GetComponent<Rigidbody>().velocity.magnitude;
            }
            targetWakeValue += b.GetComponent<Rigidbody>().velocity.magnitude;
            
        }

        targetWakeValue = targetWakeValue / CatBoneManager.Instance.bones.Count;
        if (targetWakeValue < wakeValue)
        {
            wakeValue = Mathf.Lerp(wakeValue, targetWakeValue, lerpSpeed / 3);
        }
        else
        {
            wakeValue = Mathf.Lerp(wakeValue, targetWakeValue, lerpSpeed);
        }

        if(wakeValue >= wakeThreshold && currentState != CatState.Irritated)
        {
            UpdateCatState(CatState.Irritated);
            GameManager.Instance.LoseLevel();
        }
        if(wakeValue < wakeThreshold * 3/4 && currentState == CatState.Irritated)
        {
           // UpdateCatState(CatState.Sleep);
        }
    }
 

    public void SwitchToRagdoll()
    {
        UpdateCatState(CatState.Sleep);
    }

    public void UpdateCatState(CatState state)
    {
        switch (state)
        {
            case CatState.WalkSleep:
                GetComponent<Animator>().enabled = true;
                rmm.BeginStateTransition("sleep");
                SoundManager.Instance.Play(Sound.Meow);
                break;

            case CatState.Sleep:
                rmm.BeginStateTransition("default");
                GetComponent<Animator>().enabled = false;
                SoundManager.Instance.Play(Sound.Purr);
                GameManager.Instance.SwitchToMainCam();
                sleepingVfx.SetActive(true);
                break;

            case CatState.Irritated:
                SoundManager.Instance.Play(Sound.Scream);
                sleepingVfx.SetActive(false);
                angryVfx.SetActive(true);
                //Show face anim
                break;

            case CatState.Awake:

                break;
        }
        currentState = state;
    }
}
