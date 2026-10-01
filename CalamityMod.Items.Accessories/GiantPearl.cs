using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class GiantPearl : ModItem, ILocalizedModType, IModType
{
	public const float AuraRadius = 120f;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().giantPearl = true;
		Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.45f, 0.8f, 0.8f);
		if (Main.myPlayer != player.whoAmI)
		{
			return;
		}
		float npcDistCompare = 1000f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (!n.friendly && !n.dontTakeDamage)
			{
				float currentNPCDist = Vector2.Distance(n.Center, player.Center);
				if (currentNPCDist < npcDistCompare)
				{
					npcDistCompare = currentNPCDist;
				}
			}
		}
		float opacity = Utils.Remap(npcDistCompare, 120f, 600f, 1f, 0f);
		if (opacity >= 1f)
		{
			for (int d = 0; d < 2; d++)
			{
				GeneralParticleHandler.SpawnParticle(new WaterGlobParticle(player.Center, Main.rand.NextVector2CircularEdge(13.5f, 13.5f), 0.32f, 0f, 35));
			}
		}
		GeneralParticleHandler.SpawnParticle(new SemiCircularSmearFade(player.Center, player.velocity, new Color(75, 164, 191) * opacity, (float)player.miscCounter * ((float)Math.PI / 30f), 1.35f, Vector2.One, 2, playerCentered: true));
		GeneralParticleHandler.SpawnParticle(new SemiCircularSmearFade(player.Center, player.velocity, new Color(75, 164, 191) * opacity, (float)player.miscCounter * ((float)Math.PI / 30f) + (float)Math.PI, 1.35f, Vector2.One, 2, playerCentered: true));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(player.Center, player.velocity, new Color(75, 164, 191), "CalamityMod/Particles/BloomRing", Vector2.One, 0f, 1.26f, 1.26f, 2, UseAdditiveBlend: true, opacity, fade: false, 1f, (SpriteEffects)0));
	}
}
