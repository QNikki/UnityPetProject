using UnityEditor;
using UnityEngine;
using Dungeon;

namespace Dungeon
{
   public class GenerateDungeonPb : EditorWindow
   { 
   private Object generateConfigRef;

   [MenuItem("PetProject/Generate Dungeon")]
   public static void OpenGenerateLevelWindow()
   {
      GetWindow<GenerateDungeonPb>();
   }

   private void OnEnable()
   {
      ShowModal();
   }

   private void OnGUI()
   {
      generateConfigRef = EditorGUILayout.ObjectField(generateConfigRef, typeof(DungeonGenerateConfig), true);

      if (GUILayout.Button("Generate Dungeon"))
         GenerateDungeon();
   }

      private void GenerateDungeon()
      {
         if (generateConfigRef == null)
         {
            Debug.LogError("нет конфига генерации уровня");
            return;
         }

         DungeonGenerateConfig generateConfig = generateConfigRef as DungeonGenerateConfig;

         float width = 0;
         float height = 0;

         var LevelHolder = new GameObject();
      }
   }
}
