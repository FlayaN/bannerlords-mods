using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment.Managers;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BalanceChanges.Behaviors
{
    public class CustomTournamentGameBehaviour : MissionLogic
    {
        //private readonly BattleAgentLogic _battleAgentLogic;
        //public CustomTournamentGameBehaviour()
        //{
        //    _battleAgentLogic = new BattleAgentLogic();
        //}

        public override void OnScoreHit(Agent? affectedAgent, Agent? affectorAgent, int affectorWeaponKind, bool isBlocked, float damage, float movementSpeedDamageModifier, float hitDistance, AgentAttackType attackType, float shotDifficulty, int weaponCurrentUsageIndex)
        {
            if (affectorAgent != null && affectorAgent.Character != null && affectedAgent?.Character != null)
            {
                if (damage > affectedAgent.HealthLimit)
                {
                    damage = affectedAgent.HealthLimit;
                }
                float num = damage / affectedAgent.HealthLimit;
                EnemyHitReward(affectedAgent, affectorAgent, movementSpeedDamageModifier, shotDifficulty, affectorWeaponKind, 0.5f * num, weaponCurrentUsageIndex, damage);
            }
            //_battleAgentLogic.OnScoreHit(affectedAgent, affectorAgent, affectorWeaponKind, isBlocked, damage, movementSpeedDamageModifier, hitDistance, attackType, shotDifficulty, weaponCurrentUsageIndex);

            //base.OnScoreHit(affectedAgent, affectorAgent, affectorWeaponKind, isBlocked, damage, movementSpeedDamageModifier, hitDistance, attackType, shotDifficulty, weaponCurrentUsageIndex);

            //var test = new BattleAgentLogic();


            //if (affectorAgent?.Character != null && affectedAgent?.Character != null)
            //{
            //    if (damage > affectedAgent.HealthLimit)
            //    {
            //        damage = affectedAgent.HealthLimit;
            //    }
            //    float num = damage / affectedAgent.HealthLimit;

            //    var test = new BattleAgentLogic();

            //    test.OnScoreHit(affectedAgent, affectorAgent, affectorWeaponKind, isBlocked, damage, movementSpeedDamageModifier, hitDistance, attackType, shotDifficulty, weaponCurrentUsageIndex);


            //    //EnemyHitReward(affectedAgent, affectorAgent, movementSpeedDamageModifier, shotDifficulty, affectorWeaponKind, 0.5f * num, weaponCurrentUsageIndex, damage);
            //}
        }

        //// copied from BattleAgentLogic.EnemyHitReward
        private void EnemyHitReward(Agent affectedAgent, Agent affectorAgent, float lastSpeedBonus, float lastShotDifficulty, int lastWeaponKind, float hitpointRatio, int weaponUsageIndex, float damageAmount)
        {
            if (MBNetwork.IsClient)
            {
                return;
            }
            CharacterObject affectedCharacter = (CharacterObject)affectedAgent.Character;
            CharacterObject characterObject = (CharacterObject)affectorAgent.Character;
            if (affectedAgent.Origin == null || affectorAgent == null || affectorAgent.Origin == null)
            {
                return;
            }
            Hero? captain = GetCaptain(affectorAgent);
            Hero? hero = (affectorAgent.Team.Leader != null && affectorAgent.Team.Leader.Character.IsHero) ? ((CharacterObject)affectorAgent.Team.Leader.Character).HeroObject : null;
            SkillLevelingManager.OnCombatHit(characterObject, affectedCharacter, captain, hero, lastSpeedBonus, lastShotDifficulty, lastWeaponKind, hitpointRatio, isSimulatedHit: false, affectorAgent.MountAgent != null, affectorAgent.Team == affectedAgent.Team, hero != null && affectorAgent.Character != hero.CharacterObject && (hero != Hero.MainHero || affectorAgent.Formation == null || !affectorAgent.Formation.IsAIControlled), weaponUsageIndex, damageAmount, affectedAgent.Health < 1f);
        }

        //// copied from BattleAgentLogic.GetCaptain
        private static Hero? GetCaptain(Agent affectorAgent)
        {
            Hero? result = null;
            if (affectorAgent.Formation != null)
            {
                Agent captain = affectorAgent.Formation.Captain;
                if (captain != null)
                {
                    float captainRadius = Campaign.Current.Models.CombatXpModel.CaptainRadius;
                    if (captain.Position.Distance(affectorAgent.Position) < captainRadius)
                    {
                        result = ((CharacterObject)captain.Character).HeroObject;
                    }
                }
            }
            return result;
        }
    }
}
