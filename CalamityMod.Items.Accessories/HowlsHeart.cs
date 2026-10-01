using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class HowlsHeart : ModItem, ILocalizedModType, IModType
{
	public const int HowlDamage = 45;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 4));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 26;
		base.Item.accessory = true;
		base.Item.value = Item.buyPrice(0, 20);
		base.Item.rare = 4;
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().howlsHeart = true;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_Accessory(base.Item);
			if (player.FindBuffIndex(ModContent.BuffType<HowlTrio>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<HowlTrio>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HowlsHeartHowl>()] < 1)
			{
				int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(45f);
				Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<HowlsHeartHowl>(), damage, 1f, player.whoAmI, 0f, 1f).originalDamage = 45;
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HowlsHeartCalcifer>()] < 1)
			{
				Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<HowlsHeartCalcifer>(), 0, 0f, player.whoAmI);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<HowlsHeartTurnipHead>()] < 1)
			{
				Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<HowlsHeartTurnipHead>(), 0, 0f, player.whoAmI);
			}
		}
	}

	public override void UpdateVanity(Player player)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().howlsHeartVanity = true;
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		IEntitySource source = player.GetSource_Accessory(base.Item);
		if (player.FindBuffIndex(ModContent.BuffType<HowlTrio>()) == -1)
		{
			player.AddBuff(ModContent.BuffType<HowlTrio>(), 3600);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<HowlsHeartHowl>()] < 1)
		{
			int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(45f);
			int p = Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<HowlsHeartHowl>(), damage, 1f, player.whoAmI, 0f, 1f);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = 45;
			}
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<HowlsHeartCalcifer>()] < 1)
		{
			Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<HowlsHeartCalcifer>(), 0, 0f, player.whoAmI);
		}
		if (player.ownedProjectileCounts[ModContent.ProjectileType<HowlsHeartTurnipHead>()] < 1)
		{
			Projectile.NewProjectile(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<HowlsHeartTurnipHead>(), 0, 0f, player.whoAmI);
		}
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 1f, new Vector2(0f, 0f), (SpriteEffects)0);
		return false;
	}
}
