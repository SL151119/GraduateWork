#if UNITY_EDITOR
using UnityEditor;
using Crosstales.RTVoice.EditorUtil;

namespace Crosstales.RTVoice.MaryTTS
{
   /// <summary>Editor component for for adding the prefabs from 'MaryTTS' in the "Tools"-menu.</summary>
   public static class VoiceProviderMaryTTSMenu
   {
      [MenuItem("Tools/" + Util.Constants.ASSET_NAME + "/Prefabs/VoiceProviderMaryTTS", false, EditorUtil.EditorHelper.MENU_ID + 200)]
      private static void AddVoiceProvider()
      {
         EditorHelper.InstantiatePrefab("MaryTTS", $"{EditorConfig.ASSET_PATH}Extras/MaryTTS/Resources/Prefabs/");
      }

      [MenuItem("Tools/" + Util.Constants.ASSET_NAME + "/Prefabs/VoiceProviderMaryTTS", true)]
      private static bool AddVoiceProviderValidator()
      {
         return !VoiceProviderMaryTTSEditor.isPrefabInScene;
      }
   }
}
#endif
// © 2020-2021 crosstales LLC (https://www.crosstales.com)