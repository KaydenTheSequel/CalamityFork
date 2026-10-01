using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.FishingRods;

[LegacyName(new string[] { "ChaoticSpreadRod" })]
public class RiftReeler : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.CanFishInLava[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 28;
		base.Item.useAnimation = 8;
		base.Item.useTime = 8;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.fishingPole = 45;
		base.Item.shootSpeed = 17f;
		base.Item.shoot = ModContent.ProjectileType<RiftReelerBobber>();
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(45f)), type, 10, 1f, player.whoAmI, 0f, 0f, Main.rand.Next(2));
		}
		return false;
	}

	public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		lineOriginOffset = new Vector2(67f, -33f);
		if (bobber.ai[2] == 0f)
		{
			lineColor = new Color(255, 165, 0, 100);
		}
		else
		{
			lineColor = new Color(200, 206, 209, 100);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2422).AddIngredient<ScoriaBar>(6).AddTile(134)
			.Register();
	}
}
