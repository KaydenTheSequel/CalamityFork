using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using CalamityMod.Tiles.Abyss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ApoctolithShard : ModProjectile, ILocalizedModType, IModType
{
	public int TimeBeforeHoming = 30;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Rogue/AbyssalMirrorProjectile";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 13;
		base.Projectile.height = 13;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.scale = Main.rand.NextFloat(0.7f, 1.2f);
		base.Projectile.timeLeft = 240;
		TimeBeforeHoming = Main.rand.Next(30, 60);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.ai[1] < (float)TimeBeforeHoming)
		{
			return false;
		}
		return base.CanDamage();
	}

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] < (float)TimeBeforeHoming)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.94f;
		}
		else
		{
			base.Projectile.ai[2] = MathHelper.Lerp(base.Projectile.ai[2], 10f, 0.05f);
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 400f, base.Projectile.ai[2], 0.2f);
			base.Projectile.velocity.Y += 0.25f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - base.Projectile.velocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - base.Projectile.velocity.Y;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Rectangle fr = tex.Frame(1, 3, 0, base.Projectile.frame);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, 2, Color.Lerp(ApoctolithProj.HighBlueColor, Color.Transparent, 0.8f), 1, tex.Value);
		float a = Math.Clamp(MathHelper.Lerp(255f, 0f, base.Projectile.ai[1] / (float)TimeBeforeHoming), 0f, 1f);
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, fr, ApoctolithProj.HighBlueColor.MultiplyRGBA(new Color(a, a, a, 0f)), base.Projectile.rotation, new Vector2((float)(fr.Width / 2), (float)(fr.Height / 2)), 1.35f, (SpriteEffects)0);
		return base.PreDraw(ref lightColor);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in AbyssGravel.MineSound, base.Projectile.position);
		for (int splash = 0; splash < 4; splash++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, (0f - base.Projectile.velocity.X) * 0.15f, (0f - base.Projectile.velocity.Y) * 0.1f, 150, default(Color), 0.9f);
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ApoctolithProj.LowBlueColor, "CalamityMod/Particles/LargeBloom", Vector2.One, 0f, 0.3f, 0f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", Vector2.One, 0f, 0.15f, 0f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 5; i++)
		{
			GeneralParticleHandler.SpawnParticle(new BloodParticle2(base.Projectile.Center, Utils.RotatedBy(new Vector2(Main.rand.NextFloat(6f, 12f), 0f), (double)Main.rand.NextFloat((float)Math.PI * 2f), default(Vector2)), 12, Main.rand.NextFloat(0.02f, 0.1f), ApoctolithProj.HighBlueColor));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 120);
	}
}
