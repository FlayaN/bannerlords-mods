using HarmonyLib;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.Smelting;
using TaleWorlds.Library;

namespace DontSmeltLockedWeapons.patches
{
    [HarmonyPatch(typeof(SmeltingVM))]
    public class SmeltingVMPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("RefreshList")]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Scanned by harmony")]
        static void Postfix(SmeltingVM __instance)
        {
            var lockedItemIds = Campaign.Current.GetCampaignBehavior<InventoryLockTracker>().GetLocks()?.Select(x => x.Item.Id).ToList();

            if(lockedItemIds == null)
            {
                return;
            }

            var filteredItemList = new MBBindingList<SmeltingItemVM>();

            foreach(var smeltableItem in __instance.SmeltableItemList)
            {
                if(!lockedItemIds.Contains(smeltableItem.Item.Id))
                {
                    filteredItemList.Add(smeltableItem);
                }
            }

            __instance.SmeltableItemList = filteredItemList;

            if (__instance.SmeltableItemList.Count == 0)
            {
                __instance.CurrentSelectedItem = null;
            }
        }
    }
}
