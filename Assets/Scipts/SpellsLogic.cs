using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class SpellsLogic : MonoBehaviour
{
    //the text that will display the spell name
    public TextMeshProUGUI spellName;

    //gathering the arrows
    public List<SpriteRenderer> arrows = new List<SpriteRenderer>();
    public SpriteRenderer arrow0;
    public SpriteRenderer arrow1;
    public SpriteRenderer arrow2;
    public SpriteRenderer arrow3;
    public SpriteRenderer arrow4;
    public SpriteRenderer arrow5;

    //creating rotations for arrows
    Quaternion up = Quaternion.Euler(0, 0, 0);
    Quaternion down = Quaternion.Euler(0, 0, 180);
    Quaternion left = Quaternion.Euler(0, 0, 90);
    Quaternion right = Quaternion.Euler(0, 0, 270);

    //the variable that sees how far in the spell you are
    int place = 0;

    int max;

    //audio for when you're wrong
    public AudioSource wrong;

    //colors for the arrows
    Color entered = new Color(0.4f, 0.9f, 0, 1);
    Color clear = new Color(1, 1, 1, 1);

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



    }

    //rolls a random spell and sets up thr arrows
    public void rollSpell()
    {
        //resetting position and arrows
        place = 0;
        arrow0.color = clear;
        arrow1.color = clear;
        arrow2.color = clear;
        arrow3.color = clear;
        arrow4.color = clear;
        arrow5.color = clear;

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
        if (spell == "Detect Magic")
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
        if (context.performed)
        {
            //if (arrows[place].gameObject.transform.rotation == up)
            if (arrows[place].gameObject.tag == "up")
            {
                arrows[place].color = entered;
                place ++;
            }else
            {
                whoops();
            }
        }
    }

    public void Down(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (arrows[place].gameObject.tag == "down")
            {
                arrows[place].color = entered;
                place ++;
            }else
            {
                whoops();
            }
        }
    }

    public void Left(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (arrows[place].gameObject.tag == "left")
            {
                arrows[place].color = entered;
                place ++;
            }else
            {
                whoops();
            }
        }
    }

    public void Right(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (arrows[place].gameObject.tag == "right")
            {
                arrows[place].color = entered;
                place ++;
            }else
            {
                whoops();
            }
        }
    }

    void whoops()
    {
        place = 0;
        wrong.Play(0);
        Debug.Log("bad");
        arrow0.color = clear;
        arrow1.color = clear;
        arrow2.color = clear;
        arrow3.color = clear;
        arrow4.color = clear;
        arrow5.color = clear;
    }

    void Update()
    {
        //rolls spell whenever you're done
        if (place == max)
        {
            rollSpell();
        }
        if (Input.GetKeyDown("space"))
        {
            rollSpell();
        }

        /*
        //entering spells logic
        if (spell == "Detect Magic")
        {
            if (Input.GetKeyDown("s") && place == 0)
            {
                arrow0.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("w") && place == 1)
            {
                arrow1.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("s") && place == 2)
            {
                arrow2.color = entered;
                place ++;
                done = true;
            }
        }
        if (spell == "Presto")
        {
            if (Input.GetKeyDown("w") && place == 0)
            {
                arrow0.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("s") && place == 1)
            {
                arrow1.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("d") && place == 2)
            {
                arrow2.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("d") && place == 3)
            {
                arrow3.color = entered;
                place ++;
                done = true;

            }
        }
        if (spell == "Shocking Grasp")
        {
            if (Input.GetKeyDown("d") && place == 0)
            {
                arrow0.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("d") && place == 1)
            {
                arrow1.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("a") && place == 2)
            {
                arrow2.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("a") && place == 3)
            {
                arrow3.color = entered;
                place ++;
                done = true;
            }
        }
        if (spell == "Dominate Person")
        {
            if (Input.GetKeyDown("s") && place == 0)
            {
                arrow0.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("w") && place == 1)
            {
                arrow1.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("a") && place == 2)
            {
                arrow2.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("a") && place == 3)
            {
                arrow3.color = entered;
                place ++;
            }
            else if (Input.GetKeyDown("d") && place == 4)
            {
                arrow4.color = entered;
                place ++;
                done = true;
            }
        }
        */
    }
}