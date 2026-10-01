using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class FungalClump : ModItem, ILocalizedModType, IModType
{
	public const int FungalClumpDamage = 10;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 42;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().fungalClump = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		if (player.FindBuffIndex(ModContent.BuffType<FungalClumpBuff>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<FungalClumpBuff>(), 3600);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<FungalClumpMinion>()] < 1)
		{
			int p = Projectile.NewProjectile(player.GetSource_Accessory(base.Item), Damage: (int)player.GetBestClassDamage().ApplyTo(10f), X: player.Center.X, Y: player.Center.Y, SpeedX: 0f, SpeedY: -1f, Type: ModContent.ProjectileType<FungalClumpMinion>(), KnockBack: 1f, Owner: player.whoAmI);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = 10;
			}
		}
	}

	public override void UpdateVanity(Player player)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().fungalClumpVanity = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		if (player.FindBuffIndex(ModContent.BuffType<FungalClumpBuff>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<FungalClumpBuff>(), 3600);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<FungalClumpMinion>()] < 1)
		{
			int p = Projectile.NewProjectile(player.GetSource_Accessory(base.Item), Damage: (int)player.GetBestClassDamage().ApplyTo(10f), X: player.Center.X, Y: player.Center.Y, SpeedX: 0f, SpeedY: -1f, Type: ModContent.ProjectileType<FungalClumpMinion>(), KnockBack: 1f, Owner: player.whoAmI);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = 10;
			}
		}
	}
}
