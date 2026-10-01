using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TitanArm : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 58;
		base.Item.damage = 69;
		base.Item.knockBack = 80f;
		base.Item.useAnimation = (base.Item.useTime = 12);
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.rare = 7;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit = 100f;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 300);
		YeetEnemies(player, target, hit.Crit);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 300);
	}

	private void YeetEnemies(Player player, NPC target, bool crit)
	{
		float kbAmt = player.GetWeaponKnockback(base.Item, base.Item.knockBack) * target.knockBackResist;
		if (crit)
		{
			kbAmt *= 1.4f;
		}
		float kbAmtY = (target.noGravity ? (kbAmt * -0.5f) : (kbAmt * -0.75f));
		target.velocity.Y += kbAmtY;
		target.velocity.X += kbAmt * (float)player.direction;
	}
}
