
using UnityEngine;
using UnityEngine.UI;

public class SelecionArrow : MonoBehaviour
{
   [SerializeField] private RectTransform[] options;
   private RectTransform rect;
   private int currrentPosition;
   [SerializeField] private AudioClip changeSound;
   [SerializeField] private AudioClip interactSound;

   private void Awake()
   {
      rect = GetComponent<RectTransform>();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
      {
         changePosition(-1);
      }
      if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
      {
         changePosition(1);
      }


      if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
      {
         interact();
      }
   }

   private void changePosition(int change)
   {
      currrentPosition += change;

      if (change != 0)
      {
         SoundManager.instance.PlaySound(changeSound);
      }

      if (currrentPosition < 0)
      {
         currrentPosition = options.Length - 1;
      }
      else if (currrentPosition > options.Length - 1)
      {
         currrentPosition = 0;
      }
      rect.position = new Vector3(rect.position.x, options[currrentPosition].position.y, 0);
   }

   private void interact()
   {
      SoundManager.instance.PlaySound(interactSound);
      
      options[currrentPosition].GetComponent<Button>().onClick.Invoke();
   }
}
