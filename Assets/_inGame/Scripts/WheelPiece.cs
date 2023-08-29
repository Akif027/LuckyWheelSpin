using UnityEngine ;

namespace EasyUI.PickerWheelUI {
   [System.Serializable]
   public class WheelPiece {
        public UnityEngine.Sprite Icon;
        public string Label ;

      [Tooltip ("Reward amount")] public int Amount ;
        
        [Tooltip("Probability in %")]
        [Range(0f, 100f)]
        public float Chance = Random.Range(0f, 100f);

        [HideInInspector] public int Index ;
      [HideInInspector] public double _weight = 0f ;
   }
}
