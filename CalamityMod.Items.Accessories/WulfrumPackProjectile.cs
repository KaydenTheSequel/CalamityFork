using System.Linq;
using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class WulfrumPackProjectile : GlobalProjectile
{
	public override bool? CanUseGrapple(int type, Player player)
	{
		if (player.GetModPlayer<WulfrumPackPlayer>().WulfrumPackEquipped)
		{
			if (Main.projectile.Count((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<WulfrumHook>()) > 1)
			{
				return false;
			}
		}
		else if (Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<WulfrumHook>()))
		{
			return false;
		}
		return base.CanUseGrapple(type, player);
	}
}
