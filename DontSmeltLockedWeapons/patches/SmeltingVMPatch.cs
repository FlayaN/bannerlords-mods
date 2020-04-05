using HarmonyLib;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection.Craft.Smelting;
using TaleWorlds.Library;

namespace DontSmeltLockedWeapons.patches
{
    [HarmonyPatch(typeof(SmeltingVM), "RefreshList")]
    public class SmeltingVMPatch
    {
        static void Postfix(SmeltingVM __instance)
        {
            var locks = Campaign.Current.GetCampaignBehavior<InventoryLockTracker>().GetLocks();

            var items = MobileParty.MainParty.ItemRoster;

            var filteredItemList = new MBBindingList<SmeltingItemVM>();

            foreach(var smeltableItem in __instance.SmeltableItemList)
            {
                var inventoryItem = items.FirstOrDefault(x => x.EquipmentElement.Item.Id == smeltableItem.Item.Id);
                if(!inventoryItem.IsEmpty && !locks.Contains(inventoryItem))
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
