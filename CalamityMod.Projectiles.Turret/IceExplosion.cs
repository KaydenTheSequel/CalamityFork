using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class IceExplosion : ModProjectile, IAdditiveDrawer, ILocalizedModType, IModType
{
	public bool ableToHit = true;

	public float randomRotation1 = Main.rand.NextFloat(0f, (float)Math.PI * 2f);

	public float randomRotation2 = Main.rand.NextFloat(0f, (float)Math.PI * 2f);

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/Summon/SmallAresArms/MinionPlasmaGas";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 184);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 90;
		base.Projectile.hide = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.localAI[1] = base.Projectile.timeLeft;
		base.Projectile.ArmorPenetration = 10;
	}

	public override bool PreAI()
	{
		if (base.Projectile.knockBack == 0f)
		{
			base.Projectile.hostile = true;
		}
		else
		{
			base.Projectile.friendly = true;
		}
		return true;
	}

	public override void AI()
	{
		base.Projectile.localAI[0]++;
		base.Projectile.scale = 0.5f + base.Projectile.localAI[0] * 0.01f;
		if (base.Projectile.timeLeft < 30)
		{
			ableToHit = false;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
		target.AddBuff(324, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(324, 180);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, base.Projectile.scale * 92f, targetHitbox);
	}

	public override bool? CanDamage()
	{
		if (!ableToHit)
		{
			return false;
		}
		return null;
	}

	public void AdditiveDraw(SpriteBatch spriteBatch)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] != 0f)
		{
			Texture2D texture = TextureAssets.Projectile[base.Type].Value;
			Vector2 origin = texture.Size() * 0.5f;
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
			float opacity = 0.9f;
			opacity *= (float)base.Projectile.timeLeft / base.Projectile.localAI[1];
			Color drawColor = new Color(118, 217, 222) * opacity;
			Vector2 scale = base.Projectile.Size / texture.Size() * base.Projectile.scale * 1.35f;
			spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, drawColor, randomRotation1, origin, scale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, drawColor, randomRotation2, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
