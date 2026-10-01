using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "ClamorNoctus" })]
public class AlphaDraconis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 74;
		base.Item.damage = 150;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 12;
		base.Item.useTime = 12;
		base.Item.useAnimation = 12;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.2f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.UseSound = SoundID.Item105;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AlphaDraconisStar>();
		base.Item.shootSpeed = 8f;
	}

	public override bool AltFunctionUse(Player player)
	{
		if (player.Calamity().StratusStarburst < 5)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<DracoConstellation>()] > 0;
		}
		return true;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().StratusStarburstResetTimer = (int)MathHelper.Max((float)player.Calamity().StratusStarburstResetTimer, 600f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DracoConstellation>()] > 0)
			{
				Projectile projectile = Main.projectile.First((Projectile x) => x.type == ModContent.ProjectileType<DracoConstellation>() && x.active && x.owner == player.whoAmI);
				projectile.timeLeft = (int)MathHelper.Min((float)projectile.timeLeft, 60f);
			}
			else
			{
				Projectile.NewProjectile(source, player.Center + new Vector2(-125f, -100f), -Vector2.Zero, ModContent.ProjectileType<DracoConstellation>(), (int)((float)damage * 4f), knockback, player.whoAmI);
			}
			return false;
		}
		Vector2 mousePos = player.Calamity().mouseWorld;
		player.DirectionTo(mousePos);
		for (int i = 0; i < 3; i++)
		{
			Projectile.NewProjectile(source, new Vector2(mousePos.X, player.Center.Y) + new Vector2((float)(Main.rand.Next(500, 1300) * -player.direction), (float)Main.rand.Next(-800, -600)), Vector2.UnitX * (float)player.direction * ((Vector2)(ref velocity)).Length(), type, damage, knockback, player.whoAmI, mousePos.X, mousePos.Y);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WyvernsCall>().AddIngredient<Lumenyl>(6).AddIngredient<RuinousSoul>(5)
			.AddIngredient<ExodiumCluster>(10)
			.AddTile(134)
			.Register();
	}
}
