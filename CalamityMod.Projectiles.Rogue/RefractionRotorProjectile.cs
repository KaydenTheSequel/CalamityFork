using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RefractionRotorProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const int EnergyShotCount = 6;

	public const int StealthEnergyShotCount = 4;

	private static float RotationIncrement = 0.5f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/RefractionRotor";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 16;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 142;
		base.Projectile.height = 126;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		float spin = ((base.Projectile.direction <= 0) ? (-0.8f) : 0.8f);
		base.Projectile.rotation += spin * RotationIncrement;
		base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 18, 0, 255);
		if (base.Projectile.timeLeft == 80)
		{
			OnhitGrind(spin);
		}
		base.Projectile.StickyProjAI(80, findNewNPC: true);
		if (base.Projectile.Calamity().stealthStrike)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 450f, 24f, 30f);
		}
	}

	private void OnhitGrind(float spinDir)
	{
		base.Projectile.rotation += spinDir * RotationIncrement * 0.8f;
		base.Projectile.StickyProjAI(12, findNewNPC: true);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 80)
		{
			base.Projectile.timeLeft = 80;
		}
		base.Projectile.ModifyHitNPCSticky(10);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		if (base.Projectile.soundDelay == 0)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/SwiftSlice");
			style.Volume = 0.7f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.soundDelay = 10;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			int slash = Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center, base.Projectile.velocity * 0.1f, ModContent.ProjectileType<RefractionRotorSlashCreator>(), base.Projectile.damage, 0f, base.Projectile.owner, target.whoAmI, base.Projectile.velocity.ToRotation());
			if (Main.projectile.IndexInRange(slash))
			{
				Main.projectile[slash].timeLeft = 20;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.color = Main.hslToRgb((float)i / 80f, 0.9f, 0.6f);
				dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(3f, 5.5f);
				dust.scale = Main.rand.NextFloat(1.4f, 2.4f);
				dust.fadeIn = Main.rand.NextFloat(0.8f, 1.6f);
				dust.noGravity = true;
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.DarkRed, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.2f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.SkyBlue, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.1f, 21, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.DarkGreen, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.15f, 19, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		int shootType = ModContent.ProjectileType<PrismShurikenBlade>();
		if (Main.myPlayer == base.Projectile.owner && Main.LocalPlayer.ownedProjectileCounts[shootType] <= 24)
		{
			int energyDamage = (int)((float)base.Projectile.damage * 0.5f);
			int shotAmt = (base.Projectile.Calamity().stealthStrike ? 4 : 6);
			float baseDirectionRotation = Main.rand.NextFloat((float)Math.PI * 2f);
			for (int j = 0; j < shotAmt; j++)
			{
				Vector2 shootVelocity = ((float)Math.PI * 2f * (float)j / (float)shotAmt + baseDirectionRotation).ToRotationVector2() * 9f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + shootVelocity, shootVelocity, shootType, energyDamage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/RefractionRotorGlowmask", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(position: base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, origin: value.Size() * 0.5f, texture: value, sourceRectangle: null, color: base.Projectile.GetAlpha(Color.White), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		Asset<Texture2D> p = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearSmokey", (AssetRequestMode)2);
		Asset<Texture2D> p2 = ModContent.Request<Texture2D>("CalamityMod/Particles/SemiCircularSmearSwipe", (AssetRequestMode)2);
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		Texture2D value2 = p2.Value;
		Color val = (Main.rand.NextBool() ? Color.YellowGreen : Color.Goldenrod);
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(value2, generalDrawPos, null, val * 0.55f, base.Projectile.rotation * Main.rand.NextFloat(1.6f, 1.7f), p2.Size() * 0.5f, (Main.rand.NextBool() ? 1.6f : 1.4f) * Main.rand.NextFloat(0.8f, 1.15f), (SpriteEffects)0);
		Texture2D value3 = p.Value;
		val = (Main.rand.NextBool() ? Color.OrangeRed : Color.CornflowerBlue);
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(value3, generalDrawPos, null, val * 0.75f, base.Projectile.rotation * Main.rand.NextFloat(1.2f, 1.3f), p.Size() * 0.5f, Main.rand.NextBool() ? 1.4f : 1.2f, (SpriteEffects)0);
	}
}
