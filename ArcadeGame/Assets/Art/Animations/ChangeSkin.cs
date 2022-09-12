using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    public AnimatorOverrideController UFOAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UFO()
    {
        GetComponent<Animator>().runtimeAnimatorController = UFOAnim as RuntimeAnimatorController;
    }
}
