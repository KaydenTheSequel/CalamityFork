using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.FishingRods;

public class SlurperPole : ModItem, ILocalizedModType, IModType
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
		base.Item.fishingPole = 25;
		base.Item.shootSpeed = 14f;
		base.Item.shoot = ModContent.ProjectileType<SlurperBobber>();
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
	}

	public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		lineOriginOffset = new Vector2(45f, -43f);
		lineColor = new Color(227, 79, 79, 100);
	}
}
