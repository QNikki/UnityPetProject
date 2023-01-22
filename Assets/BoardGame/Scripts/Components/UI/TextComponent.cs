using TMPro;
using UnityEngine.UI;
using Unity.Entities;

namespace BoardGame.Components.UI
{
    public class TextComponent: IComponentData
    {
        public TextMeshProUGUI TextPro;

        public Text Text;
    }
}
