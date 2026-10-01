using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CrimslimeMinion : ModProjectile, ILocalizedModType, IModType
{
	public float dust;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.alpha = 75;
		base.Projectile.aiStyle = 26;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 23;
		base.AIType = 266;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override void AI()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (this.dust == 0f)
		{
			int constant = 16;
			for (int i = 0; i < constant; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (constant / 2 - 1)) * ((float)Math.PI * 2f) / (float)constant) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int dust = Dust.NewDust(val + faceDirection, 0, 0, 5, faceDirection.X * 1f, faceDirection.Y * 1f, 100, default(Color), 1.1f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].noLight = true;
				Main.dust[dust].velocity = faceDirection;
			}
			this.dust++;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<CrimslimeMinion>();
		player.AddBuff(ModContent.BuffType<Crimslime>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.cSlime2 = false;
			}
			if (modPlayer.cSlime2)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = texture2D13.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.EntitySpriteDraw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
