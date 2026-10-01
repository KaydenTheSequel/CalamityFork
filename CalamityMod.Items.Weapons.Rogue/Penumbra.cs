using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Penumbra : RogueWeapon
{
	public static float ShootSpeed = 9f;

	public override float StealthDamageMultiplier => 0.9f;

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 32;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item103;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.damage = 725;
		base.Item.crit = 16;
		base.Item.useTime = (base.Item.useAnimation = 35);
		base.Item.knockBack = 8f;
		base.Item.shoot = ModContent.ProjectileType<PenumbraBomb>();
		base.Item.shootSpeed = 9f;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			Vector2 realPlayerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
			float mouseXDist = Main.screenPosition.X + (float)Main.mouseX - realPlayerPos.X;
			float mouseYDist = Main.screenPosition.Y + (float)((player.gravDir == -1f) ? (Main.screenHeight - Main.mouseY) : Main.mouseY) - realPlayerPos.Y;
			if ((float.IsNaN(mouseXDist) && float.IsNaN(mouseYDist)) || (mouseXDist == 0f && mouseYDist == 0f))
			{
				mouseXDist = player.direction;
				mouseYDist = 0f;
			}
			realPlayerPos += new Vector2(mouseXDist, mouseYDist);
			int proj = Projectile.NewProjectile(source, realPlayerPos, -Vector2.UnitY * 0.25f, ModContent.ProjectileType<PenumbraBomb>(), damage, knockback, player.whoAmI);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].Calamity().stealthStrike = true;
				Main.projectile[proj].timeLeft = 450;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(8).AddIngredient<NightmareFuel>(20).AddIngredient<RuinousSoul>(6)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
