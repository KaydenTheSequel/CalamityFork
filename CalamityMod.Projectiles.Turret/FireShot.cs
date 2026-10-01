using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class FireShot : ModProjectile, ILocalizedModType, IModType
{
	public bool ableToHit = true;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 16;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 4;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 52;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
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
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/FlamethrowerTurret");
			style.Volume = 0.65f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		base.Projectile.localAI[0]++;
		base.Projectile.velocity.Y -= 0.065f;
		if (base.Projectile.timeLeft < 16)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0f;
			ableToHit = false;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 600);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 300);
	}

	public override bool? CanDamage()
	{
		if (!ableToHit)
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		float rotation = 0f;
		Texture2D lightTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleVortex", (AssetRequestMode)2).Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float colorInterpolation = (float)Math.Cos((float)(base.Projectile.timeLeft - 16) / 7f + (float)i / (float)base.Projectile.oldPos.Length * (float)Math.PI) * 0.35f + 0.65f;
			Color color = Color.Lerp(Color.Yellow, Color.OrangeRed, colorInterpolation) * 0.4f;
			((Color)(ref color)).A = 7;
			rotation += 0.35f;
			Vector2 drawPosition = base.Projectile.oldPos[i] + lightTexture.Size() * 0.5f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY) + new Vector2(-186f, -186f);
			float intensity = 0.6f;
			intensity *= MathHelper.Lerp(0.15f, 1f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			intensity *= 1.5f * (base.Projectile.localAI[0] / 40f + 0.0375f);
			if (base.Projectile.timeLeft < 22)
			{
				((Color)(ref color)).A = (byte)(((Color)(ref color)).A + 50);
			}
			if (base.Projectile.timeLeft < 16)
			{
				intensity *= (float)(base.Projectile.timeLeft / 15);
			}
			Vector2 scale = new Vector2(1f) * intensity;
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, color, rotation, lightTexture.Size() * 0.5f, scale * 0.25f, (SpriteEffects)0);
		}
		return false;
	}
}
