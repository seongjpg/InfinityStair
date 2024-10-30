using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Vector3 oldPosition;
    private bool isTurn = false;

    private int moveCnt = 0;
    private int turnCnt = 0;
    private int spawnCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        oldPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CharTurn();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CharMove();
        }
    }

    private void CharTurn()
    {
        isTurn = isTurn == true ? false : true;
        spriteRenderer.flipX = isTurn;
    }
    private void CharMove()
    {
        moveCnt++;
        MoveDirection();
        if (isFailTurn())//잘못된 방향 가면 사망 처리
        {
            anim.SetBool("Die", true);

        }
        if (moveCnt > 5)
        {
            RespawnStair();
            //계단 스폰 
        }
    }

    private void MoveDirection()
    {
		if (isTurn)
		{
			oldPosition += new Vector3(-0.85f, 0.5f, 0);
		}
		else
		{
			oldPosition += new Vector3(0.85f, 0.5f, 0);
		}

		transform.position = oldPosition;
		anim.SetTrigger("Move");
	}
    private bool isFailTurn()
    {
        bool result = false;
        if (GameManager.Instance.isTurn[turnCnt] != isTurn)
        {
            result = true;
        }
        turnCnt++;
        if (turnCnt > GameManager.Instance.Stairs.Length - 1) // 0 - 29 length
            turnCnt = 0;
        return result;
    }

    private void RespawnStair()
    {
        GameManager.Instance.SpawnStair(spawnCnt);
        spawnCnt++;
        if (spawnCnt > GameManager.Instance.Stairs.Length - 1)
        {
            spawnCnt = 0;
        }
    }
}

/*??????????????????????????????????????????????????*/
