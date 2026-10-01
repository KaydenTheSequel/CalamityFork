using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CosmicSpiritBomb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 150;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		float scaleModd = (float)(int)Main.mouseTextColor / 200f - 0.35f;
		scaleModd *= 0.2f;
		base.Projectile.scale = scaleModd + 0.95f;
		Vector2 val = base.Projectile.Center - Main.player[base.Projectile.owner].Center;
		float projDistance = ((Vector2)(ref val)).Length() / 100f;
		projDistance = ((!(projDistance <= 2f)) ? (projDistance * 1.33f) : 1f);
		base.Projectile.velocity = Vector2.Normalize(Main.player[base.Projectile.owner].Center - base.Projectile.Center) * projDistance;
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		int buffType = -1;
		float num = base.Projectile.ai[0];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					buffType = 69;
				}
			}
			else
			{
				buffType = 323;
			}
		}
		else
		{
			buffType = 324;
		}
		target.AddBuff(buffType, 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		base.Projectile.ExpandHitboxBy(200);
		int dustType = -1;
		float num = base.Projectile.ai[0];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					dustType = 244;
				}
			}
			else
			{
				dustType = 73;
			}
		}
		else
		{
			dustType = 15;
		}
		for (int k = 0; k < 10; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.oldVelocity.X * 2.5f, base.Projectile.oldVelocity.Y * 2.5f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		string textureName = "";
		float num = base.Projectile.ai[0];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					textureName = "CalamityMod/Projectiles/Melee/CosmicSpiritBomb3";
				}
			}
			else
			{
				textureName = "CalamityMod/Projectiles/Melee/CosmicSpiritBomb2";
			}
		}
		else
		{
			textureName = Texture;
		}
		Texture2D realTexture = ModContent.Request<Texture2D>(textureName, (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(realTexture, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, realTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
