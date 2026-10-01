using System;
using CalamityMod.Events;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class OverlyDramaticDukeSummoner : ModProjectile, ILocalizedModType, IModType
{
	private Vector2 cen;

	public SlotId? SoundId;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/Boss/OldDukeVortex";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 408);
		base.Projectile.scale = 0.004f;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 1800;
	}

	private static void ExpandVertically(int startX, int startY, out int topY, out int bottomY, int maxExpandUp = 100, int maxExpandDown = 100)
	{
		topY = startY;
		bottomY = startY;
		if (!WorldGen.InWorld(startX, startY, 10))
		{
			return;
		}
		for (int yUp = 0; yUp < maxExpandUp; yUp++)
		{
			if (topY <= 0)
			{
				break;
			}
			if (topY < 10)
			{
				break;
			}
			if (!(Main.tile[startX, topY] != null))
			{
				break;
			}
			topY--;
		}
		for (int yDown = 0; yDown < maxExpandDown; yDown++)
		{
			if (bottomY >= Main.maxTilesY - 10)
			{
				break;
			}
			if (bottomY > Main.maxTilesY - 10)
			{
				break;
			}
			if (Main.tile[startX, bottomY] == null)
			{
				break;
			}
			bottomY++;
		}
	}

	public override void AI()
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0758: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 2 && !SoundId.HasValue)
		{
			SoundStyle style = OldDukeVortex.SpawnSound with
			{
				IsLooped = true,
				MaxInstances = 20
			};
			SoundId = SoundEngine.PlaySound(in style, base.Projectile.Center, (ActiveSound _) => new ProjectileAudioTracker(base.Projectile).IsActiveAndInGame());
		}
		if (base.Projectile.ai[0] == 0f)
		{
			cen = base.Projectile.Center;
		}
		base.Projectile.rotation -= 0.15f * (float)(1.0 - (double)base.Projectile.alpha / 255.0) * (base.Projectile.ai[0] / 660f);
		base.Projectile.ai[0]++;
		base.Projectile.ai[1]++;
		Vector2 vec = new Vector2(408f, 408f) * base.Projectile.scale;
		base.Projectile.position = cen - new Vector2((float)Math.Sqrt(vec.X), (float)Math.Sqrt(vec.Y));
		_ = 1600f * base.Projectile.scale / 16f;
		base.Projectile.Center.ToTileCoordinates();
		Vector2 top = base.Projectile.Top;
		Vector2 bottomVector = base.Projectile.Bottom;
		Vector2.Lerp(top, bottomVector, 0.5f);
		base.Projectile.width = (int)(208f * base.Projectile.scale);
		Vector2 ProjectileSpawnPosition = cen;
		if (base.Projectile.ai[0] < 90f)
		{
			base.Projectile.alpha = (int)MathHelper.Lerp(255f, 0f, base.Projectile.ai[0] / 90f);
		}
		if (base.Projectile.ai[0] < 600f)
		{
			base.Projectile.scale = MathHelper.Lerp(0.004f, 1.6f, base.Projectile.ai[0] / 660f);
			Vector2 vec2 = base.Projectile.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(320f, 540f) * base.Projectile.scale, 0f), 6.2831854820251465);
			GeneralParticleHandler.SpawnParticle(new SparkParticle(vec2, (base.Projectile.Center - vec2) / 20f, affectedByGravity: false, 10, Main.rand.NextFloat(0.5f, 1f), Color.LimeGreen, fadeIn: true));
		}
		if (base.Projectile.ai[0] % 10f == 1f && base.Projectile.ai[0] < 600f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(cen, Vector2.Zero, new Color(55, 195, 0, 20), "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), base.Projectile.scale * 0.9f, base.Projectile.scale * 0.4f, 40, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		float maxdist = 1200f;
		if (base.Projectile.ai[0] < 480f && base.Projectile.ai[0] > 90f)
		{
			if (base.Projectile.ai[0] % 10f == 9f)
			{
				Vector2 velocity = Utils.RotatedByRandom(new Vector2(0f, -18f), 0.699999988079071);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), ProjectileSpawnPosition, velocity, ModContent.ProjectileType<OldDukeSummonDrop>(), 65, 2f);
			}
			if (base.Projectile.ai[0] % 35f == 34f)
			{
				Vector2 velocity2 = Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(-3f, 3f), -7f - Main.rand.NextFloat(4f, 12f)), 0.5);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), ProjectileSpawnPosition, velocity2, ModContent.ProjectileType<OldDukeGore>(), 65, 2f);
			}
		}
		if (base.Projectile.ai[0] >= 600f)
		{
			bool canSpawnBoomer = false;
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player player = enumerator.Current;
				if (!player.dead && base.Projectile.Distance(player.Center) < 12000f)
				{
					canSpawnBoomer = true;
					break;
				}
			}
			if (base.Projectile.ai[0] == 600f)
			{
				if (canSpawnBoomer)
				{
					SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath.WithPitchOffset(0.5f), cen);
					SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact.WithPitchOffset(-0.5f), cen);
					for (float i = 0f; i <= 5f; i++)
					{
						if (i == 5f)
						{
							GeneralParticleHandler.SpawnParticle(new CustomPulse(cen, Vector2.Zero, new Color(55, 255, 0), "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, i * 0.1f, 40, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
						}
						GeneralParticleHandler.SpawnParticle(new CustomPulse(cen, Vector2.Zero, new Color(55, 255, 0), "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, i * 0.1f, 40, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					}
					if (Main.netMode != 1)
					{
						int boomer = NPC.NewNPC(base.Projectile.GetSource_FromThis(), (int)ProjectileSpawnPosition.X, (int)ProjectileSpawnPosition.Y, ModContent.NPCType<OldDuke>());
						string boomerName = Main.npc[boomer].TypeName;
						if (Main.netMode == 0)
						{
							Main.NewText((object)Language.GetTextValue("Announcement.HasAwoken", boomerName), (Color?)new Color(175, 75, 255));
							return;
						}
						if (Main.dedServ)
						{
							ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", Main.npc[boomer].GetTypeNetName()), new Color(175, 75, 255));
							return;
						}
						CalamityUtils.BossAwakenMessage(boomer);
						Main.npc[boomer].velocity = Vector2.UnitY * -12f;
						Main.npc[boomer].alpha = 255;
						Main.npc[boomer].Calamity().newAI[3] = 1f;
						Main.npc[boomer].netUpdate = true;
						AcidRainEvent.HasTriedToSummonOldDuke = true;
						AcidRainEvent.OldDukeHasBeenEncountered = true;
						AcidRainEvent.UpdateInvasion(win: false);
					}
				}
				else
				{
					AcidRainEvent.AccumulatedKillPoints = 0;
					AcidRainEvent.HasTriedToSummonOldDuke = false;
					AcidRainEvent.UpdateInvasion(win: false);
				}
			}
			if (base.Projectile.ai[0] >= 600f)
			{
				base.Projectile.alpha = (int)MathHelper.Lerp(0f, 255f, MathHelper.Clamp((base.Projectile.ai[0] - 600f) / 30f, 0f, 1f));
				base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, 0f, MathHelper.Clamp((base.Projectile.ai[0] - 600f) / 30f, 0f, 1f));
			}
		}
		if (base.Projectile.ai[0] >= 720f)
		{
			base.Projectile.Kill();
		}
		if (SoundId.HasValue && SoundEngine.TryGetActiveSound(SoundId.Value, out ActiveSound Sound) && Sound.IsPlaying)
		{
			Sound.Position = base.Projectile.Center;
			Sound.Volume = base.Projectile.scale * 2f;
			Sound.Pitch = MathHelper.Lerp(0f, -1f, MathHelper.Clamp((base.Projectile.Distance(Main.LocalPlayer.Center) - 800f) / maxdist, 0f, 1f) + (0f - base.Projectile.scale + 1f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> Tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		float sc = MathHelper.Lerp(1f, 0f, base.Projectile.localAI[2]);
		float alphaLerp = MathHelper.Lerp(1f, 0f, (float)base.Projectile.alpha / 255f);
		Main.EntitySpriteDraw(Tex.Value, cen - Main.screenPosition, Tex.Frame(), Utils.MultiplyRGBA(new Color(0f, 0f, 0f, 0.4f), new Color(alphaLerp, alphaLerp, alphaLerp, alphaLerp)), (0f - base.Projectile.rotation) / 2f * 5f, Tex.Frame().Center(), 1.61f * base.Projectile.scale * sc, (SpriteEffects)0);
		for (int i = 2; i >= 0; i--)
		{
			float lerp = (float)i / 3f;
			Main.EntitySpriteDraw(Tex.Value, cen - Main.screenPosition, Tex.Frame(), Color.Lerp(new Color(5, 155, 95, 100), new Color(255, 255, 255, 55), lerp).MultiplyRGBA(new Color(alphaLerp, alphaLerp, alphaLerp, alphaLerp)), (0f - base.Projectile.rotation) / 2f * (float)(i + 1), Tex.Frame().Center(), MathHelper.Lerp(1f, 1.7f, lerp) * base.Projectile.scale * sc, (SpriteEffects)0);
		}
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		return false;
	}
}
