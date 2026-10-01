using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class BlightFlames : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public bool postTileHit;

	public bool postEnemyHit;

	public NPC targeted;

	public Vector2 savedDist;

	public Color FogColor;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/BlightFlames";

	public ref float ScaleFactor => ref base.Projectile.ai[0];

	public ref float LightPower => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 400;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 55;
	}

	public override void AI()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.rotation += Main.rand.NextFloat(0.2f, 0.9f);
		if (Time > 6 && Time < 540 && Main.rand.NextBool(2 + Time / 7))
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center + Main.rand.NextVector2Circular(10f + (float)Time * 0.5f, 10f + (float)Time * 0.5f), Vector2.Zero, Main.rand.NextBool(3) ? Color.LimeGreen : Color.Green, new Vector2(1f, 1f), 0f, Main.rand.NextFloat(0.03f, 0.09f) + (float)Time * 0.00055f, 0f, 25));
		}
		if (Time > 6 && Time < 150 && !postTileHit && !postEnemyHit && Main.rand.NextBool(3 + Time / 7))
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Main.rand.NextVector2Circular(5f + (float)Time * 0.2f, 5f + (float)Time * 0.2f), -base.Projectile.velocity * 0.05f, Main.rand.NextBool(3) ? Color.LimeGreen : Color.Lime, Color.Black, Main.rand.NextFloat(0.3f, 0.8f) + (float)Time * 0.013f, 160f));
		}
		Color newColor;
		if (Time == 8)
		{
			for (int i = 0; i <= 8; i++)
			{
				Vector2 center = base.Projectile.Center;
				Vector2? velocity = base.Projectile.velocity * 0.6f;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(center, 91, velocity, 0, newColor);
				dust.scale = Main.rand.NextFloat(1.1f, 1.9f);
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.3f, 2.1f);
				dust.noGravity = true;
				dust.color = (Main.rand.NextBool() ? Color.Chartreuse : Color.LimeGreen);
				dust.noLight = true;
				dust.alpha = 90;
			}
		}
		if (Time == 10)
		{
			for (int j = 0; j <= 3; j++)
			{
				Vector2 center2 = base.Projectile.Center;
				Vector2? velocity2 = base.Projectile.velocity * 0.6f;
				newColor = default(Color);
				Dust dust2 = Dust.NewDustPerfect(center2, 220, velocity2, 0, newColor);
				dust2.scale = Main.rand.NextFloat(0.4f, 1.2f);
				dust2.velocity = base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.3f, 2.1f);
				dust2.noGravity = false;
			}
		}
		if (Main.rand.NextBool(4) && Time > 135 && !postTileHit && !postEnemyHit)
		{
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(145f, 145f);
			Vector2? velocity3 = Vector2.Zero;
			newColor = default(Color);
			Dust dust3 = Dust.NewDustPerfect(position, 220, velocity3, 0, newColor);
			dust3.scale = Main.rand.NextFloat(0.2f, 0.4f);
			dust3.noGravity = true;
		}
		if (Main.rand.NextBool(20) && (postTileHit || postEnemyHit))
		{
			Vector2 position2 = base.Projectile.Center + Main.rand.NextVector2Circular(145f, 145f);
			Vector2? velocity4 = Vector2.Zero;
			newColor = default(Color);
			Dust dust4 = Dust.NewDustPerfect(position2, 220, velocity4, 0, newColor);
			dust4.scale = Main.rand.NextFloat(0.4f, 1.2f);
			dust4.noGravity = true;
		}
		ScaleFactor += 0.0061f;
		ScaleFactor = MathHelper.Clamp(ScaleFactor, 0f, base.Projectile.scale * 0.8f);
		Lighting.AddLight(base.Projectile.Center, new Vector3(1f, 1f, 0.25f) * ScaleFactor);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.99f;
		base.Projectile.Opacity = Utils.GetLerpValue(30f, 50f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(0f, 130f, base.Projectile.timeLeft, clamped: true);
		if (postEnemyHit && !postTileHit && targeted != null && targeted.life > 0 && targeted.active)
		{
			base.Projectile.Center = targeted.Center + savedDist;
			savedDist *= 0.99f;
		}
		if (!Main.dedServ)
		{
			newColor = Lighting.GetColor((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16 + 6);
			Vector3 val = ((Color)(ref newColor)).ToVector3();
			float lightPowerBelow = ((Vector3)(ref val)).Length() / (float)Math.Sqrt(3.0);
			LightPower = MathHelper.Lerp(LightPower, lightPowerBelow, 0.15f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Plague>(), 1200);
		for (int i = 0; i <= 3; i++)
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Main.rand.NextVector2Circular(5f + (float)Time * 0.2f, 5f + (float)Time * 0.2f), Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(2f, 6f), Main.rand.NextFloat(2f, 6f)), 60.0), Main.rand.NextBool(3) ? Color.LimeGreen : Color.Lime, Color.Black, Main.rand.NextFloat(1.2f, 2.3f), 140f));
		}
		if (!postTileHit && !postEnemyHit)
		{
			SoundEngine.PlaySound(in BlightSpewer.Nanomachines, base.Projectile.Center);
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.timeLeft = 300;
			postEnemyHit = true;
			targeted = target;
			savedDist = base.Projectile.Center - target.Center;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.95f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		if (!postEnemyHit)
		{
			base.Projectile.velocity = oldVelocity * 0.95f;
			Projectile projectile = base.Projectile;
			projectile.Center -= base.Projectile.velocity;
			if (!postTileHit)
			{
				base.Projectile.timeLeft = 800;
				postTileHit = true;
			}
		}
		return false;
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = (int)Utils.Remap(Time, 0f, 90f, 10f, 95f);
		((Rectangle)(ref hitbox)).Inflate(size, size);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		_ = ModContent.Request<Texture2D>("CalamityMod/Particles/WaterFoam", (AssetRequestMode)2).Value;
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float opacity = base.Projectile.Opacity * 0.3f;
		Color fogColor = FogColor;
		((Color)(ref fogColor)).A = 0;
		Color drawColor = fogColor * opacity;
		Main.EntitySpriteDraw(texture, drawPosition + Main.rand.NextVector2Circular(19f, 19f), null, drawColor * 0.55f, base.Projectile.rotation, texture.Size() * 0.5f, ScaleFactor * 1.2f, (SpriteEffects)0);
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, (0f - base.Projectile.rotation) * 0.9f, texture.Size() * 0.5f, ScaleFactor, (SpriteEffects)0);
		return false;
	}

	public BlightFlames()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		FogColor = new Color(30, 255, 30);
		base._002Ector();
	}
}
