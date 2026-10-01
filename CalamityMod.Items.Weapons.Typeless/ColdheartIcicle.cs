using System;
using CalamityMod.NPCs.Other;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

[LegacyName(new string[] { "AlicornonaStick" })]
public class ColdheartIcicle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 50;
		base.Item.holdStyle = 6;
		base.Item.value = 0;
		base.Item.rare = ModContent.RarityType<CalamityRed>();
	}

	public override void HoldStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		player.itemRotation = ((player.direction == -1) ? (-(float)Math.PI / 4f) : ((float)Math.PI / 4f));
		player.itemLocation = player.GetBackHandPosition(Player.CompositeArmStretchAmount.Full, (float)player.direction * ((float)Math.PI / 4f)) + Vector2.UnitX * (float)player.direction * 14f;
	}

	public override void HoldItemFrame(Player player)
	{
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, (float)(-player.direction) * ((float)Math.PI / 4f));
	}

	public override void HoldItem(Player player)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		int offset = ((player.direction == 1) ? 5 : (-base.Item.width - 5));
		Rectangle itemRect = default(Rectangle);
		((Rectangle)(ref itemRect))._002Ector((int)player.Center.X + offset, (int)player.position.Y - 10, base.Item.width, base.Item.height);
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.dontTakeDamage && npc.type != ModContent.NPCType<THELORDE>() && ((Rectangle)(ref itemRect)).Intersects(npc.getRect()))
			{
				int damage = npc.SimpleStrikeNPC(npc.lifeMax / 200, player.direction, crit: true);
				SoundStyle style = CnidarianJellyfishOnTheString.SlapSound with
				{
					Volume = 2f,
					MaxInstances = 200
				};
				SoundEngine.PlaySound(in style, npc.Center);
				if (!Main.dedServ)
				{
					BloodShed(itemRect, npc, damage, player);
				}
			}
		}
	}

	public void BloodShed(Rectangle hitBox, NPC target, int damage, Player player)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		float damageInterpolant = Utils.GetLerpValue(950f, 2000f, damage, clamped: true);
		Vector2 impactPoint = Vector2.Lerp(((Rectangle)(ref hitBox)).Center.ToVector2(), target.Center, 0.65f);
		Vector2 bloodSpawnPosition = target.Center + Main.rand.NextVector2Circular(target.width, target.height) * 0.04f;
		Vector2 splatterDirection = Utils.SafeNormalize(new Vector2(bloodSpawnPosition.X * (float)player.direction, bloodSpawnPosition.Y), Vector2.UnitY);
		if (target.Organic())
		{
			for (int i = 0; i < 16; i++)
			{
				int bloodLifetime = Main.rand.Next(22, 36);
				float bloodScale = Main.rand.NextFloat(0.6f, 0.8f);
				Color bloodColor = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat());
				bloodColor = Color.Lerp(bloodColor, new Color(51, 22, 94), Main.rand.NextFloat(0.65f));
				if (Main.rand.NextBool(20))
				{
					bloodScale *= 2f;
				}
				Vector2 bloodVelocity = splatterDirection.RotatedByRandom(0.8100000023841858) * Main.rand.NextFloat(11f, 23f);
				bloodVelocity.Y -= 12f;
				GeneralParticleHandler.SpawnParticle(new BloodParticle(bloodSpawnPosition, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
			}
			for (int j = 0; j < 9; j++)
			{
				float bloodScale2 = Main.rand.NextFloat(0.2f, 0.33f);
				Color bloodColor2 = Color.Lerp(Color.Red, Color.DarkRed, Main.rand.NextFloat(0.5f, 1f));
				Vector2 bloodVelocity2 = splatterDirection.RotatedByRandom(0.8999999761581421) * Main.rand.NextFloat(9f, 14.5f);
				GeneralParticleHandler.SpawnParticle(new BloodParticle2(bloodSpawnPosition, bloodVelocity2, 20, bloodScale2, bloodColor2));
			}
			return;
		}
		for (int k = 0; k < 16; k++)
		{
			int sparkLifetime = Main.rand.Next(22, 36);
			float sparkScale = Main.rand.NextFloat(0.8f, 1f) + damageInterpolant * 0.85f;
			Color sparkColor = Color.Lerp(Color.Silver, Color.Gold, Main.rand.NextFloat(0.7f));
			sparkColor = Color.Lerp(sparkColor, Color.Orange, Main.rand.NextFloat());
			if (Main.rand.NextBool(10))
			{
				sparkScale *= 2f;
			}
			Vector2 sparkVelocity = splatterDirection.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(12f, 25f);
			sparkVelocity.Y -= 6f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(impactPoint, sparkVelocity, affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
		}
	}
}
