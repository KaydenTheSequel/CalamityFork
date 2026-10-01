using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BobbitHead : ModProjectile, ILocalizedModType, IModType
{
	public static Asset<Texture2D> Chain;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void Load()
	{
		Chain = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/BobbitHookChain", (AssetRequestMode)2);
	}

	public override void SetDefaults()
	{
		base.Projectile.CloneDefaults(230);
	}

	public override float GrappleRange()
	{
		return BobbitHook.Reach * 16f;
	}

	public override void GrappleRetreatSpeed(Player player, ref float speed)
	{
		speed = BobbitHook.ReelbackSpeed;
	}

	public override void GrapplePullSpeed(Player player, ref float speed)
	{
		speed = BobbitHook.PullSpeed;
	}

	public override bool? CanUseGrapple(Player player)
	{
		int hooksOut = 0;
		for (int l = 0; l < Main.maxProjectiles; l++)
		{
			if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == base.Projectile.type)
			{
				hooksOut++;
			}
		}
		if (hooksOut > 1)
		{
			return false;
		}
		return true;
	}

	public override void NumGrappleHooks(Player player, ref int numHooks)
	{
		numHooks = 1;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return base.Projectile.DrawHook(Chain.Value);
	}

	public override void AI()
	{
		base.Projectile.spriteDirection = -base.Projectile.direction;
		if (base.Projectile.ai[0] == 2f)
		{
			base.Projectile.extraUpdates = 1;
		}
		else
		{
			base.Projectile.extraUpdates = 0;
		}
	}
}
