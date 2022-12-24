using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalogController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController playerController;

    void Start()
    {

#if UNITY_ANDROID
       playerController = FindObjectOfType<PlayerController>(); 
#else
        Destroy(gameObject);
#endif
    }

    public void _OnButtonMove(int move)
    {
        playerController._OnButtonMove(move);
        Debug.Log(move);
    }

    public void _OnButtonStop()
    {
        playerController._OnStopMove();
    }
    public void _OnButtonJump()
    {
        playerController.Jump();
    }
}
