using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BeamingBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 120;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		if (base.Projectile.ai[0] == 1f)
		{
			tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/BeamingThornBlossom", (AssetRequestMode)2).Value;
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void AI()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		for (int dust = 0; dust < 2; dust++)
		{
			int randomDust = Utils.SelectRandom<int>(Main.rand, 164, 58, 204);
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, randomDust, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 6; k++)
		{
			int randomDust = Utils.SelectRandom<int>(Main.rand, 164, 58, 204);
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, randomDust, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < 8; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 8f - (MathHelper.ToRadians(67.5f) - base.Projectile.velocity.ToRotation())).ToRotationVector2() * 4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<BeamingBolt2>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0]);
			}
		}
		SoundEngine.PlaySound(in SoundID.Item105, base.Projectile.Center);
	}
}
