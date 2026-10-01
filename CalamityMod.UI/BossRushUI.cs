using System.Linq;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI;

public class BossRushUI : InvasionProgressUI
{
	public override int SecondaryDigitPrecision => 1;

	public override bool IsActive => BossRushEvent.BossRushActive;

	public override float CompletionRatio
	{
		get
		{
			float invasionBasedCompletion = (float)BossRushEvent.BossRushStage / (float)BossRushEvent.Bosses.Count;
			if (!CalamityPlayer.areThereAnyDamnBosses || !BossRushEvent.Bosses.IndexInRange(BossRushEvent.BossRushStage))
			{
				return invasionBasedCompletion;
			}
			int bossIndex = NPC.FindFirstNPC(BossRushEvent.CurrentlyFoughtBoss);
			if (!Main.npc.IndexInRange(bossIndex))
			{
				return invasionBasedCompletion;
			}
			NPC currentBoss = Main.npc[bossIndex];
			float bossBasedCompletion = 1f - (float)currentBoss.life / (float)currentBoss.lifeMax;
			return invasionBasedCompletion + bossBasedCompletion / (float)BossRushEvent.Bosses.Count;
		}
	}

	public override string InvasionName => CalamityUtils.GetTextValue("Events.BossRush");

	public override Color InvasionBarColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.DarkSlateBlue;
		}
	}

	public override Texture2D IconTexture => ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/BossRushIcon", (AssetRequestMode)2).Value;

	public static float EvaluationLifeRatioFromNPCTypes(params int[] types)
	{
		int totalLife = 0;
		int totalLifeMax = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (types.Contains(n.type))
			{
				totalLife += n.life;
				totalLifeMax += n.lifeMax;
			}
		}
		if (totalLifeMax != 0)
		{
			return (float)totalLife / (float)totalLifeMax;
		}
		return 0f;
	}
}
