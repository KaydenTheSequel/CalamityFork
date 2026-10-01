using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class DeathsAscension : ModItem, ILocalizedModType, IModType
{
	public const int RiftLifeTime = 600;

	public const float OrbitalScytheDamageMult = 0.4f;

	public const float RiftScytheDamageMult = 0.125f;

	public const int RiftOrbitalAmount = 4;

	public const int ScytheShotAmount = 4;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 70;
		base.Item.height = 70;
		base.Item.damage = 700;
		base.Item.knockBack = 9f;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.DamageType = DamageClass.Melee;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<DeathsAscensionSwing>();
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.shoot = ModContent.ProjectileType<DeathsAscensionProjectile>();
		}
		else
		{
			base.Item.shoot = ModContent.ProjectileType<DeathsAscensionSwing>();
		}
		return base.UseItem(player);
	}

	public override bool CanUseItem(Player player)
	{
		if ((float)player.altFunctionUse == 2f)
		{
			base.Item.useStyle = 1;
			base.Item.UseSound = SoundID.Item71;
			base.Item.useTurn = true;
			base.Item.autoReuse = true;
			base.Item.noMelee = false;
			base.Item.noUseGraphic = false;
			base.Item.channel = false;
		}
		else
		{
			base.Item.useStyle = 5;
			base.Item.UseSound = null;
			base.Item.useTurn = false;
			base.Item.autoReuse = false;
			base.Item.noMelee = true;
			base.Item.noUseGraphic = true;
			base.Item.channel = true;
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		int spreadfactor = 9;
		if ((float)player.altFunctionUse == 2f)
		{
			for (int index = 0; index < 4; index++)
			{
				float SpeedX = velocity.X + Main.rand.NextFloat(-spreadfactor, spreadfactor + 1);
				float SpeedY = velocity.Y + Main.rand.NextFloat(-spreadfactor, spreadfactor + 1);
				Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((float)damage * 0.125f), knockback, player.whoAmI);
			}
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == ModContent.ProjectileType<DeathsAscensionRift>() && p.owner == player.whoAmI && p.ai[0] <= 0f)
				{
					p.ai[0] = 10f;
				}
			}
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DeathsAscensionSwing>(), damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void UseItemFrame(Player player)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		player.itemLocation = player.HandPosition.Value;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1327).AddIngredient<RuinousSoul>(4).AddIngredient(521, 15)
			.AddIngredient<TwistingNether>(3)
			.AddTile(134)
			.Register();
	}
}
