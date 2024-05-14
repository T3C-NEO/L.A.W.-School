using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class SpellsLogic : MonoBehaviour
{

    //losing and pausing
    bool lose = false;
    bool pause = false;

    //scoring
    int score = 0;
    int highScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    //creating the timer
    [SerializeField] float remainingTime;
    [SerializeField] Slider timer;

    //the text that will display the spell name
    public TextMeshProUGUI spellName;

    //gathering the arrows
    public List<MeshRenderer> arrows = new List<MeshRenderer>();
    public MeshRenderer arrow0;
    public MeshRenderer arrow1;
    public MeshRenderer arrow2;
    public MeshRenderer arrow3;
    public MeshRenderer arrow4;
    public MeshRenderer arrow5;

    //creating rotations for arrows
    Quaternion up = Quaternion.Euler(-90, 0, 90);
    Quaternion down = Quaternion.Euler(90, 90, 0);
    Quaternion left = Quaternion.Euler(-180, 90, 0);
    Quaternion right = Quaternion.Euler(0, 90, 0);

    //the variable that sees how far in the spell you are
    int place = 0;

    int max;

    //pause and end screens
    public GameObject spellsMenu;
    public GameObject endsMenu;

    List<string> lines = new List<string>();
    public TextMeshProUGUI linesText;

    //audio for when you're wrong
    public AudioSource wrong;

    //materials for the arrows
    Color entered = new Color(0.4f, 0.9f, 0, 1);
    Color clear = new Color(1, 1, 1, 1);

    public Material enteredMat;
    public Material clearMat;

    //creating where the positions are depending on how many arrows there are. Three and five share cuz 3 is just 5 without the outsides
    public List<Vector2> fourPos = new List<Vector2>();
    public List<Vector2> threefivePos = new List<Vector2>();
    public List<Vector2> sixPos = new List<Vector2>();

    //the list of spells
    public List<string> spellList = new List<string>();

    string spell;



    // Start is called before the first frame update
    void Start()
    {
        //positions for a spell with four commands
        fourPos.Add(new Vector2(-4.5f, 0));
        fourPos.Add(new Vector2(-1.5f, 0));
        fourPos.Add(new Vector2(1.5f, 0));
        fourPos.Add(new Vector2(4.5f, 0));

        //positions for a spell with five or three commands
        threefivePos.Add(new Vector2(-6, 0));
        threefivePos.Add(new Vector2(-3, 0));
        threefivePos.Add(new Vector2(0, 0));
        threefivePos.Add(new Vector2(3, 0));
        threefivePos.Add(new Vector2(6, 0));

        //positions for a spell with six commands
        sixPos.Add(new Vector2(-7.5f, 0));
        sixPos.Add(new Vector2(-4.5f, 0));
        sixPos.Add(new Vector2(-1.5f, 0));
        sixPos.Add(new Vector2(1.5f, 0));
        sixPos.Add(new Vector2(4.5f, 0));
        sixPos.Add(new Vector2(7.5f, 0));

        arrows.Add(arrow0);
        arrows.Add(arrow1);
        arrows.Add(arrow2);
        arrows.Add(arrow3);
        arrows.Add(arrow4);
        arrows.Add(arrow5);

        //adding the spells to the list
        spellList.Add("Detect Magic");
        spellList.Add("Presto");
        spellList.Add("Shocking Grasp");
        spellList.Add("Dominate Person");
        spellList.Add("Power Word Kill");

        lines.Add("My grandmother could do better!");
        lines.Add("GAAAAAAAAAAAAAARBAGE!");
        lines.Add("What, do you want a medal?");
        lines.Add("Passable.");
        lines.Add("Objection!");
        lines.Add("Just... Do better next time");

    }

    //rolls a random spell and sets up thr arrows
    public void rollSpell()
    {
        //resetting position and arrows
        place = 0;
        arrow0.material = clearMat;
        arrow1.material = clearMat;
        arrow2.material = clearMat;
        arrow3.material = clearMat;
        arrow4.material = clearMat;
        arrow5.material = clearMat;

        //adds a second to the timer
        remainingTime++;

        score++;

        //rolling the spell
        int i = Random.Range(0, spellList.Count);
        while (spell == spellList[i])
        {
            i = Random.Range(0, spellList.Count);
            Debug.Log("dsafsafsa");
        }

        spellName.text = spellList[i];
        spell = spellList[i];


        //sets arrows depending on how long the spell is
        if (spell == "Detect Magic" || spell == "Power Word Kill")
        {
            arrow0.gameObject.SetActive(true);
            arrow1.gameObject.SetActive(true);
            arrow2.gameObject.SetActive(true);
            arrow3.gameObject.SetActive(false);
            arrow4.gameObject.SetActive(false);
            arrow5.gameObject.SetActive(false);
            arrow0.gameObject.transform.position = threefivePos[1];
            arrow1.gameObject.transform.position = threefivePos[2];
            arrow2.gameObject.transform.position = threefivePos[3];
            max = 3;
        }
        if (spell == "Presto" || spell == "Shocking Grasp")
        {
            arrow0.gameObject.SetActive(true);
            arrow1.gameObject.SetActive(true);
            arrow2.gameObject.SetActive(true);
            arrow3.gameObject.SetActive(true);
            arrow4.gameObject.SetActive(false);
            arrow5.gameObject.SetActive(false);
            arrow0.gameObject.transform.position = fourPos[0];
            arrow1.gameObject.transform.position = fourPos[1];
            arrow2.gameObject.transform.position = fourPos[2];
            arrow3.gameObject.transform.position = fourPos[3];

            max = 4;
        }
        if (spell == "Dominate Person")
        {
            arrow0.gameObject.SetActive(true);
            arrow1.gameObject.SetActive(true);
            arrow2.gameObject.SetActive(true);
            arrow3.gameObject.SetActive(true);
            arrow4.gameObject.SetActive(true);
            arrow5.gameObject.SetActive(false);
            arrow0.gameObject.transform.position = threefivePos[0];
            arrow1.gameObject.transform.position = threefivePos[1];
            arrow2.gameObject.transform.position = threefivePos[2];
            arrow3.gameObject.transform.position = threefivePos[3];
            arrow4.gameObject.transform.position = threefivePos[4];
        
            max = 5;
        }

        //sets actual rotations for the spells
        if (spell == "Detect Magic")
        {
            arrow0.gameObject.tag = "down";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "down";
        }

        if (spell == "Power Word Kill")
        {
            arrow0.gameObject.tag = "right";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "left";
        }

        if (spell == "Presto")
        {
        
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "down";
            arrow2.gameObject.tag = "right";
            arrow3.gameObject.tag = "right";
        }
        if (spell == "Shocking Grasp")
        {
            arrow0.gameObject.tag = "right";
            arrow1.gameObject.tag = "right";
            arrow2.gameObject.tag = "left";
            arrow3.gameObject.tag = "left";
        }
        if (spell == "Dominate Person")
        {
            arrow0.gameObject.tag = "down";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "left";
            arrow3.gameObject.tag = "left";
            arrow4.gameObject.tag = "right";
        }

        for (int j = 0; j < arrows.Count; j++)
        {
            if (arrows[j].gameObject.tag == "up")
            {
                arrows[j].gameObject.transform.rotation = up;
            }else if (arrows[j].gameObject.tag == "down")
            {
                arrows[j].gameObject.transform.rotation = down;
            }else if (arrows[j].gameObject.tag == "left")
            {
                arrows[j].gameObject.transform.rotation = left;
            }else if (arrows[j].gameObject.tag == "right")
            {
                arrows[j].gameObject.transform.rotation = right;
            }
        }

    }

    public void Up(InputAction.CallbackContext context)
    {
        if (lose == false && pause == false)
        {
            if (context.performed)
            {
                //if (arrows[place].gameObject.transform.rotation == up)
                if (arrows[place].gameObject.tag == "up")
                {
                    arrows[place].material = enteredMat;
                    place++;
                }
                else
                {
                    whoops();
                }
            }
        }
    }

    public void Down(InputAction.CallbackContext context)
    {

        if (lose == false && pause == false)
        {
            if (context.performed)
            {
                if (arrows[place].gameObject.tag == "down")
                {
                    arrows[place].material = enteredMat;
                    place++;
                }
                else
                {
                    whoops();
                }
            }
        }
    }

    public void Left(InputAction.CallbackContext context)
    {
        if (lose == false && pause == false)
        {
            if (context.performed)
            {
                if (arrows[place].gameObject.tag == "left")
                {
                    arrows[place].material = enteredMat;
                    place++;
                }
                else
                {
                    whoops();
                }
            }
        }
    }

    public void Right(InputAction.CallbackContext context)
    {

        if (lose == false && pause == false)
        {
            if (context.performed)
            {
                if (arrows[place].gameObject.tag == "right")
                {
                    arrows[place].material = enteredMat;
                    place++;
                }
                else
                {
                    whoops();
                }
            }
        }
    }

    void whoops()
    {
        place = 0;
        wrong.Play(0);
        Debug.Log("bad");
        arrow0.material = clearMat;
        arrow1.material = clearMat;
        arrow2.material = clearMat;
        arrow3.material = clearMat;
        arrow4.material = clearMat;
        arrow5.material = clearMat;
    }

    void Update()
    {
        if (lose == false && pause == false)
        {
            remainingTime -= Time.deltaTime;
            timer.value = remainingTime;
            //rolls spell whenever you're done
            if (place == max)
            {
                rollSpell();
            }
            if (Input.GetKeyDown("space"))
            {
                rollSpell();
            }
        }
        if (Input.GetKeyDown("escape"))
        {
            pause = !pause;
            spellsMenu.SetActive(pause);
        }
        if (remainingTime <= 0)
        {
            if (lose == false)
            {
                int i = Random.Range(0, lines.Count);
                linesText.text = lines[i] + " -Professor Ex-Lawyer";
            }
            lose = true;
            if (score > highScore)
            {
                highScore = score;
            }
            highScoreText.text = "High Score: " + highScore;
            scoreText.text = "Score: " + score;
            endsMenu.SetActive(true);
            spellName.gameObject.SetActive(false);
        }
    }
    public void Restart()
    {
        remainingTime = 30;
        rollSpell();
        endsMenu.SetActive(false);
        spellName.gameObject.SetActive(true);
        lose = false;
    }
}
