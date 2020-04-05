using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;

namespace BalanceChanges.Models
{
    public class CustomSmithingModel : DefaultSmithingModel
    {
        public override int GetEnergyCostForRefining(ref Crafting.RefiningFormula refineFormula, Hero hero)
        {
            int num = 2;
            if(hero.GetPerkValue(DefaultPerks.Crafting.PracticalRefiner))
            {
                num = 1;
            }
            return num;
        }

        public override int GetEnergyCostForSmelting(ItemObject item, Hero hero)
        {
            int num = 4;
            if(hero.GetPerkValue(DefaultPerks.Crafting.PracticalSmelter))
            {
                num = 2;
            }
            return num;
        }
    }
}
