using System;
using CalamityMod.DataStructures;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TerratomereExplosion : ModProjectile, IAdditiveDrawer, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 520);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 150;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 14;
		base.Projectile.scale = 0.2f;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundStyle style = SubsumingVortex.ExplosionSound with
			{
				Volume = 0.6f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.localAI[0] = 1f;
		}
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3() * 1.5f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 5 == 4)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= 17)
		{
			base.Projectile.Kill();
		}
		base.Projectile.scale *= 1.013f;
		base.Projectile.Opacity = Utils.GetLerpValue(5f, 36f, base.Projectile.timeLeft, clamped: true);
	}

	public void AdditiveDraw(SpriteBatch spriteBatch)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Texture2D lightTexture = ModContent.Request<Texture2D>("CalamityMod/Skies/XerocLight", (AssetRequestMode)2).Value;
		Rectangle frame = texture.Frame(3, 6, base.Projectile.frame / 6, base.Projectile.frame % 6);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = frame.Size() * 0.5f;
		for (int i = 0; i < 2; i++)
		{
			Vector2 lightDrawPosition = drawPosition + ((float)Math.PI * 2f * (float)i / 36f + Main.GlobalTimeWrappedHourly * 5f).ToRotationVector2() * base.Projectile.scale * 12f;
			Color lightBurstColor = CalamityUtils.MulticolorLerp((float)base.Projectile.timeLeft / 144f, Terratomere.TerraColor1, Terratomere.TerraColor2);
			lightBurstColor = Color.Lerp(lightBurstColor, Color.White, 0.4f) * base.Projectile.Opacity * 0.24f;
			Main.spriteBatch.Draw(lightTexture, lightDrawPosition, (Rectangle?)null, lightBurstColor, 0f, lightTexture.Size() * 0.5f, base.Projectile.scale * 1.32f, (SpriteEffects)0, 0f);
		}
		if (base.Projectile.timeLeft < 149)
		{
			Main.spriteBatch.Draw(texture, drawPosition, (Rectangle?)frame, Color.White, 0f, origin, 1.6f, (SpriteEffects)0, 0f);
		}
	}
}
