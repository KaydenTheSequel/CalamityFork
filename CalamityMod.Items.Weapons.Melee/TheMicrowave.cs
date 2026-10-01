using CalamityMod.Projectiles.Melee.Yoyos;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TheMicrowave : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle BeepSound = new SoundStyle("CalamityMod/Sounds/Custom/MicrowaveBeep");

	public static readonly SoundStyle MMMSound = new SoundStyle("CalamityMod/Sounds/Custom/MMMMMMMMMMMMM")
	{
		IsLooped = true
	};

	public static float Reach = 512f;

	public static float Speed = 54f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Reach.ToTiles(), Speed);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[base.Type] = true;
		ItemID.Sets.GamepadExtraRange[base.Type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 34;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 111;
		base.Item.knockBack = 3f;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<MicrowaveYoyo>();
		base.Item.shootSpeed = 14f;
		base.Item.rare = 9;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
	}
}
