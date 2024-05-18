using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class SpellsLogic : MonoBehaviour
{

    //losing and pausing
    bool lose = false;
    bool pause = false;

    //scoring
    int score = -1;
    int highScore = -1;
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
    Quaternion dow = Quaternion.Euler(90, 90, 0);
    Quaternion le = Quaternion.Euler(-180, 90, 0);
    Quaternion ri = Quaternion.Euler(0, 90, 0);

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
    public AudioSource music;

    int musicTrack = 0;
    public List<AudioClip> audioList = new List<AudioClip>();
    public List<AudioClip> musicList = new List<AudioClip>();
    public AudioClip end;

    //materials for the arrows
    Color entered = new Color(0.4f, 0.9f, 0, 1);
    Color clear = new Color(1, 1, 1, 1);

    public Material enteredMat;
    public Material clearMat;

    //creating where the positions are depending on how many arrows there are. Three and five share cuz 3 is just 5 without the outsides
    public List<Vector2> threefivePos = new List<Vector2>();
    public List<Vector2> foursixPos = new List<Vector2>();

    //the list of spells
    public List<string> spellList = new List<string>();

    string spell;



    // Start is called before the first frame update
    void Start()
    {
        //positions for a spell with five or three commands
        threefivePos.Add(new Vector2(-6, 0));
        threefivePos.Add(new Vector2(-3, 0));
        threefivePos.Add(new Vector2(0, 0));
        threefivePos.Add(new Vector2(3, 0));
        threefivePos.Add(new Vector2(6, 0));

        //positions for a spell with six commands
        foursixPos.Add(new Vector2(-7.5f, 0));
        foursixPos.Add(new Vector2(-4.5f, 0));
        foursixPos.Add(new Vector2(-1.5f, 0));
        foursixPos.Add(new Vector2(1.5f, 0));
        foursixPos.Add(new Vector2(4.5f, 0));
        foursixPos.Add(new Vector2(7.5f, 0));

        //assmeble the arrows
        arrows.Add(arrow0);
        arrows.Add(arrow1);
        arrows.Add(arrow2);
        arrows.Add(arrow3);
        arrows.Add(arrow4);
        arrows.Add(arrow5);

        //adding the spells to the list
        spellList.Add("Ignis");
        spellList.Add("Eldritch Ray");
        spellList.Add("Barrier");
        spellList.Add("Charm");
        spellList.Add("Detect Spell");
        spellList.Add("Magic Missile");
        spellList.Add("Mend Wound");
        spellList.Add("Foggy Step");
        spellList.Add("Reanimate");
        spellList.Add("Speed");
        spellList.Add("Power Word");
        spellList.Add("Grant Wish");
        spellList.Add("Fireball");

        spellList.Add("Twenty Six");
        spellList.Add("Drac Flow");
        spellList.Add("Sigma");
        spellList.Add("Once Asking");
        spellList.Add("Wojeks");
        spellList.Add("Crease Jorn");
        spellList.Add("Jinner");
        spellList.Add("Skibidi");
        spellList.Add("Dead Heavy");
        spellList.Add("Weatherboy");
        spellList.Add("Bad Luck");
        spellList.Add("Yassification");
        spellList.Add("Walkin'");
        
        spellList.Add("Antiperson");
        spellList.Add("Helltower");
        spellList.Add("Raven Wings");
        spellList.Add("Trash Blast");
        spellList.Add("Starbeam");
        spellList.Add("Enforce");
        spellList.Add("Raven's Call");
        spellList.Add("Raven Bomb");
        spellList.Add("Dawn Fire");
        spellList.Add("Fire Barrier");
        spellList.Add("Hellhound");
        spellList.Add("Pain Rain");
        spellList.Add("Hellbomb");


        //adding audio
        
        //bitch lines for the end of the round
        lines.Add("My grandmother could do better!");
        lines.Add("GAAAAAAAAAAAAAARBAGE!");
        lines.Add("Press esc to turn off the hard spells.");
        lines.Add("Press esc to turn off the hard spells.");
        lines.Add("What, do you want a medal?");
        lines.Add("Passable.");
        lines.Add("Objection!");
        lines.Add("Just... Do better next time...");

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
        if (spellList.Count > 1)
        {
            int i = Random.Range(0, spellList.Count);
            while (spell == spellList[i])
            {
                i = Random.Range(0, spellList.Count);
            }

            spellName.text = spellList[i];
            spell = spellList[i];
        }else
        {
            spellName.text = "Prestidigitation";
            spell = "Prestidigitation";
        }

        //sets arrows depending on how long the spell is
        if (spell == "Sigma" || spell == "Drac Flow" || spell == "Twenty Six" || spell == "Detect Spell" || spell == "Charm" || spell == "Barrier" || spell == "Eldritch Ray" || spell == "Ignis")
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
        if (spell == "Raven Wings" || spell == "Helltower" || spell == "Antiperson" || spell == "Wojeks" || spell == "Once Asking" || spell == "Foggy Step" || spell == "Mend Wound" || spell == "Magic Missile")
        {
            arrow0.gameObject.SetActive(true);
            arrow1.gameObject.SetActive(true);
            arrow2.gameObject.SetActive(true);
            arrow3.gameObject.SetActive(true);
            arrow4.gameObject.SetActive(false);
            arrow5.gameObject.SetActive(false);
            arrow0.gameObject.transform.position = foursixPos[1];
            arrow1.gameObject.transform.position = foursixPos[2];
            arrow2.gameObject.transform.position = foursixPos[3];
            arrow3.gameObject.transform.position = foursixPos[4];

            max = 4;
        }
        if (spell == "Dawn Fire" || spell == "Raven Bomb" || spell == "Raven's Call" || spell == "Enforce" || spell == "Starbeam" || spell == "Trash Blast" || spell == "Dead Heavy" || spell == "Skibidi" || spell == "Jinner" || spell == "Crease Jorn" || spell == "Power Word" || spell == "Speed" || spell == "Reanimate")
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
        if (spell == "Prestidigitation" || spell == "Hellbomb" || spell == "Pain Rain" || spell == "Hellhound" || spell == "Fire Barrier" || spell == "Walkin'" || spell == "Yassification" || spell == "Bad Luck" || spell == "Weatherboy" || spell == "Fireball" || spell == "Grant Wish")
        {
            arrow0.gameObject.SetActive(true);
            arrow1.gameObject.SetActive(true);
            arrow2.gameObject.SetActive(true);
            arrow3.gameObject.SetActive(true);
            arrow4.gameObject.SetActive(true);
            arrow5.gameObject.SetActive(true);
            arrow0.gameObject.transform.position = foursixPos[0];
            arrow1.gameObject.transform.position = foursixPos[1];
            arrow2.gameObject.transform.position = foursixPos[2];
            arrow3.gameObject.transform.position = foursixPos[3];
            arrow4.gameObject.transform.position = foursixPos[4];
            arrow5.gameObject.transform.position = foursixPos[5];

            max = 6;
        }

        //sets actual rotations for the spells
        if (spell == "Ignis")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "ri";
        }

        if (spell == "Eldritch Ray")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "up";
        }

        if (spell == "Barrier")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "up";
        }
        if (spell == "Charm")
        {
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "ri";
        }
        if (spell == "Detect Spell")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "le";
        }
        if (spell == "Magic Missile")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "ri";
            arrow3.gameObject.tag = "ri";
        }
        if (spell == "Mend Wound")
        {
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "le";
        }
        if (spell == "Foggy Step")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "do";
            arrow3.gameObject.tag = "do";
        }
        if (spell == "Reanimate")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "ri";
            arrow3.gameObject.tag = "ri";
            arrow4.gameObject.tag = "do";
        }
        if (spell == "Speed")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "ri";
            arrow3.gameObject.tag = "ri";
            arrow4.gameObject.tag = "ri";
        }
        if (spell == "Power Word")
        {
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "do";
            arrow3.gameObject.tag = "do";
            arrow4.gameObject.tag = "do";
        }
        if (spell == "Grant Wish")
        {
            arrow0.gameObject.tag = "ri";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "up";
            arrow4.gameObject.tag = "up";
            arrow5.gameObject.tag = "ri";
        }
        if (spell == "Fireball")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "le";
            arrow3.gameObject.tag = "le";
            arrow4.gameObject.tag = "le";
            arrow5.gameObject.tag = "ri";
        }

        //fey spells
        if (spell == "Twenty Six")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "do";
        }
        if (spell == "Drac Flow")
        {
            arrow0.gameObject.tag = "ri";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "le";
        }
        if (spell == "Sigma")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "ri";
        }
        if (spell == "Once Asking")
        {
            arrow0.gameObject.tag = "ri";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "le";
            arrow3.gameObject.tag = "le";
        }
        if (spell == "Wojeks")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "ri";
            arrow3.gameObject.tag = "do";
        }
        if (spell == "Crease Jorn")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "ri";
            arrow4.gameObject.tag = "do";
        }
        if (spell == "Jinner")
        {
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "le";
            arrow4.gameObject.tag = "up";
        }
        if (spell == "Skibidi")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "up";
            arrow4.gameObject.tag = "ri";
        }
        if (spell == "Dead Heavy")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "le";
            arrow4.gameObject.tag = "ri";
        }
        if (spell == "Weatherboy")
        {
            arrow0.gameObject.tag = "le";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "le";
            arrow3.gameObject.tag = "ri";
            arrow4.gameObject.tag = "le";
            arrow5.gameObject.tag = "ri";
        }
        if (spell == "Bad Luck")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "up";
            arrow4.gameObject.tag = "do";
            arrow5.gameObject.tag = "do";
        }
        if (spell == "Yassification")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "ri";
            arrow3.gameObject.tag = "le";
            arrow4.gameObject.tag = "do";
            arrow5.gameObject.tag = "do";
        }
        if (spell == "Walkin'")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "do";
            arrow3.gameObject.tag = "do";
            arrow4.gameObject.tag = "ri";
            arrow5.gameObject.tag = "do";
        }

        //hell spells
        if (spell == "Antiperson")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "ri";
        }
        if (spell == "Helltower")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "ri";
            arrow3.gameObject.tag = "ri";
        }
        if (spell == "Raven Wings")
        {
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "do";
            arrow3.gameObject.tag = "ri";
        }
        if (spell == "Trash Blast")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "le";
            arrow3.gameObject.tag = "up";
            arrow4.gameObject.tag = "ri";
        }
        if (spell == "Starbeam")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "le";
            arrow2.gameObject.tag = "do";
            arrow3.gameObject.tag = "up";
            arrow4.gameObject.tag = "le";
        }
        if (spell == "Enforce")
        {
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "ri";
            arrow3.gameObject.tag = "le";
            arrow4.gameObject.tag = "up";
        }
        if (spell == "Raven's Call")
        {
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "le";
            arrow3.gameObject.tag = "up";
            arrow4.gameObject.tag = "ri";
        }
        if (spell == "Raven Bomb")
        {
            arrow0.gameObject.tag = "up";
            arrow1.gameObject.tag = "ri";
            arrow2.gameObject.tag = "do";
            arrow3.gameObject.tag = "do";
            arrow4.gameObject.tag = "do";
        }
        if (spell == "Dawn Fire")
        {
            arrow0.gameObject.tag = "ri";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "ri";
            arrow4.gameObject.tag = "do";
        }
        if (spell == "Fire Barrier")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "le";
            arrow3.gameObject.tag = "ri";
            arrow4.gameObject.tag = "le";
            arrow5.gameObject.tag = "ri";
        }
        if (spell == "Hellhound")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "le";
            arrow3.gameObject.tag = "up";
            arrow4.gameObject.tag = "ri";
            arrow5.gameObject.tag = "ri";
        }
        if (spell == "Pain Rain")
        {
            arrow0.gameObject.tag = "ri";
            arrow1.gameObject.tag = "do";
            arrow2.gameObject.tag = "up";
            arrow3.gameObject.tag = "do";
            arrow4.gameObject.tag = "le";
            arrow5.gameObject.tag = "do";
        }
        if (spell == "Hellbomb")
        {
            arrow0.gameObject.tag = "do";
            arrow1.gameObject.tag = "up";
            arrow2.gameObject.tag = "le";
            arrow3.gameObject.tag = "do";
            arrow4.gameObject.tag = "up";
            arrow5.gameObject.tag = "ri";
        }


        //special random spell if you select 1 or nothing
        if (spell == "Prestidigitation")
        {

            List<string> direc = new List<string>();
            direc.Add("up");
            direc.Add("do");
            direc.Add("le");
            direc.Add("ri");
            int i0 = Random.Range(0, 3);
            int i1 = Random.Range(0, 3);
            int i2 = Random.Range(0, 3);
            int i3 = Random.Range(0, 3);
            int i4 = Random.Range(0, 3);
            int i5 = Random.Range(0, 3);
            arrow0.gameObject.tag = direc[i0];
            arrow1.gameObject.tag = direc[i1];
            arrow2.gameObject.tag = direc[i2];
            arrow3.gameObject.tag = direc[i3];
            arrow4.gameObject.tag = direc[i4];
            arrow5.gameObject.tag = direc[i5];
        }
        //put arrows in order
        for (int j = 0; j < arrows.Count; j++)
            {
                if (arrows[j].gameObject.tag == "up")
                {
                    arrows[j].gameObject.transform.rotation = up;
                } else if (arrows[j].gameObject.tag == "do")
                {
                    arrows[j].gameObject.transform.rotation = dow;
                } else if (arrows[j].gameObject.tag == "le")
                {
                    arrows[j].gameObject.transform.rotation = le;
                } else if (arrows[j].gameObject.tag == "ri")
                {
                    arrows[j].gameObject.transform.rotation = ri;
                }
            }
    }
    //detecting button presses
    public void Up(InputAction.CallbackContext context)
    {
        if (lose == false && pause == false)
        {
            if (context.performed)
            {
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
                if (arrows[place].gameObject.tag == "do")
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
                if (arrows[place].gameObject.tag == "le")
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
                if (arrows[place].gameObject.tag == "ri")
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
    //POV you messed up
    void whoops()
    {
        place = 0;
        if (!wrong.isPlaying)
        {
            int i = Random.Range(0, audioList.Count);
            wrong.clip = audioList[i];
            wrong.Play();
        }
        arrow0.material = clearMat;
        arrow1.material = clearMat;
        arrow2.material = clearMat;
        arrow3.material = clearMat;
        arrow4.material = clearMat;
        arrow5.material = clearMat;
    }

    //toggling spells from menu
    public void OnClicked(Toggle button)
    {
        print(button.name);
        if (button.isOn == false)
        {
            spellList.Remove(button.name);
        }
        else
        {
            spellList.Add(button.name);
        }
    }

    void Update()
    {
        if (!music.isPlaying)
        {
            music.clip = musicList[musicTrack];
            music.Play();
            musicTrack++;
            if (musicTrack >= 5)
            {
                musicTrack = 0;
            }
        }
        if (lose == false && pause == false)
        {
            remainingTime -= Time.deltaTime;
            timer.value = remainingTime;
            //rolls spell whenever you're done
            if (place == max)
            {
                rollSpell();
            }
        }
        //open the pause menu
        if ((Input.GetKeyDown("escape") || Input.GetKeyDown("space")) && lose == false)
        {
            pause = !pause;
            spellsMenu.SetActive(pause);
        }
        if ((Input.GetKeyDown("escape") || Input.GetKeyDown("space")) && lose == true)
        {
            Restart();
        }
        //handles losing the game and such
        if (remainingTime <= 0 && lose == false)
        {
            int i = Random.Range(0, lines.Count);
            linesText.text = lines[i] + " -Professor Ex-Lawyer";
            wrong.clip = end;
            wrong.Play();
            
            if (score > highScore)
            {
                highScore = score;
            }
            highScoreText.text = "High Score: " + highScore;
            scoreText.text = "Score: " + score;
            endsMenu.SetActive(true);
            spellName.gameObject.SetActive(false);
            lose = true;
        }
    }
    //resets the game after
    public void Restart()
    {
        score = -1;
        remainingTime = 15;
        rollSpell();
        endsMenu.SetActive(false);
        spellName.gameObject.SetActive(true);
        lose = false;
    }
}
