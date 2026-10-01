using System;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class IgneousBladeStrike : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Summon/IgneousBlade";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
		base.Projectile.timeLeft = 360;
		base.Projectile.alpha = 127;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f + (float)Math.PI / 4f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < Main.rand.Next(28, 41); i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(10f), 6, Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 4f));
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D AllWhiteTexture = IgneousExaltation.GetBladeOutlineTex();
		Main.spriteBatch.Draw(AllWhiteTexture, base.Projectile.Center + new Vector2(0f, 2f) - Main.screenPosition, (Rectangle?)null, new Color(166, 46, 61), base.Projectile.rotation, AllWhiteTexture.Size() * 0.5f, base.Projectile.scale * 1f, (SpriteEffects)0, 1f);
		Main.spriteBatch.Draw(AllWhiteTexture, base.Projectile.Center + new Vector2(2f, 0f) - Main.screenPosition, (Rectangle?)null, new Color(166, 46, 61), base.Projectile.rotation, AllWhiteTexture.Size() * 0.5f, base.Projectile.scale * 1f, (SpriteEffects)0, 1f);
		Main.spriteBatch.Draw(AllWhiteTexture, base.Projectile.Center + new Vector2(0f, -2f) - Main.screenPosition, (Rectangle?)null, new Color(166, 46, 61), base.Projectile.rotation, AllWhiteTexture.Size() * 0.5f, base.Projectile.scale * 1f, (SpriteEffects)0, 1f);
		Main.spriteBatch.Draw(AllWhiteTexture, base.Projectile.Center + new Vector2(-2f, 0f) - Main.screenPosition, (Rectangle?)null, new Color(166, 46, 61), base.Projectile.rotation, AllWhiteTexture.Size() * 0.5f, base.Projectile.scale * 1f, (SpriteEffects)0, 1f);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.spriteBatch.Draw(tex, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, lightColor, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale * 1f, (SpriteEffects)0, 1f);
		return false;
	}
}
