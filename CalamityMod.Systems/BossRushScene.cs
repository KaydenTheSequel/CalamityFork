using System.Collections.Generic;
using CalamityMod.Events;
using CalamityMod.Skies;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class BossRushScene : ModSceneEffect
{
	public override int Music => BossRushEvent.MusicToPlay;

	public override SceneEffectPriority Priority => SceneEffectPriority.BossHigh;

	public override bool IsSceneEffectActive(Player player)
	{
		return BossRushSky.DetermineDrawEligibility();
	}

	public override void SpecialVisuals(Player player, bool isActive)
	{
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		if (isActive)
		{
			Dictionary<string, CustomSky> skies = ((EffectManager<CustomSky>)SkyManager.Instance)._effects;
			bool updateRequired = false;
			foreach (string skyName in skies.Keys)
			{
				if (skies[skyName].IsActive() && skyName != "CalamityMod:BossRush")
				{
					skies[skyName].Opacity = 0f;
					skies[skyName].Deactivate();
					updateRequired = true;
				}
			}
			if (updateRequired)
			{
				SkyManager.Instance.Update(new GameTime());
			}
		}
		if (SkyManager.Instance["CalamityMod:BossRush"] != null && isActive != SkyManager.Instance["CalamityMod:BossRush"].IsActive())
		{
			if (isActive)
			{
				SkyManager.Instance.Activate("CalamityMod:BossRush", default(Vector2));
			}
			else
			{
				SkyManager.Instance.Deactivate("CalamityMod:BossRush");
			}
		}
	}

	public override float GetWeight(Player player)
	{
		return 1f;
	}
}
