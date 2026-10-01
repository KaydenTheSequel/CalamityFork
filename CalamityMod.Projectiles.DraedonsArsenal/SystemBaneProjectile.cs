using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class SystemBaneProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const int LightningFireRate = 60;

	public const int FieldLightningFireRate = 45;

	public const float FieldRadius = 360f;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Items/Weapons/DraedonsArsenal/SystemBane";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 34);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 480;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.velocity.Y < 15f)
		{
			base.Projectile.velocity.Y += 0.5f;
		}
		if (Time % 15f == 0f)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 229);
			dust.velocity = Main.rand.NextVector2Circular(10f, 10f);
			dust.fadeIn = 1.05f;
			dust.noGravity = true;
		}
		if (Time % 60f == 0f && Main.myPlayer == base.Projectile.owner)
		{
			int lightningDamage = base.Projectile.damage;
			int totalSystemBanes = Main.player[base.Projectile.owner].ownedProjectileCounts[base.Type];
			lightningDamage = (int)Math.Ceiling((double)lightningDamage / Math.Pow(totalSystemBanes, 1.0 / 3.0));
			NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(900f);
			if (potentialTarget != null)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(potentialTarget.Center) * 15f, ModContent.ProjectileType<SystemBaneLightning>(), lightningDamage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			NPC potentialTarget2 = base.Projectile.Center.ClosestNPCAt(360f);
			if (Time % 45f == 0f && potentialTarget2 != null && Main.myPlayer == base.Projectile.owner)
			{
				Vector2 spawnPosition = base.Projectile.Center + Main.rand.NextVector2CircularEdge(360f, 360f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, potentialTarget2.DirectionFrom(spawnPosition) * 14f, ModContent.ProjectileType<SystemBaneLightning>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike)
		{
			int totalCirclePoints = 55;
			float generalOpacity = Utils.GetLerpValue(0f, 30f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(480f, 450f, base.Projectile.timeLeft, clamped: true);
			Texture2D lightningTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/LightningProj", (AssetRequestMode)2).Value;
			for (int i = 0; i < totalCirclePoints; i++)
			{
				float angle = (float)Math.PI * 2f * (float)i / (float)totalCirclePoints;
				float nextAngle = angle + (float)Math.PI * 2f / (float)totalCirclePoints;
				float radiusOffset = (float)Math.Cos(Main.GlobalTimeWrappedHourly * 65f);
				Vector2 start = base.Projectile.Center + angle.ToRotationVector2() * (360f + radiusOffset) - Main.screenPosition;
				Vector2 end = base.Projectile.Center + nextAngle.ToRotationVector2() * (360f + radiusOffset) - Main.screenPosition;
				DelegateMethods.f_1 = 0.75f * generalOpacity;
				DelegateMethods.c_1 = SystemBaneLightning.InnerLightningColor;
				Utils.DrawLaser(Main.spriteBatch, lightningTexture, start, end, new Vector2(0.3f), DelegateMethods.LightningLaserDraw);
				DelegateMethods.f_1 = 0.35f * generalOpacity;
				DelegateMethods.c_1 = SystemBaneLightning.OuterLightningColor;
				Utils.DrawLaser(Main.spriteBatch, lightningTexture, start, end, new Vector2(0.5f), DelegateMethods.LightningLaserDraw);
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.velocity.X *= 0.8f;
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}
}
