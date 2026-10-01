using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class RedtideWhirlpool : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 40;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.97f;
		base.Projectile.rotation += 0.32724923f * (float)Math.Sign(base.Projectile.velocity.X);
		int dustCount = Main.rand.Next(4);
		float offset = Main.rand.NextFloat((float)Math.PI * 2f);
		for (int i = 0; i < dustCount; i++)
		{
			float angle = (float)i / (float)dustCount * ((float)Math.PI * 2f) + offset;
			Vector2 position = base.Projectile.Center + angle.ToRotationVector2() * 46f;
			Vector2? velocity = (angle - (float)Math.PI / 2f * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2() * 8f + base.Projectile.velocity;
			float scale = Main.rand.NextFloat(1.6f, 3f);
			Dust.NewDustPerfect(position, 176, velocity, 0, default(Color), scale).noGravity = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		SpriteEffects flip = (SpriteEffects)(Math.Sign(base.Projectile.velocity.X) >= 0);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, lightColor * 0.3f, base.Projectile.rotation * 1.2f, texture.Size() * 0.5f, 1.2f, flip);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, lightColor * 0.4f, base.Projectile.rotation, texture.Size() * 0.5f, 1.6f, flip);
		for (int i = 1; i < base.Projectile.oldPos.Length; i++)
		{
			float afterimageRot = base.Projectile.oldRot[i];
			Vector2 drawPos = base.Projectile.oldPos[i] + texture.Size() * 0.5f - Main.screenPosition;
			float intensity = MathHelper.Lerp(0.01f, 0.1f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			Main.EntitySpriteDraw(texture, drawPos, null, lightColor * intensity, afterimageRot, texture.Size() * 0.5f, 1.6f, flip);
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 56f, targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath19, base.Projectile.position);
		int dustCount = 36;
		for (int i = 0; i < dustCount; i++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (dustCount / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustCount) + base.Projectile.Center;
			Vector2 dustDirection = val - base.Projectile.Center;
			Dust dust = Dust.NewDustPerfect(val + dustDirection, 172, Vector2.Zero, 100, default(Color), 1.4f);
			dust.noGravity = true;
			dust.noLight = true;
			dust.velocity = dustDirection;
		}
	}
}
