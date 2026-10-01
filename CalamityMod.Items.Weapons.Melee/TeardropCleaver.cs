using CalamityMod.Buffs.StatDebuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TeardropCleaver : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 66;
		base.Item.damage = 38;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 24;
		base.Item.useStyle = 1;
		base.Item.useTime = 24;
		base.Item.useTurn = true;
		base.Item.knockBack = 5.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<TemporalSadness>(), 60);
	}
}
