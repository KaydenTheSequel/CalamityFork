using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class WulfrumDiggingTurtle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 10;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 38;
		base.Item.useAnimation = (base.Item.useTime = 8);
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.shootSpeed = 20f;
		base.Item.shoot = ModContent.ProjectileType<WulfrumDiggingTurtleProjectile>();
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool ConsumeItem(Player player)
	{
		return player.altFunctionUse != 2;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			bool explodedAny = false;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.owner == player.whoAmI && p.type == base.Item.shoot)
				{
					p.ai[1] = 1f;
					p.timeLeft = 1;
					p.ForceNetUpdate();
					explodedAny = true;
				}
			}
			if (explodedAny)
			{
				SoundEngine.PlaySound(in SoundID.Item73, position);
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe(15).AddIngredient<WulfrumMetalScrap>(3).AddIngredient<EnergyCore>().Register();
	}
}
