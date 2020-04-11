using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BalanceChanges
{
    public class Startup : MBSubModuleBase
    {
        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            base.OnGameStart(game, gameStarter);

            if(!(game.GameType is Campaign))
            {
                return;
            }

            if (gameStarter is CampaignGameStarter campaignGameStarter)
            {
                //campaignGameStarter.
                //campaignGameStarter.AddBehavior();
            }
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            base.OnMissionBehaviourInitialize(mission);
        }
    }
}
