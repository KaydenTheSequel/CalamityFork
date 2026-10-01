using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TauCannonPortal : ModProjectile, ILocalizedModType, IModType
{
	public Color color1;

	public Color color2;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/ExtraTextures/GreyscaleVortex";

	private float Scale01
	{
		get
		{
			if (base.Projectile.timeLeft > 360)
			{
				return Utils.GetLerpValue(420f, 360f, base.Projectile.timeLeft, clamped: true);
			}
			if (base.Projectile.timeLeft >= 60 && base.Projectile.timeLeft <= 360)
			{
				return 1f;
			}
			if (base.Projectile.timeLeft < 60)
			{
				return Utils.GetLerpValue(0f, 60f, base.Projectile.timeLeft, clamped: true);
			}
			return 0f;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.width = (base.Projectile.height = 100);
		base.Projectile.timeLeft = 420;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5 * base.Projectile.MaxUpdates;
		base.Projectile.extraUpdates = 2;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 100f * Scale01, targetHitbox);
	}

	public override void AI()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += MathHelper.ToRadians(1.5f) * Scale01;
		if (base.Projectile.timeLeft < 300)
		{
			Vector2 moveTotarget = (Main.player[base.Projectile.owner].Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 15f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += moveTotarget * 0.05f;
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.98f;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= 0.08f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			int randomBoltAmount = Main.rand.Next(9, 13);
			float starterAngle = Main.rand.NextFloat((float)Math.PI * 2f);
			for (int i = 0; i < randomBoltAmount; i++)
			{
				Vector2 velocity = (starterAngle + (float)Math.PI * 2f / (float)randomBoltAmount * (float)i).ToRotationVector2() * 14f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<TauCannonBolt>(), (int)((float)base.Projectile.damage * 0.65f), base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, 5f);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 anchorPoint = texture.Size() * 0.5f;
		for (int i = 0; i < 13; i++)
		{
			Color val = Color.Lerp(color1, Color.White, (float)i * 0.075f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(texture, drawPosition, null, val * Scale01 * 0.45f, base.Projectile.rotation * 3f - (float)i * 0.15f, anchorPoint, MathHelper.Clamp(Scale01 * 0.375f - (float)i * 0.02f, 0f, 5f), (SpriteEffects)0);
		}
		return false;
	}

	public TauCannonPortal()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		color1 = Color.Coral;
		color2 = Color.MediumTurquoise;
		base._002Ector();
	}
}
