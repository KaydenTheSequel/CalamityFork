using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class CrystylCrusher : ModItem, ILocalizedModType, IModType
{
	private static int PickPower = 1000;

	private static float LaserSpeed = 14f;

	public static readonly SoundStyle ChargeSound = new SoundStyle("CalamityMod/Sounds/Item/CrystylCharge");

	public new string LocalizationCategory => "Items.Tools";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 70;
		base.Item.height = 70;
		base.Item.damage = 400;
		base.Item.knockBack = 9f;
		base.Item.useTime = 1;
		base.Item.useAnimation = 10;
		base.Item.pick = PickPower;
		base.Item.tileBoost = 50;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.channel = true;
		base.Item.useStyle = 1;
		base.Item.shootSpeed = LaserSpeed;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shoot = ModContent.ProjectileType<CrystylCrusherRay>();
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override Vector2? HoldoutOrigin()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (base.Item.useStyle == 1)
		{
			return null;
		}
		return new Vector2(10f, 10f);
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 25f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void HoldItem(Player player)
	{
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == player.whoAmI)
		{
			player.Calamity().rightClickListener = true;
		}
		if (player.Calamity().mouseRight && CanUseItem(player) && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.blockMouse)
		{
			if (!Main.projectile.Any((Projectile n) => n.active && n.type == ModContent.ProjectileType<CrystylCrusherRay>() && n.owner == player.whoAmI))
			{
				int damage = (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo(base.Item.damage);
				float kb = player.GetTotalKnockback<MeleeDamageClass>().ApplyTo(base.Item.knockBack);
				Projectile.NewProjectile(base.Item.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<CrystylCrusherRay>(), damage, kb, player.whoAmI);
				base.Item.shoot = ModContent.ProjectileType<CrystylCrusherRay>();
				base.Item.tileBoost = int.MinValue;
				base.Item.autoReuse = false;
			}
		}
		else if (player.ownedProjectileCounts[ModContent.ProjectileType<CrystylCrusherRay>()] <= 0)
		{
			base.Item.shoot = 0;
			base.Item.tileBoost = 50;
			base.Item.autoReuse = true;
		}
	}

	public override bool? UseItem(Player player)
	{
		base.Item.noMelee = player.altFunctionUse == 2;
		return base.UseItem(player);
	}

	public override void UseAnimation(Player player)
	{
		if ((float)player.altFunctionUse == 2f)
		{
			base.Item.useStyle = 5;
			base.Item.UseSound = null;
			base.Item.useTurn = false;
		}
		else
		{
			base.Item.useStyle = 1;
			base.Item.UseSound = SoundID.Item1;
			base.Item.useTurn = true;
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		if (base.Item.useStyle == 5)
		{
			TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "TileBoost");
			if (line != null)
			{
				line.Text = string.Empty;
			}
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 1 && Main.rand.NextBool(3))
		{
			int dustType = Utils.SelectRandom<int>(Main.rand, 173, 57, 58);
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, dustType);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("LunarPickaxe").AddIngredient<BlossomPickaxe>().AddIngredient<ShadowspecBar>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
