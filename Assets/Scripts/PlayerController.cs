using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public GameObject portal;
    public static bool CanPortal;
    private bool portaling;

    public float speed = 0.4f;
    Vector2 _dest = Vector2.zero;
    Vector2 _dir = Vector2.zero;
    Vector2 _nextDir = Vector2.zero;

    [Serializable]
    public class PointSprites
    {
        public GameObject[] pointSprites;
    }

    public PointSprites points;

    public static int killstreak = 0;

    // script handles
    private GameGUINavigation GUINav;
    private GameManager GM;
    private ScoreManager SM;

    private bool _deadPlaying = false;

    void Start()
    {
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        SM = GameObject.Find("Game Manager").GetComponent<ScoreManager>();
        GUINav = GameObject.Find("UI Manager").GetComponent<GameGUINavigation>();
        var destination = new Vector3(transform.position.x + 2f, transform.position.y);
        _dest = destination;

        var portalGameObject = (GameObject)Instantiate(portal, transform.position, Quaternion.identity);
        Destroy(portalGameObject, 1f);
        LeanTween.moveX(gameObject, transform.position.x + 2f, 0.5f);
    }

    private void Update() {
        if (Input.GetButtonDown("Portal") && CanPortal) {
            CanPortal = false;
            portaling = true;
            Instantiate(portal, transform.position, Quaternion.identity);
            LeanTween.scale(gameObject, new Vector3(0.2f, 0.2f), 0.4f).setOnComplete(() => {
                GameManager.Level++;
                SceneManager.LoadScene("game");
                portaling = false;
            });
        }
    }

    void FixedUpdate()
    {
        switch (GameManager.gameState)
        {
            case GameManager.GameState.Game:
                ReadInputAndMove();
                Animate();
                break;

            case GameManager.GameState.Dead:
                if (!_deadPlaying) {
                    StartCoroutine(PlayDeadAnimation());
                }
                break;
        }


    }

    private IEnumerator PlayDeadAnimation() {
        LeanTween.scale(gameObject, new Vector3(0.2f, 0.2f), 1f);
        LeanTween.rotateZ(gameObject, 180f, 1f);
        _deadPlaying = true;

        yield return new WaitForSeconds(1f);
        _deadPlaying = false;
        transform.localScale = new Vector3(1f, 1f);
        transform.rotation = Quaternion.identity;

        if (GameManager.lives <= 0)
        {
            Debug.Log("Treshold for High Score: " + SM.LowestHigh());
            //if (GameManager.score >= SM.LowestHigh())
            //    GUINav.getScoresMenu();
            //else
                GUINav.H_ShowGameOverScreen();
        }

        else
            GM.ResetScene();
    }

    void Animate()
    {
        Vector2 dir = _dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool Valid(Vector2 direction)
    {
        // cast line from 'next to pacman' to pacman
        // not from directly the center of next tile but just a little further from center of next tile
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.45f, direction.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.tag == "pacdot" || hit.collider == GetComponent<Collider2D>();
    }

    public void ResetDestination()
    {
        _dest = new Vector2(15f, 11f);
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void ReadInputAndMove() {

        if (portaling) return;

        // move closer to destination
        Vector2 p = Vector2.MoveTowards(transform.position, _dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // get the next direction from keyboard
        if (Input.GetAxis("Horizontal") > 0) _nextDir = Vector2.right;
        if (Input.GetAxis("Horizontal") < 0) _nextDir = -Vector2.right;
        if (Input.GetAxis("Vertical") > 0) _nextDir = Vector2.up;
        if (Input.GetAxis("Vertical") < 0) _nextDir = -Vector2.up;

        // if pacman is in the center of a tile
        if (Vector2.Distance(_dest, transform.position) < 0.00001f)
        {
            if (Valid(_nextDir))
            {
                _dest = (Vector2)transform.position + _nextDir;
                _dir = _nextDir;
            }
            else   // if next direction is not valid
            {
                if (Valid(_dir))  // and the prev. direction is valid
                    _dest = (Vector2)transform.position + _dir;   // continue on that direction

                // otherwise, do nothing
            }
        }
    }

    public Vector2 getDir()
    {
        return _dir;
    }

    public void UpdateScore()
    {
        killstreak++;

        // limit killstreak at 4
        if (killstreak > 4) killstreak = 4;

        Instantiate(points.pointSprites[killstreak - 1], transform.position, Quaternion.identity);
        GameManager.score += (int)Mathf.Pow(2, killstreak) * 100;

    }
}
