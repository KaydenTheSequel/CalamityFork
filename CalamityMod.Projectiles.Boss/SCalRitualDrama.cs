using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Particles;
using CalamityMod.Skies;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class SCalRitualDrama : ModProjectile, ILocalizedModType, IModType
{
	public const int TotalRitualTime = 270;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 690;
	}

	public override void AI()
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 689)
		{
			for (int i = 0; i < 2; i++)
			{
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Red, Color.Magenta, 0.3f), 0f, 0.55f, 270, fade: false));
			}
			GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.White, 0f, 0.5f, 270, fade: false));
		}
		if (base.Projectile.timeLeft == 509)
		{
			GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, new Color(121, 21, 77), 0f, 0.85f, 90, fade: false));
		}
		if (!NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()))
		{
			SCalSky.OverridingIntensity = Utils.GetLerpValue(90f, 245f, Time, clamped: true);
			Main.LocalPlayer.Calamity().GeneralScreenShakePower = Utils.GetLerpValue(90f, 245f, Time, clamped: true);
			Main.LocalPlayer.Calamity().GeneralScreenShakePower *= Utils.GetLerpValue(3400f, 1560f, Main.LocalPlayer.Distance(base.Projectile.Center), clamped: true) * 4f;
		}
		if (Time == 269f)
		{
			SummonSCal();
		}
		if (Time >= 270f)
		{
			if (Main.netMode != 1 && !NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()))
			{
				base.Projectile.Kill();
			}
			return;
		}
		int fireReleaseRate = ((!(Time > 150f)) ? 1 : 2);
		for (int j = 0; j < fireReleaseRate; j++)
		{
			if (Main.rand.NextBool())
			{
				float variance = Main.rand.NextFloat(-25f, 25f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + new Vector2(variance, 20f), 267);
				dust.scale = Main.rand.NextFloat(0.35f, 1.2f);
				dust.color = (Color)(Main.rand.NextBool() ? Color.Red : new Color(121, 21, 77));
				dust.fadeIn = 0.7f;
				dust.velocity = -Vector2.UnitY.RotatedBy(variance * 0.02f) * Main.rand.NextFloat(1.1f, 2.1f) * (Time * 0.023f);
				dust.noGravity = true;
			}
		}
		Time++;
	}

	public void SummonSCal()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spawnPosition = base.Projectile.Center - new Vector2(53f, 39f);
		if (Main.netMode != 1)
		{
			NPC scal = CalamityUtils.SpawnBossBetter(spawnPosition, ModContent.NPCType<SupremeCalamitas>());
			if (base.Projectile.ai[1] == 1f)
			{
				scal.ModNPC<SupremeCalamitas>().permafrost = true;
			}
		}
		SoundEngine.PlaySound((base.Projectile.ai[1] == 1f) ? Cryogen.DeathSound : SupremeCalamitas.SpawnSound, base.Projectile.Center);
		Main.LocalPlayer.SetScreenshake(Utils.GetLerpValue(3400f, 1560f, Main.LocalPlayer.Distance(base.Projectile.Center), clamped: true) * 16f);
		for (int i = 0; i < 90; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (base.Projectile.ai[1] == 1f) ? 161 : 235, Utils.RotatedByRandom(new Vector2(30f, 30f), 100.0) * Main.rand.NextFloat(0.05f, 1.2f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.2f, 2.3f);
		}
		for (int j = 0; j < 40; j++)
		{
			Vector2 sparkVel = Utils.RotatedByRandom(new Vector2(20f, 20f), 100.0) * Main.rand.NextFloat(0.1f, 1.1f);
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + sparkVel * 2f, sparkVel, affectedByGravity: false, 120, Main.rand.NextFloat(1.55f, 2.75f), (base.Projectile.ai[1] == 1f) ? Color.Cyan : Color.Red, AddativeBlend: true, needed: true));
		}
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, (base.Projectile.ai[1] == 1f) ? Color.Cyan : Color.Red, new Vector2(2f, 2f), 0f, 0f, 2.7f, 60));
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, (Color)((base.Projectile.ai[1] == 1f) ? Color.Cyan : new Color(121, 21, 77)), new Vector2(2f, 2f), 0f, 0f, 2.1f, 60));
	}
}
