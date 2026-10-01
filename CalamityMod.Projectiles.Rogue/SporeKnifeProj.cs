using System;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SporeKnifeProj : ModProjectile, ILocalizedModType, IModType
{
	public static int spinTime = 270;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/SporeKnife";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.aiStyle = 2;
		base.Projectile.timeLeft = 300;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		float minScale = 0.5f;
		float maxScale = 0.8f;
		int dust = Dust.NewDust(base.Projectile.position - new Vector2(10f, 10f), 30, 30, 44, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), Main.rand.NextFloat(minScale, maxScale));
		Main.dust[dust].noGravity = true;
		if (base.Projectile.timeLeft < spinTime)
		{
			base.Projectile.rotation += 0.4f * (float)base.Projectile.direction;
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(20, 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(20, 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			SoundEngine.PlaySound(in SporeKnife.StealthImpactSound, base.Projectile.Center);
		}
		else
		{
			SoundEngine.PlaySound(in SporeKnife.ImpactSound, base.Projectile.Center);
		}
		for (int i = 0; i < 18; i++)
		{
			Vector2 smokeVel = Main.rand.NextVector2Unit() * Main.rand.NextVector2Circular(20f, 20f);
			Color smokeColor = (Main.rand.NextBool() ? Color.DarkOliveGreen : Color.ForestGreen);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, smokeVel, smokeColor, Color.Black, Main.rand.NextFloat(0.9f, 1.6f), 200 - Main.rand.Next(60), 0.08f));
		}
		for (int k = 0; k < 11; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 44, Utils.RotatedByRandom(new Vector2(6f, 6f), 6.2831854820251465) * Main.rand.NextFloat(4f, 9f), 0, default(Color), Main.rand.NextFloat(0.4f, 0.6f));
			dust.noGravity = false;
			dust.alpha = Main.rand.Next(100, 121);
		}
		for (int j = 0; j < 11; j++)
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 15, Utils.RotatedByRandom(new Vector2(6f, 6f), 6.2831854820251465) * Main.rand.NextFloat(0.5f, 0.8f), 0, Color.GreenYellow, Main.rand.NextFloat(0.6f, 0.8f));
			dust2.noGravity = false;
			dust2.alpha = Main.rand.Next(100, 121);
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.ForestGreen * 0.8f, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.04f, 0.09f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Green * 0.8f, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.04f, 0.07f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.3f);
			base.Projectile.penetrate = -1;
			base.Projectile.ExpandHitboxBy(120);
			base.Projectile.usesLocalNPCImmunity = true;
			base.Projectile.localNPCHitCooldown = 10;
			base.Projectile.Damage();
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			for (int l = 0; l < 3; l++)
			{
				float baseDirectionRotation = Main.rand.NextFloat((float)Math.PI * 2f);
				Vector2 shootVelocity = ((float)Math.PI * 2f * (float)l / 15f + baseDirectionRotation).ToRotationVector2() * 9f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity, ModContent.ProjectileType<SporeKnifeBud>(), (int)((float)base.Projectile.damage * 0.5f), 0f, base.Projectile.owner);
			}
		}
	}
}
