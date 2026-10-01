using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class StarofDestruction : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.3f;

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 94);
		base.Item.damage = 438;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 90);
		base.Item.useStyle = 1;
		base.Item.knockBack = 10f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.shoot = ModContent.ProjectileType<DestructionBolt>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			bool spawner = true;
			for (int t = 0; t < 4; t++)
			{
				for (int i = 0; i < 5; i++)
				{
					float rot = MathHelper.ToRadians(-36f) + MathHelper.ToRadians(18f) * (float)i;
					Vector2 vel = velocity.RotatedBy(rot) * i switch
					{
						3 => 0.85f, 
						1 => 0.85f, 
						4 => 0.7f, 
						0 => 0.7f, 
						_ => 1f, 
					};
					int proj = Projectile.NewProjectile(source, position, vel, type, damage, knockback, player.whoAmI, 0f, i);
					if (proj.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[proj].ai[2] = 1f;
					}
					if (spawner && i == 4)
					{
						Main.projectile[proj].ai[2] = 15f;
						spawner = false;
					}
				}
				velocity = velocity.RotatedBy(MathHelper.ToRadians(90f));
			}
		}
		else
		{
			for (int j = 0; j < 5; j++)
			{
				float rot2 = -0.8f + 0.4f * (float)j;
				Vector2 vel2 = velocity.RotatedBy(rot2) * j switch
				{
					3 => 0.85f, 
					1 => 0.85f, 
					4 => 0.7f, 
					0 => 0.7f, 
					_ => 1f, 
				};
				Projectile.NewProjectile(source, position, vel2, type, damage, knockback, player.whoAmI, 0f, j);
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MeldConstruct>(18).AddTile(412).Register();
	}
}
