using System;
using System.Collections.Generic;
using CalamityMod.Items.Placeables.Furniture;
using CalamityMod.Items.Tools;
using CalamityMod.NPCs.NormalNPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class WulfrumLureSignal : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public static List<int> LureSpawnPool => new List<int>
	{
		ModContent.NPCType<WulfrumDrone>(),
		ModContent.NPCType<WulfrumGyrator>(),
		ModContent.NPCType<WulfrumHovercraft>(),
		ModContent.NPCType<WulfrumRover>()
	};

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = WulfrumLureItem.SignalTime;
	}

	public override void AI()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time % (float)WulfrumLureItem.SpawnIntervals == 0f)
		{
			WulfrumLureItem.MaxEnemiesPerWave = 5;
			int enemiesToSpawn = Main.rand.Next(1, WulfrumLureItem.MaxEnemiesPerWave);
			Player player = Main.LocalPlayer;
			Vector2 val;
			if (Main.netMode == 1)
			{
				val = Main.LocalPlayer.Center - base.Projectile.Center;
				float closestPlayerDistance = ((Vector2)(ref val)).Length();
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player plr = enumerator.Current;
					val = plr.Center - base.Projectile.Center;
					float newDistance = ((Vector2)(ref val)).Length();
					if (newDistance < closestPlayerDistance)
					{
						closestPlayerDistance = newDistance;
						player = plr;
					}
				}
			}
			val = player.Center - base.Projectile.Center;
			if (((Vector2)(ref val)).Length() > 3500f)
			{
				return;
			}
			for (int i = 0; i < enemiesToSpawn; i++)
			{
				int tries = 0;
				Vector2 spawnPosition;
				do
				{
					Vector2 displacey = Main.rand.NextVector2Unit();
					if (displacey.Y > 0f)
					{
						displacey.Y *= -1f;
					}
					spawnPosition = player.Center + displacey * Main.rand.NextFloat(600f, 1015f) * new Vector2(1.5f, 1f);
					if (spawnPosition.Y > player.Center.Y)
					{
						spawnPosition.Y = player.Center.Y;
					}
					if (tries > 500)
					{
						break;
					}
					tries++;
				}
				while (WorldGen.SolidTile(CalamityUtils.ParanoidTileRetrieval((int)spawnPosition.X / 16, (int)spawnPosition.Y / 16)));
				if (tries < 500)
				{
					int npcToSpawn = LureSpawnPool[Main.rand.Next(LureSpawnPool.Count)];
					NPC.NewNPC(base.Projectile.GetSource_FromAI(), (int)spawnPosition.X, (int)spawnPosition.Y, npcToSpawn, 0, 0f, 0f, 0f, 0f, player.whoAmI);
					for (int iy = 0; iy < 16; iy++)
					{
						Dust.NewDustPerfect(spawnPosition + Main.rand.NextVector2Circular(1f, 1f) * 20f, 226, Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(1f, 2.3f) - Vector2.UnitY * 6f).noGravity = true;
					}
				}
			}
		}
		if (Time % 2f == 0f && CalamityUtils.IntoMorseCode("perimeter breached", Time / (float)WulfrumLureItem.SignalTime))
		{
			float dustCount = (float)Math.PI * 75f;
			for (int j = 0; (float)j < dustCount; j++)
			{
				float angle = (float)Math.PI * 2f * (float)j / dustCount;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 229);
				dust.position = base.Projectile.Center + angle.ToRotationVector2() * 300f;
				dust.scale = 0.7f;
				dust.noGravity = true;
				dust.velocity = base.Projectile.velocity;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in WulfrumTreasurePinger.RechargeBeepSound, base.Projectile.Center);
	}
}
