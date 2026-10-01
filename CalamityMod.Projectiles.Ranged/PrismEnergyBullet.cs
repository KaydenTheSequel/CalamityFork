using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PrismEnergyBullet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float CurrentLaserLength => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public override void SetDefaults()
	{
		base.Projectile.scale = 1.7f;
		base.Projectile.width = (base.Projectile.height = 12);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 13;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 300)
		{
			SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		}
		CurrentLaserLength = (int)MathHelper.Lerp(1f, 70f, Utils.GetLerpValue(0f, 15f, Time, clamped: true) * Utils.GetLerpValue(0f, 15f, base.Projectile.timeLeft, clamped: true));
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 shootVelocity = base.Projectile.velocity.RotatedBy(MathHelper.Lerp(-0.3f, 0.3f, (float)i / 2f)) * 0.4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shootVelocity, ModContent.ProjectileType<PrismComet>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = texture.Size() * 0.5f;
		Vector2 currentDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		for (int i = 0; (float)i < CurrentLaserLength; i++)
		{
			float scale = MathHelper.Lerp(1f, 0.2f, (float)i / CurrentLaserLength) * base.Projectile.scale;
			Vector2 drawPosition = base.Projectile.Center - currentDirection * (float)i * 4f - Main.screenPosition;
			Color drawColor = Color.Lerp(Color.Lime, Color.YellowGreen, (float)Math.Cos((float)i / CurrentLaserLength * 2.1f - Main.GlobalTimeWrappedHourly * 2.5f) * 0.5f + 0.5f);
			drawColor = Color.Lerp(drawColor, Color.Yellow, 0.55f);
			drawColor = Color.Lerp(drawColor, Color.White, (float)Math.Pow((float)i / CurrentLaserLength, 3.0));
			((Color)(ref drawColor)).A = 0;
			Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, base.Projectile.rotation, origin, scale, (SpriteEffects)0);
		}
		return false;
	}
}
