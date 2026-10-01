using System;
using CalamityMod.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class GladiatorHealOrb : ModProjectile, ILocalizedModType, IModType
{
	public int target = -1;

	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float heal => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 4800;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = MathHelper.Lerp(heal / 8f, 1f, 0.65f);
		float maxDistance = 150f;
		if (target < 0)
		{
			PassiveBehavior();
			for (int playerIndex = 0; playerIndex < 255; playerIndex++)
			{
				Player obj = Main.player[playerIndex];
				if (obj.lifeMagnet)
				{
					maxDistance = 225f;
				}
				float targetDist = Vector2.Distance(obj.Center, base.Projectile.Center);
				if (targetDist < maxDistance)
				{
					maxDistance = targetDist;
					target = playerIndex;
				}
			}
		}
		else
		{
			HealHome();
		}
	}

	public void PassiveBehavior()
	{
		float maxYVelocity = 2f;
		base.Projectile.StickToTiles(ignorePlatforms: false, stickToEverything: false);
		base.Projectile.velocity.X *= 0.99f;
		if (base.Projectile.velocity.Y < maxYVelocity)
		{
			base.Projectile.velocity.Y += 0.02f;
		}
		if (base.Projectile.velocity.Y > maxYVelocity)
		{
			base.Projectile.velocity.Y = maxYVelocity;
		}
	}

	public void HealHome()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[target];
		Vector2 playerVector = player.Center - base.Projectile.Center;
		if (((Vector2)(ref playerVector)).Length() < 50f && base.Projectile.position.X < player.position.X + (float)player.width && base.Projectile.position.X + (float)base.Projectile.width > player.position.X && base.Projectile.position.Y < player.position.Y + (float)player.height && base.Projectile.position.Y + (float)base.Projectile.height > player.position.Y)
		{
			player.HealPlayer((int)heal, HealTextType.Local);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/OrbHeal", 5);
			style.Volume = 0.15f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			NetMessage.SendData(66, -1, -1, null, target, heal);
			base.Projectile.Kill();
		}
		base.Projectile.velocity = playerVector.SafeNormalize(Vector2.UnitY) * 3.5f + player.velocity / 4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		Texture2D lightTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SmallGreyscaleCircle", (AssetRequestMode)2).Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float colorInterpolation = (float)Math.Cos((float)base.Projectile.timeLeft / 32f + Main.GlobalTimeWrappedHourly / 20f + (float)i / (float)base.Projectile.oldPos.Length * (float)Math.PI) * 0.5f + 0.5f;
			Color color = Color.Lerp(Color.LightSeaGreen, Color.LimeGreen, colorInterpolation) * 0.4f;
			((Color)(ref color)).A = 0;
			Vector2 drawPosition = base.Projectile.oldPos[i] + lightTexture.Size() * 0.5f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY) + new Vector2(-32.5f, -32.5f);
			Color outerColor = color;
			Color innerColor = color * 0.5f;
			float intensity = 0.9f + 0.15f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 60f * ((float)Math.PI * 2f));
			intensity *= MathHelper.Lerp(0.15f, 1f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			if (base.Projectile.timeLeft <= 60)
			{
				intensity *= (float)base.Projectile.timeLeft / 60f;
			}
			Vector2 outerScale = new Vector2(1f) * base.Projectile.scale * intensity;
			Vector2 innerScale = new Vector2(1f) * base.Projectile.scale * intensity * 0.7f;
			outerColor *= intensity;
			innerColor *= intensity;
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, outerColor, 0f, lightTexture.Size() * 0.5f, outerScale * 0.25f, (SpriteEffects)0);
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, innerColor, 0f, lightTexture.Size() * 0.5f, innerScale * 0.25f, (SpriteEffects)0);
		}
		return false;
	}
}
