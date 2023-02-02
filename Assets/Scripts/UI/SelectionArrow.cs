using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentPos;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangPos(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangPos(1);
        }


        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        options[currentPos].GetComponent<Button>().onClick.Invoke();
    }

    private void ChangPos(int _change)
    {
        currentPos += _change;

        if (_change != 0)
        {
            SoundManager.instance.PlaySound(changeSound);
        }

        if (currentPos < 0)
        {
            currentPos = options.Length - 1;
        }
        else if (currentPos > options.Length - 1)
        {
            currentPos = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currentPos].position.y, 0);
    }

}
