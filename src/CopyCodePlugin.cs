using BepInEx;
using FarmHelper.API.Attributes;
using HarmonyLib;

namespace CopyCode;

[BepInPlugin("org.warpersan.copycode", "Copy Code", "1.0.0.0")]
[FarmInfo("WarperSan", "https://github.com/WarperSan/CopyCodePlugin-TFWR")]
public class CopyCodePlugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony("org.warpersan.copycode");
        harmony.PatchAll();
        Log.SetLogger(Logger);
    }
}
