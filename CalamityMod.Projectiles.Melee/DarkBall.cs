using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DarkBall : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.scale = 0.9f;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 200;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			int tileXMin = (int)(base.Projectile.position.X / 16f) - 1;
			int tileXMax = (int)((base.Projectile.position.X + (float)base.Projectile.width) / 16f) + 2;
			int tileYMin = (int)(base.Projectile.position.Y / 16f) - 1;
			int tileYMax = (int)((base.Projectile.position.Y + (float)base.Projectile.height) / 16f) + 2;
			if (tileXMin < 0)
			{
				tileXMin = 0;
			}
			if (tileXMax > Main.maxTilesX)
			{
				tileXMax = Main.maxTilesX;
			}
			if (tileYMin < 0)
			{
				tileYMin = 0;
			}
			if (tileYMax > Main.maxTilesY)
			{
				tileYMax = Main.maxTilesY;
			}
			Vector2 projPos = default(Vector2);
			for (int i = tileXMin; i < tileXMax; i++)
			{
				for (int j = tileYMin; j < tileYMax; j++)
				{
					if (Main.tile[i, j] != null && Main.tile[i, j].HasUnactuatedTile && (Main.tileSolid[Main.tile[i, j].TileType] || (Main.tileSolidTop[Main.tile[i, j].TileType] && Main.tile[i, j].TileFrameY == 0)))
					{
						projPos.X = i * 16;
						projPos.Y = j * 16;
						if (base.Projectile.position.X + (float)base.Projectile.width - 4f > projPos.X && base.Projectile.position.X + 4f < projPos.X + 16f && base.Projectile.position.Y + (float)base.Projectile.height - 4f > projPos.Y && base.Projectile.position.Y + 4f < projPos.Y + 16f)
						{
							base.Projectile.velocity.X = 0f;
							base.Projectile.velocity.Y = -0.2f;
						}
					}
				}
			}
		}
		catch
		{
		}
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.timeLeft <= 3)
		{
			base.Projectile.tileCollide = false;
			base.Projectile.ai[1] = 0f;
			base.Projectile.alpha = 255;
			base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
			base.Projectile.width = 128;
			base.Projectile.height = 128;
			base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
			base.Projectile.knockBack = 8f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 10f)
		{
			base.Projectile.ai[0] = 10f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
				if ((double)base.Projectile.velocity.X > -0.01 && (double)base.Projectile.velocity.X < 0.01)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.18f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath21, base.Projectile.position);
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int corruptDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj = Main.dust[corruptDust];
			obj.velocity *= 1.4f;
		}
		for (int j = 0; j < 10; j++)
		{
			int corruptDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[corruptDust2].noGravity = true;
			Dust obj2 = Main.dust[corruptDust2];
			obj2.velocity *= 5f;
			corruptDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj3 = Main.dust[corruptDust2];
			obj3.velocity *= 3f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrainRot>(), 120);
		base.Projectile.Kill();
	}
}
