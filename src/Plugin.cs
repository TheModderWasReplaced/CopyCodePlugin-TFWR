using BepInEx;
using FarmHelper.API.Attributes;
using HarmonyLib;

namespace CopyCode;

[BepInPlugin("org.warpersan.copycode", "Copy Code", "1.0.0.0")]
[FarmInfo("WarperSan", "https://github.com/WarperSan/CopyCodePlugin-TFWR")]
internal class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony(Info.Metadata.GUID);
        harmony.PatchAll();
        Log.SetLogger(Logger);
    }
}
