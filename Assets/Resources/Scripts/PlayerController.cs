using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PhotonView pV;
    private Animator animator;

    private void Awake()
    {
        pV = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (pV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<PhotonView>().RPC("PlayAnimation", RpcTarget.AllBufferedViaServer);
            }
        }
    }
    
    [PunRPC]
    void PlayAnimation()
    {
        animator.Play("Jump");
    }
}
