using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ReflectableMovement
{
    Up,
    Down,
    Left,
    Right
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Transform movePoint;
    public Transform spawnPoint;
    public Transform goalPoint;
    public LayerMask obstacles;
    public LayerMask deadlyObstacles;
    public LevelController levelController;

    public Sprite deadSprite;
    private Sprite aliveSprite;

    private Stack<ReflectableMovement> moves = new Stack<ReflectableMovement>();

    private bool movementEnabled = true;
    private bool dead = false;

    private PlayerSoundController sound;

    // Start is called before the first frame update
    void Start()
    {
        // Move move point and spawn point outside player so they are not affected by player transform.
        movePoint.parent = null;
        spawnPoint.parent = null;
        sound = GetComponent<PlayerSoundController>();
        aliveSprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f && movementEnabled)
        {
            ReflectableMovement? move = null;

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                move = Input.GetAxisRaw("Horizontal") > 0 ? ReflectableMovement.Right : ReflectableMovement.Left;
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                move = Input.GetAxisRaw("Vertical") > 0 ? ReflectableMovement.Up : ReflectableMovement.Down;
            }

            if (move != null && MoveAllowed((ReflectableMovement) move))
            {
                DoMove((ReflectableMovement) move);
                moves.Push((ReflectableMovement) move);
            }

        }
    }

    private bool MoveAllowed(ReflectableMovement move)
    {
        if (Physics2D.OverlapCircle(GetNewMovePoint(move), .2f, obstacles)) return false;
        return true;
    }

    private Vector3 GetNewMovePoint (ReflectableMovement move)
    {
        switch (move)
        {
            case ReflectableMovement.Up:
                return movePoint.position + new Vector3(0f, 1f, 0f);

            case ReflectableMovement.Down:
                return movePoint.position + new Vector3(0f, -1f, 0f);

            case ReflectableMovement.Left:
                return movePoint.position + new Vector3(-1f, 0f, 0f);

            case ReflectableMovement.Right:
                return movePoint.position + new Vector3(1f, 0f, 0f);
            default: throw new System.Exception("Invalid move");
        }
    }

    private void DoMove (ReflectableMovement move)
    {
        Vector3 newPoint = GetNewMovePoint(move);
        if (MoveAllowed(move)) movePoint.position = newPoint;
        sound.PlayMoveSound();

        
        if (Physics2D.OverlapCircle(newPoint, .2f, deadlyObstacles))
        {
            Die();
        }
    }

    private ReflectableMovement ReflectedMove(ReflectableMovement move)
    {
        switch (move)
        {
            case ReflectableMovement.Up: return ReflectableMovement.Down;
            case ReflectableMovement.Down: return ReflectableMovement.Up;
            case ReflectableMovement.Left: return ReflectableMovement.Right;
            case ReflectableMovement.Right: return ReflectableMovement.Left;
            default: return ReflectableMovement.Left; // WHYYYY
        }
    }

    private IEnumerator RewindActions ()
    {
        yield return new WaitForSeconds(0.4f);
        while (moves.Count > 0)
        {
            if (dead) break;

            ReflectableMovement lastMove = moves.Pop();
            Debug.Log("Rewinding: " + lastMove);

            DoMove(ReflectedMove(lastMove));

            yield return new WaitForSeconds(0.2f);
        }

        if (goalPoint.transform.position == movePoint.position)
        {
            Debug.Log("YOU WIN");
        } else
        {
            Die();
        }
    }

    public void OnFlagCaptured ()
    {
        StartCoroutine(RewindActions());
        sound.PlayFlagGetSound();
        movementEnabled = false;
    }

    public void TeleportTo(Vector3 point)
    {
        sound.PlayTeleportSound();
        //Vector3 newPos = new Vector3(point.x, point.y, -1f);
        transform.position = point;
        movePoint.position = point;
    }

    public void Die ()
    {
        sound.PlayDeathSound();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = deadSprite;
        movementEnabled = false;
        levelController.OnDie();
        dead = true;
    }

    public void Revive ()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = aliveSprite;
        transform.position = spawnPoint.position;
        movePoint.position = spawnPoint.position;
        moves.Clear();
        movementEnabled = true;
        dead = false;
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Well hello there!!!!");
    //}
}
