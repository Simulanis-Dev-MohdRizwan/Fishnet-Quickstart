using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProcessCount : MonoBehaviour
{
    public int steps = 0;
    public List<Button> buttons;
    public UnityEvent OnAllButtonClicked;
    private void OnEnable()
    {
        if (buttons.Count != 0)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].onClick.AddListener(()=>Clicked(buttons[i]));
            }
        }
    }
    private void OnDisable()
    {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].onClick.RemoveListener(()=>Clicked(buttons[i]));
            }
    }
    void Clicked(Button btn)
    {
        steps++;
        btn.interactable = false;
        btn.gameObject.SetActive(false);
        CheckProcessComplete();
    }
    void CheckProcessComplete()
    {
        if (steps==buttons.Count)
        {
            OnAllButtonClicked?.Invoke();
        }
    }
}
