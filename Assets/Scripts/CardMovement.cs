﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour , IBeginDragHandler, IEndDragHandler , IDragHandler, IPointerEnterHandler , IPointerExitHandler
{
    private Camera mainCamera;

    private Vector3 CardPosition;
    private Vector3 offset;
    public Transform DefaultParent;

    public bool CardPlayed = false;

    public void Awake()
    {
        mainCamera = Camera.allCameras[0];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CardPlayed) return;
        offset = transform.position - mainCamera.ScreenToWorldPoint(eventData.position);
        DefaultParent = transform.parent;
        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CardPlayed) return;
        CardPosition = mainCamera.ScreenToWorldPoint(eventData.position) + offset;
        CardPosition.z = 0;
        Debug.Log(CardPosition);
        transform.position = CardPosition; //CardPosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CardPlayed) return;
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (DefaultParent.gameObject.name == "PlayedCards") CardPlayed = true;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("In", true);
        anim.SetBool("Out", false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("In", false);
        anim.SetBool("Out", true);
    }

    public void GoToVoid()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Void", true);
        Destroy(gameObject, 0.95f);
    }
}