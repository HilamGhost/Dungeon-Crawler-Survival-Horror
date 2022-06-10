using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SHDC.Dialogue 
{
    public class DialogueSystem : Singleton<DialogueSystem>
    {

        [SerializeField] bool isOpen;
        [SerializeField] bool isContinuing;
        [SerializeField] int currentQuote;

        List<Quotes> quotes;

        [SerializeField] string currentText = "";
        [SerializeField] float delay = 0.1f;
        [Header("UI")]
        [SerializeField] GameObject dialogueGO;
        [SerializeField] TMP_Text nameText;
        [SerializeField] TMP_Text text;
        

        #region Properties
        public bool IsOpen => isOpen;
        #endregion


        private void Start()
        {
            InteractWithDialogue();
        }
        private void Update()
        {
            text.text = currentText;
        }
        IEnumerator StartDialogue(Quotes quote)
        {

            nameText.text = quote.charName;

            isContinuing = true;
            for (int i = 0; i < quote.quote.Length + 1; i++)
            {
                currentText = quote.quote.Substring(0, i);
                yield return new WaitForSeconds(delay);
            }
            isContinuing = false;
        }
        /// <summary>
        /// Starts the Dialogue on UI
        /// </summary>
        public void TakeQuote(List<Quotes> quoteLists)
        {
            isOpen = true;
            quotes = quoteLists;
            dialogueGO.SetActive(true);
            StartCoroutine(StartDialogue(quotes[currentQuote]));
        }

        /// <summary>
        /// Closes the Dialogue or Skips the Quote
        /// </summary>
        public void InteractWithDialogue()
        {
            if (isContinuing)
            {
                StopAllCoroutines();
                currentText = quotes[currentQuote].quote;
                isContinuing = false;
            }
            else
            {
                currentQuote++;
                currentText = "";
                nameText.text = "";
                
                if (currentQuote < quotes?.Count) 
                {
                    StartCoroutine(StartDialogue(quotes[currentQuote])); 
                }
                else
                {
                    currentQuote = 0;
                    currentText = "";
                    nameText.text = "";
                    dialogueGO.SetActive(false);
                    isOpen = false;
                    
                }
            }
        }
    }
}

    
