using BepInEx;
using Log = CopyCode.Helpers.Log;
using Patch = CopyCode.Helpers.Patch;

namespace CopyCode;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
internal class Plugin : BaseUnityPlugin
{
	private void Awake() {
		Patch.ApplyAll();
		Log.Info($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
	}

	private void OnDestroy() {
		Patch.RevertAll();
		Log.Info($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has unloaded!");
	}
}