using CalamityMod.Projectiles.Melee.MaceFlails;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class BallOFugu : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle BlowSound = new SoundStyle("CalamityMod/Sounds/Item/FuguBlow")
	{
		PitchVariance = 0.1f
	};

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[base.Type] = 2f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 10;
		base.Item.damage = 28;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<BallOFuguProj>();
		base.Item.shootSpeed = 12f;
	}
}
