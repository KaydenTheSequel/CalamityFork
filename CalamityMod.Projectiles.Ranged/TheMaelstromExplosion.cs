using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TheMaelstromExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/ExtraTextures/SmallGreyscaleCircle";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 75;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.scale = 0.1f;
		base.Projectile.width = (base.Projectile.height = (int)(120f / base.Projectile.scale));
		base.Projectile.timeLeft = 60;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16;
	}

	public override void AI()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, 1f, 0.125f);
		base.Projectile.Opacity = base.Projectile.scale * Utils.GetLerpValue(0f, 30f, base.Projectile.timeLeft, clamped: true);
		if (!Main.dedServ)
		{
			int sparkLifetime = Main.rand.Next(22, 36);
			float sparkScale = Main.rand.NextFloat(1f, 1.3f);
			Color sparkColor = Color.Lerp(Color.Cyan, Color.DarkBlue, Main.rand.NextFloat(0.7f));
			Vector2 sparkVelocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(3f, 8f);
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
			Vector2 dustSpawnOffset = Main.rand.NextVector2Circular(base.Projectile.width, base.Projectile.height) * base.Projectile.scale * 0.4f;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustSpawnOffset, 267);
			dust.color = Color.Cyan;
			((Color)(ref dust.color)).A = 84;
			dust.scale *= Main.rand.NextFloat(0.7f, 1.2f);
			dust.velocity = dustSpawnOffset.SafeNormalize(Vector2.UnitY).RotatedBy(1.5707963705062866).RotatedByRandom(0.3700000047683716);
			dust.velocity *= Main.rand.NextFloat(2f, 6f);
			dust.noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = texture.Size() * 0.5f;
		float scale = base.Projectile.scale * (float)base.Projectile.width / (float)texture.Width;
		Color frontAfterimageColor = base.Projectile.GetAlpha(Color.Lerp(Color.Cyan, Color.DarkBlue, (float)base.Projectile.identity / 7f % 0.8f)) * 0.2f;
		((Color)(ref frontAfterimageColor)).A = 0;
		for (int i = 0; i < 12; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 12f + base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 2f;
			Vector2 afterimageDrawPosition = base.Projectile.Center + drawOffset - Main.screenPosition;
			Main.EntitySpriteDraw(texture, afterimageDrawPosition, null, frontAfterimageColor, base.Projectile.rotation, origin, scale, (SpriteEffects)0);
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Vector2 size = base.Projectile.Size;
		return CalamityUtils.CircularHitboxCollision(center, ((Vector2)(ref size)).Length() * base.Projectile.scale / 1.414f, targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.Opacity > 0.4f))
		{
			return false;
		}
		return null;
	}
}
