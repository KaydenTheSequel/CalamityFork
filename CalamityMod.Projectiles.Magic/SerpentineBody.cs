using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SerpentineBody : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0.55f / 255f, (float)(255 - base.Projectile.alpha) * 0.55f / 255f);
		Vector2 zeroing = Vector2.Zero;
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		int chase = Projectile.GetByUUID(base.Projectile.owner, (int)base.Projectile.ai[0]);
		if (chase >= 0 && Main.projectile[chase].active)
		{
			zeroing = Main.projectile[chase].Center;
			_ = Main.projectile[chase].velocity;
			float projRotation = Main.projectile[chase].rotation;
			float projScale = MathHelper.Clamp(Main.projectile[chase].scale, 0f, 50f);
			float sixteenScale = 16f;
			Main.projectile[chase].localAI[0] = base.Projectile.localAI[0] + 1f;
			if (base.Projectile.alpha > 0)
			{
				base.Projectile.alpha -= 40;
			}
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
			base.Projectile.velocity = Vector2.Zero;
			Vector2 rotateInLine = zeroing - base.Projectile.Center;
			if (projRotation != base.Projectile.rotation)
			{
				float angleWrap = MathHelper.WrapAngle(projRotation - base.Projectile.rotation);
				rotateInLine = rotateInLine.RotatedBy(angleWrap * 0.1f);
			}
			base.Projectile.rotation = rotateInLine.ToRotation() + (float)Math.PI / 2f;
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.scale = projScale;
			base.Projectile.width = (base.Projectile.height = (int)(10f * base.Projectile.scale));
			base.Projectile.Center = base.Projectile.position;
			if (rotateInLine != Vector2.Zero)
			{
				base.Projectile.Center = zeroing - Vector2.Normalize(rotateInLine) * sixteenScale * projScale;
			}
			base.Projectile.spriteDirection = ((rotateInLine.X > 0f) ? 1 : (-1));
		}
		else
		{
			for (int k = 0; k < 8; k++)
			{
				int seaDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 68, 0f, 0f, 100, default(Color), 1.25f);
				Dust obj = Main.dust[seaDust];
				obj.velocity *= 0.3f;
				Main.dust[seaDust].position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
				Main.dust[seaDust].position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2) + (float)Main.rand.Next(-4, 5);
				Main.dust[seaDust].noGravity = true;
			}
			base.Projectile.active = false;
			base.Projectile.Kill();
		}
	}
}
