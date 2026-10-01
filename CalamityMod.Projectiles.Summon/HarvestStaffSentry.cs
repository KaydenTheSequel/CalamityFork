using System;
using System.Collections.Generic;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HarvestStaffSentry : ModProjectile, ILocalizedModType, IModType
{
	public static float Gravity = 0.8f;

	public static float MaxGravity = 20f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.tileCollide = true;
		base.Projectile.width = 68;
		base.Projectile.height = 32;
	}

	public override void AI()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		if (PumpkinAmount() < HarvestStaff.PumpkinsPerSentry * SentryAmount())
		{
			Timer++;
		}
		if (Timer > HarvestStaff.TimePerPumpkin)
		{
			float randomOffset = 160f;
			Vector2 spawnPosition = base.Projectile.Center + new Vector2(Main.rand.NextFloat(0f - randomOffset, randomOffset), -80f);
			MakeSpawnValid(ref spawnPosition);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, Vector2.Zero, ModContent.ProjectileType<HarvestStaffMinion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, Main.rand.Next(3));
			Timer = 0f;
			base.Projectile.netUpdate = true;
		}
		float speed = base.Projectile.velocity.Y;
		if (speed < MaxGravity)
		{
			speed = MathF.Min(speed + Gravity, MaxGravity);
		}
		base.Projectile.velocity.Y = speed;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 10)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
	}

	public int PumpkinAmount()
	{
		int amount = 0;
		for (int i = 0; i < Main.maxProjectiles; i++)
		{
			Projectile proj = Main.projectile[i];
			if (proj != null && proj.active && proj.type == ModContent.ProjectileType<HarvestStaffMinion>() && proj.owner == base.Projectile.owner)
			{
				amount++;
			}
		}
		return amount;
	}

	public int SentryAmount()
	{
		int amount = 0;
		for (int i = 0; i < Main.maxProjectiles; i++)
		{
			Projectile proj = Main.projectile[i];
			if (proj != null && proj.active && proj.type == ModContent.ProjectileType<HarvestStaffSentry>() && proj.owner == base.Projectile.owner)
			{
				amount++;
			}
		}
		return amount;
	}

	public void MakeSpawnValid(ref Vector2 spawnpoint)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		Point spawnpointTileCoords = spawnpoint.ToSafeTileCoordinates();
		Point sentryTileCoords = base.Projectile.Top.ToSafeTileCoordinates();
		if (!Main.tile[spawnpointTileCoords].IsTileSolid())
		{
			return;
		}
		Point tileCoords = default(Point);
		while (Main.tile[spawnpoint.ToSafeTileCoordinates()].IsTileSolid())
		{
			for (int coordY = spawnpointTileCoords.Y; coordY < sentryTileCoords.Y; coordY++)
			{
				((Point)(ref tileCoords))._002Ector(spawnpointTileCoords.X, coordY);
				if (!Main.tile[tileCoords].IsTileSolid())
				{
					spawnpoint = tileCoords.ToVector2();
					break;
				}
			}
			spawnpoint.X += Main.rand.NextFloat(-160f, 160f);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(color: base.Projectile.GetAlpha(lightColor), origin: frame.Size() * 0.5f, texture: value, position: drawPosition, sourceRectangle: frame, rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}
}
