using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DoGWeaponTeleportRift : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public ref float GeneralRotationalOffset => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 170);
		base.Projectile.scale = 0.25f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.Opacity = 0f;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 45;
		base.Projectile.netImportant = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = base.Projectile.ai[1];
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[0] = 45f;
		}
		GeneralRotationalOffset += (float)Math.PI / 8f;
		base.Projectile.Opacity = MathHelper.Clamp(GeneralRotationalOffset, 0f, 1f);
		if (base.Projectile.Opacity == 1f && Main.rand.NextBool(15))
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 0, 0, 267, 0f, 0f, 100, new Color(150, 100, 255, 255));
			dust.velocity.X = 0f;
			dust.noGravity = true;
			dust.position = base.Projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(4f, 20f);
			dust.fadeIn = 1f;
			dust.scale = 0.5f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		float currentFade = Utils.GetLerpValue(0f, 5f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(base.Projectile.ai[0], base.Projectile.ai[0] - 5f, base.Projectile.timeLeft, clamped: true);
		currentFade *= (1f + 0.2f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 30f * (float)Math.PI * 3f)) * 0.8f;
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Color baseColor = new Color(150, 100, 255, 255) * base.Projectile.Opacity;
		baseColor *= 0.5f;
		((Color)(ref baseColor)).A = 0;
		Color colorA = baseColor;
		Color colorB = baseColor * 0.5f;
		colorA *= currentFade;
		colorB *= currentFade;
		Vector2 origin = value.Size() / 2f;
		Vector2 scale = new Vector2(3f, 7f) * base.Projectile.Opacity * base.Projectile.scale * currentFade;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI / 2f, origin, scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorA, 0f, origin, scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI / 2f, origin, scale * 0.8f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorB, 0f, origin, scale * 0.8f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI / 2f + GeneralRotationalOffset * 0.25f, origin, scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorA, GeneralRotationalOffset * 0.25f, origin, scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI / 2f + GeneralRotationalOffset * 0.5f, origin, scale * 0.8f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorB, GeneralRotationalOffset * 0.5f, origin, scale * 0.8f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI / 4f, origin, scale * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI * 3f / 4f, origin, scale * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI / 4f, origin, scale * 0.4f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI * 3f / 4f, origin, scale * 0.4f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI / 4f + GeneralRotationalOffset * 0.75f, origin, scale * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorA, (float)Math.PI * 3f / 4f + GeneralRotationalOffset * 0.75f, origin, scale * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI / 4f + GeneralRotationalOffset, origin, scale * 0.4f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, colorB, (float)Math.PI * 3f / 4f + GeneralRotationalOffset, origin, scale * 0.4f, spriteEffects);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
