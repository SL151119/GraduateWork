#if UNITY_EDITOR
using UnityEditor;
using Crosstales.RTVoice.EditorUtil;

namespace Crosstales.RTVoice.MaryTTS
{
   /// <summary>Editor component for for adding the prefabs from 'MaryTTS' in the "Hierarchy"-menu.</summary>
   public static class VoiceProviderMaryTTSGameObject
   {
      [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/VoiceProviderMaryTTS", false, EditorUtil.EditorHelper.GO_ID + 10)]
      private static void AddVoiceProvider()
      {
         EditorHelper.InstantiatePrefab("MaryTTS", $"{EditorConfig.ASSET_PATH}Extras/MaryTTS/Resources/Prefabs/");
      }

      [MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/VoiceProviderMaryTTS", true)]
      private static bool AddVoiceProviderValidator()
      {
         return !VoiceProviderMaryTTSEditor.isPrefabInScene;
      }
   }
}
#endif
// © 2020-2021 crosstales LLC (https://www.crosstales.com)