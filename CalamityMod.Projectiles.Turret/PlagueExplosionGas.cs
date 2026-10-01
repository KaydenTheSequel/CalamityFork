using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class PlagueExplosionGas : ModProjectile, IAdditiveDrawer, ILocalizedModType, IModType
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
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		base.Projectile.scale = 0.5f + base.Projectile.localAI[0] * 0.01f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		if (base.Projectile.timeLeft < 40)
		{
			ableToHit = false;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 60);
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
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] != 0f)
		{
			Texture2D texture = TextureAssets.Projectile[base.Type].Value;
			Vector2 origin = texture.Size() * 0.5f;
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
			float opacity = 1f;
			opacity *= (float)base.Projectile.timeLeft / base.Projectile.localAI[1];
			Color drawColor = new Color(55, 125, 11) * opacity;
			Vector2 scale = base.Projectile.Size / texture.Size() * base.Projectile.scale * 1.35f;
			spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, drawColor, randomRotation1, origin, scale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(texture, drawPosition, (Rectangle?)null, drawColor, randomRotation2, origin, scale, (SpriteEffects)0, 0f);
		}
	}
}
