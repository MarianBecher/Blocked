using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelector : MonoBehaviour {


    private List<PlayerController2D> characters;
    private int currentPlayer = 0;

    private bool lastFrameKeyPressed = true;
    public Transform arrowPrefab;
    private CharacterPointer lastArrow = null;

	// Use this for initialization
    void Start()
    {
	}
	
	// Update is called once per frame
	void Update () {
        handleSwitchCharacter();
	}

    public void rigisterNewCharacter(PlayerController2D character)
    {
        if(characters == null)
        {
            characters = new List<PlayerController2D>();
        }

        characters.Add(character);
        updateCurrentPlayerStatus();
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
                characters[i].active = true;
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
