using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGDeath : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 OldVelocity;

	public const float TelegraphTotalTime = 35f;

	public const float TelegraphFadeTime = 5f;

	public const float TelegraphWidth = 2400f;

	public const float FadeTime = 20f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public float TelegraphDelay
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.Opacity = 0f;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		writer.WriteVector2(OldVelocity);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		OldVelocity = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (TelegraphDelay > 35f)
		{
			if ((float)base.Projectile.timeLeft < 20f)
			{
				base.Projectile.Opacity = (float)base.Projectile.timeLeft / 20f;
			}
			else
			{
				if (base.Projectile.Opacity < 1f)
				{
					base.Projectile.Opacity += 0.05f;
				}
				if (base.Projectile.Opacity > 1f)
				{
					base.Projectile.Opacity = 1f;
				}
			}
			if (OldVelocity != Vector2.Zero)
			{
				base.Projectile.velocity = OldVelocity;
				OldVelocity = Vector2.Zero;
				base.Projectile.netUpdate = true;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
		else if (OldVelocity == Vector2.Zero)
		{
			OldVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.netUpdate = true;
			base.Projectile.rotation = OldVelocity.ToRotation() + (float)Math.PI / 2f;
		}
		TelegraphDelay++;
	}

	public override bool CanHitPlayer(Player target)
	{
		if (TelegraphDelay > 35f)
		{
			return base.Projectile.Opacity == 1f;
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && TelegraphDelay > 35f && base.Projectile.Opacity == 1f)
		{
			target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, 0) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		if (TelegraphDelay >= 35f)
		{
			return true;
		}
		if (Main.zenithWorld)
		{
			return false;
		}
		Texture2D laserTelegraph = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/LaserWallTelegraphBeam", (AssetRequestMode)2).Value;
		float yScale = 2f;
		if (TelegraphDelay < 5f)
		{
			yScale = MathHelper.Lerp(0f, 2f, TelegraphDelay / 5f);
		}
		if (TelegraphDelay > 30f)
		{
			yScale = MathHelper.Lerp(2f, 0f, (TelegraphDelay - 30f) / 5f);
		}
		Vector2 scaleInner = default(Vector2);
		((Vector2)(ref scaleInner))._002Ector(2400f / (float)laserTelegraph.Width, yScale);
		Vector2 origin = laserTelegraph.Size() * new Vector2(0f, 0.5f);
		Vector2 scaleOuter = scaleInner * new Vector2(1f, 1.6f);
		Color colorOuter = Color.Lerp(Color.Cyan, Color.Purple, TelegraphDelay / 35f * 2f % 1f);
		Color colorInner = Color.Lerp(colorOuter, Color.White, 0.75f);
		colorOuter *= 0.7f;
		colorInner *= 0.7f;
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorInner, OldVelocity.ToRotation(), origin, scaleInner, (SpriteEffects)0);
		Main.EntitySpriteDraw(laserTelegraph, base.Projectile.Center - Main.screenPosition, null, colorOuter, OldVelocity.ToRotation(), origin, scaleOuter, (SpriteEffects)0);
		return false;
	}
}
