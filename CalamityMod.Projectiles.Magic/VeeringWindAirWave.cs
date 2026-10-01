using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VeeringWindAirWave : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 48);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 45;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3() * 0.3f);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((double)base.Projectile.damage * 0.5);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		Texture2D lightTexture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float colorInterpolation = (float)Math.Cos((float)base.Projectile.timeLeft / 2.4f + Main.GlobalTimeWrappedHourly / 20f + (float)i / (float)base.Projectile.oldPos.Length * (float)Math.PI) * 0.5f + 0.5f;
			Color color = Color.Lerp(Color.White, Color.LightGray, colorInterpolation);
			Color invertedColor = Color.Lerp(Color.LightGray, Color.Gray, 1f - colorInterpolation);
			((Color)(ref color)).A = 0;
			((Color)(ref invertedColor)).A = 0;
			new Vector2(-28f, -28f);
			Vector2 drawPosition = base.Projectile.oldPos[i] + lightTexture.Size() * 0.5f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
			Color outerColor = color;
			Color innerColor = invertedColor;
			float intensity = 0.9f + 0.15f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 60f * ((float)Math.PI * 2f));
			intensity *= MathHelper.Lerp(0.15f, 1f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			if (base.Projectile.timeLeft <= 15)
			{
				intensity *= (float)base.Projectile.timeLeft / 15f;
			}
			Vector2 outerScale = new Vector2(1f) * intensity;
			Vector2 innerScale = new Vector2(1f) * intensity;
			outerColor *= intensity * 0.5f;
			innerColor *= intensity * 0.5f;
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, outerColor, base.Projectile.rotation, lightTexture.Size() * 0.5f, outerScale, (SpriteEffects)0);
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, innerColor, base.Projectile.rotation, lightTexture.Size() * 0.5f, innerScale * 0.6f, (SpriteEffects)0);
		}
		return false;
	}
}
