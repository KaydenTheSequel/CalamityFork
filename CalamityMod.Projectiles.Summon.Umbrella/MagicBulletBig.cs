using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.Umbrella;

public class MagicBulletBig : ModProjectile, ILocalizedModType, IModType
{
	public VertexStrip TrailDrawer;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.extraUpdates = 12;
		base.Projectile.light = 0.5f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.alpha = 255;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.penetrate = 4;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.FinalExtraUpdate() && base.Projectile.ai[0]++ > 1f)
		{
			base.Projectile.alpha = 0;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (!target.betsysCurse)
		{
			target.AddBuff(203, 180);
		}
		if (!target.ichor)
		{
			target.AddBuff(69, 180);
		}
		if (target.Calamity().markedForDeath)
		{
			target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 180);
		}
		if (target.Calamity().armorCrunch)
		{
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 180);
		}
		if (target.Calamity().whisperingDeath)
		{
			target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 180);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(69, 180);
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 180);
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 180);
		target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 180);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha != 0)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame();
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Main.spriteBatch.Draw(texture, drawPosition, (Rectangle?)frame, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
		CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
