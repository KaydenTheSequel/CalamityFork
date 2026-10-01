using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public abstract class SandBall : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public virtual bool Fired => true;

	public virtual int TileType => 53;

	public virtual int ItemType => 169;

	public virtual int DustType => 32;

	public virtual bool DropAsItem => false;

	public override void SetDefaults()
	{
		base.Projectile.knockBack = 6f;
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		if (Fired)
		{
			base.Projectile.MaxUpdates = 2;
			base.Projectile.DamageType = DamageClass.Ranged;
		}
		else
		{
			base.Projectile.hostile = true;
		}
	}

	public override void AI()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool() && DustType >= 0)
		{
			int i = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, DustType);
			Main.dust[i].velocity.X *= 0.4f;
			Main.dust[i].velocity.Y += (Fired ? 0f : (base.Projectile.velocity.Y * 0.5f));
		}
		base.Projectile.ai[1]++;
		base.Projectile.rotation += 0.1f;
		if (base.Projectile.ai[1] >= 60f || !Fired)
		{
			base.Projectile.ai[1] = 60f;
			base.Projectile.velocity.Y += 0.2f;
		}
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		Point p = base.Projectile.Center.ToTileCoordinates();
		if (p.X < 0 || p.X >= Main.maxTilesX || p.Y < 0 || p.Y >= Main.maxTilesY)
		{
			return;
		}
		if (DropAsItem)
		{
			if (Main.netMode != 1)
			{
				Item.NewItem(base.Projectile.GetSource_DropAsItem(), base.Projectile.position, base.Projectile.width, base.Projectile.height, ItemType);
			}
			return;
		}
		Tile placer = Main.tile[p.X, p.Y];
		if (placer.IsHalfBlock && base.Projectile.velocity.Y > 0f && Math.Abs(base.Projectile.velocity.Y) > Math.Abs(base.Projectile.velocity.X))
		{
			placer = Main.tile[p.X, --p.Y];
		}
		bool ValidTileBelow = true;
		bool SlopeTileBelow = false;
		if (!placer.HasTile && placer.TileType != 314)
		{
			if (p.Y + 1 < Main.maxTilesY)
			{
				Tile under = Main.tile[p.X, p.Y + 1];
				if (under.HasTile)
				{
					if (under.TileType == 314)
					{
						ValidTileBelow = false;
					}
					else if (under.IsHalfBlock || under.Slope != SlopeType.Solid)
					{
						SlopeTileBelow = true;
					}
				}
			}
			if (!ValidTileBelow)
			{
				return;
			}
			bool num = WorldGen.PlaceTile(p.X, p.Y, TileType, mute: false, forced: true);
			WorldGen.SquareTileFrame(p.X, p.Y);
			if (num & SlopeTileBelow)
			{
				WorldGen.SlopeTile(p.X, p.Y + 1);
				if (Main.netMode != 0)
				{
					NetMessage.SendData(17, -1, -1, null, 14, p.X, p.Y + 1);
				}
			}
			if (num && Main.netMode != 0)
			{
				NetMessage.SendData(17, -1, -1, null, 1, p.X, p.Y, TileType);
			}
		}
		else if (Main.netMode != 1)
		{
			Item.NewItem(base.Projectile.GetSource_DropAsItem(), base.Projectile.position, base.Projectile.width, base.Projectile.height, ItemType);
		}
	}
}
