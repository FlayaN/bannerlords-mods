using BalanceChanges.Behaviors;
using BalanceChanges.Models;
using SandBox;
using SandBox.TournamentMissions.Missions;
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

            if(gameStarter is CampaignGameStarter campaignGameStarter)
            {
                campaignGameStarter.AddModel(new CustomSmithingModel());
                
                //campaignGameStarter.AddBehavior();
            }
        }

        public override void OnMissionBehaviourInitialize(Mission mission)
        {
            base.OnMissionBehaviourInitialize(mission);

            if (
                !mission.HasMissionBehaviour<CustomTournamentGameBehaviour>() &&
                (
                    mission.HasMissionBehaviour<TournamentFightMissionController>() ||
                    mission.HasMissionBehaviour<TournamentArcheryMissionController>() ||
                    mission.HasMissionBehaviour<TournamentJoustingMissionController>() ||
                    mission.HasMissionBehaviour<TownHorseRaceMissionController>())
                )
            {
                mission.AddMissionBehaviour(new CustomTournamentGameBehaviour());
            }
        }
    }
}
