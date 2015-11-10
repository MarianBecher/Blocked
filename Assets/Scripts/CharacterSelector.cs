using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelector : MonoBehaviour {

    private CameraController camController;
    private List<PlayerController2D> characters;
    private int currentPlayer = 0;

    private bool lastFrameKeyPressed = true;
    public Transform arrowPrefab;
    private CharacterPointer lastArrow = null;
    private bool initilized = false;

	// Use this for initialization
    void Start()
    {
        if (!initilized)
            Initialize();
	}

    private void Initialize()
    {
        this.camController = transform.Find("Main Camera").GetComponent<CameraController>();
        characters = new List<PlayerController2D>();
        initilized = true;
    }

	
	// Update is called once per frame
	void Update () {
        handleSwitchCharacter();
	}

    public void rigisterNewCharacter(PlayerController2D character)
    {
        if (!initilized)
            Initialize();
        if (characters.IndexOf(character) <= 0)
        {
            characters.Add(character);
            updateCurrentPlayerStatus();
        }
    }

    public void handleSplitCharacter(PlayerController2D initiator, PlayerController2D child1, PlayerController2D child2)
    {
        rigisterNewCharacter(child1);
        rigisterNewCharacter(child2);
        this.currentPlayer = characters.IndexOf(child1);
        this.deleteCharacter(initiator);
        pointArrowTo(currentPlayer);
        updateCurrentPlayerStatus();
    }

    private void deleteCharacter(PlayerController2D character)
    {
        this.characters.Remove(character);
        Destroy(character.gameObject);
    }

    private void handleSwitchCharacter()
    {
        float switchKey = Input.GetAxis("SwitchCharacter");
        int direction = 0;
        if (switchKey > 0 && !lastFrameKeyPressed)
        {
            direction = 1;
            lastFrameKeyPressed = true;
        }
        else if (switchKey < 0 && !lastFrameKeyPressed)
        {
            direction = characters.Count -1;
            lastFrameKeyPressed = true;
        }
        else if (switchKey == 0)
        {
            lastFrameKeyPressed = false;
        }
        if(direction != 0 )
        {
            currentPlayer = (currentPlayer + direction) % characters.Count;
            updateCurrentPlayerStatus();
            pointArrowTo(currentPlayer);
        }
    }

    private void updateCurrentPlayerStatus()
    {
        if (currentPlayer >= characters.Count)
        {
            currentPlayer = 0;
        }
        for (int i = 0; i < characters.Count; i++)
        {
            if (i == currentPlayer)
            {
                characters[i].active = true;
                camController.target = characters[i].transform;
            }
            else
                characters[i].active = false;
        }
    }

    private void pointArrowTo(int index)
    {
        //kill remaining arrow
        if (lastArrow != null)
        {
            Destroy(lastArrow.gameObject);
        }

        //create arrow
        CharacterPointer arrow = ((Transform)Instantiate(arrowPrefab)).GetComponent<CharacterPointer>();
        arrow.objectToFollow = characters[currentPlayer].transform;
        lastArrow = arrow;

    }
}
