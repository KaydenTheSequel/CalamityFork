using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class HeartoftheElements : ModItem, ILocalizedModType, IModType
{
	public static int ElementalDamage = 50;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 8));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 10;
	}

	public override bool CanEquipAccessory(Player player, int slot, bool modded)
	{
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.brimElemental || modPlayer.sandElemental || modPlayer.rareSandElemental || modPlayer.cloudElemental || modPlayer.waterElemental)
		{
			return false;
		}
		return true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		if (player != null && !player.dead)
		{
			Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, (float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f);
		}
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.allElementals = true;
		calamityPlayer.elementalHeart = true;
		int brimmy = ModContent.ProjectileType<BrimstoneElementalMinion>();
		int siren = ModContent.ProjectileType<WaterElementalMinion>();
		int healer = ModContent.ProjectileType<SandElementalHealer>();
		int sandy = ModContent.ProjectileType<SandElementalMinion>();
		int cloudy = ModContent.ProjectileType<CloudElementalMinion>();
		IEntitySource source = player.GetSource_Accessory(base.Item);
		Vector2 velocity = default(Vector2);
		((Vector2)(ref velocity))._002Ector(0f, -1f);
		int elementalDmg = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ElementalDamage);
		float kBack = 2f + player.GetKnockback<SummonDamageClass>().Additive;
		if (player.ownedProjectileCounts[brimmy] > 1 || player.ownedProjectileCounts[siren] > 1 || player.ownedProjectileCounts[healer] > 1 || player.ownedProjectileCounts[sandy] > 1 || player.ownedProjectileCounts[cloudy] > 1)
		{
			player.ClearBuff(ModContent.BuffType<HotE>());
		}
		if (player == null || player.whoAmI != Main.myPlayer || player.dead)
		{
			return;
		}
		if (player.FindBuffIndex(ModContent.BuffType<HotE>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<HotE>(), 3600);
		}
		if (player.ownedProjectileCounts[brimmy] < 1)
		{
			int p = Projectile.NewProjectile(source, player.Center, velocity, brimmy, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = ElementalDamage;
			}
		}
		if (player.ownedProjectileCounts[siren] < 1)
		{
			int p2 = Projectile.NewProjectile(source, player.Center, velocity, siren, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p2))
			{
				Main.projectile[p2].originalDamage = ElementalDamage;
			}
		}
		if (player.ownedProjectileCounts[healer] < 1)
		{
			int p3 = Projectile.NewProjectile(source, player.Center, velocity, healer, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p3))
			{
				Main.projectile[p3].originalDamage = ElementalDamage;
			}
		}
		if (player.ownedProjectileCounts[sandy] < 1)
		{
			int p4 = Projectile.NewProjectile(source, player.Center, velocity, sandy, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p4))
			{
				Main.projectile[p4].originalDamage = ElementalDamage;
			}
		}
		if (player.ownedProjectileCounts[cloudy] < 1)
		{
			int p5 = Projectile.NewProjectile(source, player.Center, velocity, cloudy, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p5))
			{
				Main.projectile[p5].originalDamage = ElementalDamage;
			}
		}
	}

	public override void UpdateVanity(Player player)
	{
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().allElementalsVanity = true;
		int brimmy = ModContent.ProjectileType<BrimstoneElementalMinion>();
		int siren = ModContent.ProjectileType<WaterElementalMinion>();
		int healer = ModContent.ProjectileType<SandElementalHealer>();
		int sandy = ModContent.ProjectileType<SandElementalMinion>();
		int cloudy = ModContent.ProjectileType<CloudElementalMinion>();
		IEntitySource source = player.GetSource_Accessory(base.Item);
		Vector2 velocity = default(Vector2);
		((Vector2)(ref velocity))._002Ector(0f, -1f);
		int elementalDmg = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(ElementalDamage);
		float kBack = 2f + player.GetKnockback<SummonDamageClass>().Additive;
		if (player.ownedProjectileCounts[brimmy] > 1 || player.ownedProjectileCounts[siren] > 1 || player.ownedProjectileCounts[healer] > 1 || player.ownedProjectileCounts[sandy] > 1 || player.ownedProjectileCounts[cloudy] > 1)
		{
			player.ClearBuff(ModContent.BuffType<HotE>());
		}
		if (player == null || player.whoAmI != Main.myPlayer || player.dead)
		{
			return;
		}
		if (player.FindBuffIndex(ModContent.BuffType<HotE>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<HotE>(), 3600);
		}
		if (player.ownedProjectileCounts[brimmy] < 1)
		{
			int p = Projectile.NewProjectile(source, player.Center, velocity, brimmy, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = ElementalDamage;
			}
		}
		if (player.ownedProjectileCounts[siren] < 1)
		{
			int p2 = Projectile.NewProjectile(source, player.Center, velocity, siren, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p2))
			{
				Main.projectile[p2].originalDamage = ElementalDamage;
			}
		}
		if (player.ownedProjectileCounts[healer] < 1)
		{
			int p3 = Projectile.NewProjectile(source, player.Center, velocity, healer, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p3))
			{
				Main.projectile[p3].originalDamage = ElementalDamage;
			}
		}
		if (player.ownedProjectileCounts[sandy] < 1)
		{
			int p4 = Projectile.NewProjectile(source, player.Center, velocity, sandy, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p4))
			{
				Main.projectile[p4].originalDamage = ElementalDamage;
			}
		}
		if (player.ownedProjectileCounts[cloudy] < 1)
		{
			int p5 = Projectile.NewProjectile(source, player.Center, velocity, cloudy, elementalDmg, kBack, player.whoAmI);
			if (Main.projectile.IndexInRange(p5))
			{
				Main.projectile[p5].originalDamage = ElementalDamage;
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ElementalinaBottle>().AddIngredient<RareElementalinaBottle>().AddIngredient<PearlofEnthrallment>()
			.AddIngredient<EyeoftheStorm>()
			.AddIngredient<RoseStone>()
			.AddIngredient(3459, 6)
			.AddTile(412)
			.Register();
	}
}
