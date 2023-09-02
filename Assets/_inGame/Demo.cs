﻿using UnityEngine ;
using EasyUI.PickerWheelUI ;
using UnityEngine.UI ;
using TMPro;

public class Demo : MonoBehaviour {
   [SerializeField] private Button uiSpinButton ;
   [SerializeField] private TMP_Text uiSpinButtonText ;

   [SerializeField] private PickerWheel pickerWheel ;


   private void Start () {
      uiSpinButton.onClick.AddListener (() => {

         uiSpinButton.interactable = false ;
         uiSpinButtonText.text = "Spinning" ;

         pickerWheel.OnSpinEnd (wheelPiece => {
            Debug.Log (
               @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
               + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
            ) ;

             UiManager.Instance.Winner(wheelPiece.Label,wheelPiece.Icon);
            uiSpinButton.interactable = true ;
            uiSpinButtonText.text = "Spin" ;
         }) ;

         pickerWheel.Spin () ;

      }) ;

   }

}
