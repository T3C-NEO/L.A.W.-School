using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellsLogic : MonoBehaviour
{
    //the text that will display the spell name
    public TextMeshProUGUI spellName;

    //gathering the arrows
    public SpriteRenderer arrow0;
    public SpriteRenderer arrow1;
    public SpriteRenderer arrow2;
    public SpriteRenderer arrow3;
    public SpriteRenderer arrow4;
    public SpriteRenderer arrow5;

    //creating rotations for arrows
    Quaternion up = new Quaternion(0, 0, 0, 0);
    Quaternion down = Quaternion.Euler(0, 0, 180);
    Quaternion left = Quaternion.Euler(0, 0, 90);
    Quaternion right = Quaternion.Euler(0, 0, 270);

    //the variable that sees how far in the spell you are
    int place = 0;

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

    //to see if you've finished the spell
    bool done = true;


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

        //adding the spells to the list
        spellList.Add("Detect Magic");
        spellList.Add("Presto");
        spellList.Add("Shocking Grasp");
        spellList.Add("Dominate Person");



    }

    //rolls a random spell and sets up thr arrows
    void rollSpell()
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
        }

        //sets actual rotations for the spells
        if (spell == "Detect Magic")
        {
            arrow0.gameObject.transform.rotation = down;
            arrow1.gameObject.transform.rotation = up;
            arrow2.gameObject.transform.rotation = down;
        }

        if (spell == "Presto")
        {
            arrow0.gameObject.transform.rotation = up;
            arrow1.gameObject.transform.rotation = down;
            arrow2.gameObject.transform.rotation = right;
            arrow3.gameObject.transform.rotation = right;
        }
        if (spell == "Shocking Grasp")
        {
            arrow0.gameObject.transform.rotation = right;
            arrow1.gameObject.transform.rotation = right;
            arrow2.gameObject.transform.rotation = left;
            arrow3.gameObject.transform.rotation = left;
        }
        if (spell == "Dominate Person")
        {
            arrow0.gameObject.transform.rotation = down;
            arrow1.gameObject.transform.rotation = up;
            arrow2.gameObject.transform.rotation = left;
            arrow3.gameObject.transform.rotation = left;
            arrow4.gameObject.transform.rotation = right;
        }

        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        //rolls spell whenever you're done
        if (done == true)
        {
            rollSpell();
        }

        //entering spells logic
        if (spell == "Detect Magic")
        {
            if (Input.GetKeyDown("s") && place == 0)
            {
                arrow0.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("w") && place == 1)
            {
                arrow1.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("s") && place == 2)
            {
                arrow2.color = entered;
                place += 1;
                done = true;
            }
        }
        if (spell == "Presto")
        {
            if (Input.GetKeyDown("w") && place == 0)
            {
                arrow0.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("s") && place == 1)
            {
                arrow1.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("d") && place == 2)
            {
                arrow2.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("d") && place == 3)
            {
                arrow3.color = entered;
                place += 1;
                done = true;

            }
        }
        if (spell == "Shocking Grasp")
        {
            if (Input.GetKeyDown("d") && place == 0)
            {
                arrow0.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("d") && place == 1)
            {
                arrow1.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("a") && place == 2)
            {
                arrow2.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("a") && place == 3)
            {
                arrow3.color = entered;
                place += 1;
                done = true;
            }
        }
        if (spell == "Dominate Person")
        {
            if (Input.GetKeyDown("s") && place == 0)
            {
                arrow0.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("w") && place == 1)
            {
                arrow1.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("a") && place == 2)
            {
                arrow2.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("a") && place == 3)
            {
                arrow3.color = entered;
                place += 1;
            }
            else if (Input.GetKeyDown("d") && place == 4)
            {
                arrow4.color = entered;
                place += 1;
                done = true;
            }
        }
    }
}
