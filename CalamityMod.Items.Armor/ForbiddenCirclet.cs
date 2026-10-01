using CalamityMod.Buffs.Summon.Whips;
using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Systems.Collections;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class ForbiddenCirclet : ModItem, ILocalizedModType, IModType
{
	public static float SummonDamageBoost = 0.1f;

	public static float RogueVelocityBoost = 0.15f;

	public static float SetBonusRogueStealth = 0.4f;

	public static int TagDuration = CalamityUtils.SecondsToFrames(10);

	public static int StormManaCost = 60;

	public static int StormCooldown = 45;

	public static int StormDamage = 60;

	public static float StormKB = 1f;

	public static int EaterSpawnCount = 6;

	public static int EaterSpawnCooldown = 15;

	public static int EaterDamage = 40;

	public static SummonTag summonTag = new SummonTag
	{
		MultiplicativeTagDamage = 0.25f,
		FlatTagDamage = 5,
		AllowsWhipStacking = true,
		TagOnHit = tagOnHit,
		TagModifyHitEffects = SummonTag.BlankTagModifyHit,
		AutoDrawTooltip = false
	};

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBoost.ToPercent(), RogueVelocityBoost.ToPercent());

	public static void tagOnHit(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			int damage = (int)Main.player[projectile.owner].GetBestClassDamage().ApplyTo((float)damageDone * summonTag.MultiplicativeTagDamage) + summonTag.FlatTagDamage;
			Projectile.NewProjectile(projectile.GetSource_OnHit(npc), npc.Center, Main.rand.NextVector2Circular(5f, 5f), ModContent.ProjectileType<ForbiddenCircletEater>(), damage, 3f, projectile.owner);
		}
	}

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			ArmorIDs.Head.Sets.DrawFullHair[base.Item.headSlot] = true;
		}
		summonTag.TagItem = base.Type;
		CalamityBuffSets.SummonTagDebuff.Add(ModContent.BuffType<ForbiddenStealthSummonTagBuff>(), summonTag);
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 1;
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = 5;
		base.Item.Calamity().donorItem = true;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == 3777)
		{
			return legs.type == 3778;
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadowLokis = true;
		player.armorEffectDrawOutlinesForbidden = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		int stormMana = (int)((float)StormManaCost * player.manaCost);
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), CalamityUtils.GetArmorSetBonusKey(), stormMana);
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.forbiddenCirclet = true;
		calamityPlayer.rogueStealthMax += SetBonusRogueStealth;
		calamityPlayer.wearingRogueArmor = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
		player.Calamity().rogueVelocity += RogueVelocityBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyAdamantiteBar", 10).AddIngredient(3783).AddTile(134)
			.Register();
	}
}
