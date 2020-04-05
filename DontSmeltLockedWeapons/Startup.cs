using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace DontSmeltLockedWeapons
{
    public class Startup : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            try
            {
                var harmony = new Harmony("mod.bannerlord.dontsmeltlockedweapons");
                harmony.PatchAll();
            }
            catch
            {
            }
        }
    }
}
