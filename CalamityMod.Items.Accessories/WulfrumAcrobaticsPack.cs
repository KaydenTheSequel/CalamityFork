using System;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Back })]
public class WulfrumAcrobaticsPack : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumHookShoot")
	{
		Volume = 0.7f,
		MaxInstances = 1,
		SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
	};

	public static readonly SoundStyle GrabSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumHookGrapple")
	{
		Volume = 0.7f,
		MaxInstances = 1,
		SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
	};

	public static readonly SoundStyle ReleaseSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumHookDisengage")
	{
		Volume = 0.7f,
		MaxInstances = 1,
		SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
	};

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 1;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.TryGetModPlayer<WulfrumPackPlayer>(out var mp))
		{
			return mp.hookCooldown <= 0;
		}
		return false;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		player.moveSpeed += 0.08f;
		player.GetModPlayer<WulfrumPackPlayer>().WulfrumPackEquipped = true;
		player.GetModPlayer<WulfrumPackPlayer>().PackItem = base.Item;
		player.maxFallSpeed *= 1.25f;
		Vector2 center = player.Center;
		Color val = Color.Lerp(Color.DeepSkyBlue, Color.GreenYellow, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.5f + 0.5f);
		Lighting.AddLight(center, ((Color)(ref val)).ToVector3());
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(5).AddIngredient<EnergyCore>().AddIngredient(85, 2)
			.AddTile(16)
			.Register();
	}
}
