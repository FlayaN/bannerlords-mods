using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace TestMod
{
    public class Startup : MBSubModuleBase
    {
        private Game? CurrentGame => Game.Current;

        private BasicCharacterObject? CurrentPlayer => CurrentGame?.PlayerTroop;

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            //PerkHelper.AddFeatBonusForPerson(DefaultFeats.Cultural.SturgianSnowAgility, party.Leader, ref bonuses);
            //bonuses.AddToStat(feat.IncrementType, feat.EffectBonus, _textFeatBonuses);

            ExplainedNumber withutPerk = new ExplainedNumber(100.0f);
            withutPerk.AddFactor(-0.1f, new TextObject("snow"));
            withutPerk.LimitMin(1f);

            ExplainedNumber withPerk = new ExplainedNumber(100.0f);
            withPerk.AddFactor(-0.1f, new TextObject("snow"));
            withPerk.AddFactor(0.2f * 0.01f, new TextObject("{=snSBfQkV}Feats"));
            withPerk.LimitMin(1f);

            //string testVal = CurrentPlayer.Age.ToString();

            Module.CurrentModule.AddInitialStateOption(
                new InitialStateOption(
                    id: "message",
                    name: new TextObject("Trigger TestMod action"),
                    orderIndex: 9990,
                    action: () => {
                        InformationManager.DisplayMessage(new InformationMessage($"Without perk: {withutPerk.ResultNumber}, with perk: {withPerk.ResultNumber}"));
                    },
                    isDisabled: false
                )
            );

            //if (CurrentGame != null)
            //{
            //    ExplainedNumber bonuses = new ExplainedNumber(1.0f);
            //    //PerkHelper.AddFeatBonusForPerson(DefaultFeats.Cultural.SturgianSnowAgility, party.Leader, ref bonuses);
            //    //bonuses.AddToStat(feat.IncrementType, feat.EffectBonus, _textFeatBonuses);

            //    bonuses.AddFactor(-0.1f, new TextObject("snow"));

            //    bonuses.AddFactor(0.2f * 0.01f, new TextObject("{=snSBfQkV}Feats"));
            //    bonuses.LimitMin(1f);

            //    string testVal = bonuses.ResultNumber.ToString();//CurrentPlayer.Age;

            //    Module.CurrentModule.AddInitialStateOption(
            //        new InitialStateOption(
            //            id: "message",
            //            name: new TextObject("Trigger TestMod action: " + testVal),
            //            orderIndex: 9990,
            //            action: () => {
            //                InformationManager.DisplayMessage(new InformationMessage(CurrentGame.IsDevelopmentMode ? "dev" : "prod"));
            //            },
            //            isDisabled: false
            //        )
            //    );
            //}
        }
    }
}
