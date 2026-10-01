using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class JawsOfOblivion : RogueWeapon
{
	public override float StealthKnockbackMultiplier => 7f;

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 40;
		base.Item.damage = 159;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 1;
		base.Item.useTime = 15;
		base.Item.knockBack = 1f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<JawsProjectile>();
		base.Item.shootSpeed = 25f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		float spreadAngle = MathHelper.ToRadians(2.5f);
		Vector2 baseDirection = velocity.RotatedBy((0f - spreadAngle) * 2.5f);
		for (int i = 0; i < 6; i++)
		{
			Vector2 currentDirection = baseDirection.RotatedBy(spreadAngle * (float)i);
			currentDirection = currentDirection.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-1f, 1f)));
			if (player.Calamity().StealthStrikeAvailable())
			{
				int p = Projectile.NewProjectile(source, position, currentDirection, type, damage, knockback, player.whoAmI);
				if (p.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[p].Calamity().stealthStrike = true;
				}
			}
			else
			{
				Projectile.NewProjectile(source, position, currentDirection, type, damage, knockback, player.whoAmI);
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LeviathanTeeth>().AddIngredient<ReaperTooth>(6).AddIngredient<Lumenyl>(15)
			.AddIngredient<RuinousSoul>(2)
			.AddTile(134)
			.Register();
	}
}
